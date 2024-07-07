using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildingSelection : MonoBehaviour
{
    public Vector3 screenPosition;
    public LayerMask hitLayers;

    //button UI (platform / buidling)
    public GameObject platformButtons;
    public GameObject buildingButtons;
    private GameObject selectedPlatform;

    //statsStorage
    public StatsHolder stats;

    //buildingButton
    public BuildingButton buildingButton;

    //building stats
    public GameObject buildingStats;
    public Image atkImage;
    public TMP_Text atkText;
    public TMP_Text rangeText;
    public TMP_Text cooldownText;
    public Image buildingImage;
    public TMP_Text buildingName;
    public TMP_Text upgradeText;
    public Image currencyIcon;

    //icons
    public Sprite atkIcon;
    public Sprite hydrogenIcon;
    public Sprite heliumIcon;

    //building icons
    public Sprite turret;
    public Sprite hydrogen;
    public Sprite helium;
    public Sprite hydrogenTurret;

    private BuildingCollision sphereScript;
    private GameObject selected;
    bool firstTime = true;

    private bool hoverUpgrade = false;


    // Update is called once per frame
    void Update()
    {
        screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hitData, 100, hitLayers))
        {
            if(hitData.transform.gameObject.tag == "Building")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    onBuildingClick(hitData.transform.gameObject);
                }
            } else if(hitData.transform.gameObject.tag == "Platform")
            {
                if (Input.GetMouseButton(0))
                {
                    if(hitData.transform.gameObject.GetComponent<PlatformBehaviour>().isOccupied())
                    {
                        GameObject data = hitData.transform.gameObject.GetComponent<PlatformBehaviour>().getBuilding();
                        onBuildingClick(data);
                    } else
                    {
                        platformButtons.SetActive(false);
                        buildingButtons.SetActive(true);
                        selectedPlatform = hitData.transform.gameObject;
                    }
                }
                
            }
            else
            {
                if (Input.GetMouseButtonDown(0) && !firstTime && !hoverUpgrade)
                {
                    sphereScript.DeleteSphere();
                }
                if (Input.GetMouseButton(0) && !buildingButton.hovered)
                {
                    switchToPlatformState();
                }

            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && !firstTime && !hoverUpgrade)
            {
                sphereScript.DeleteSphere();
                buildingStats.SetActive(false);
            }
            if (Input.GetMouseButton(0) && !buildingButton.hovered)
            {
                switchToPlatformState();
            }
        }
    }

    void onBuildingClick(GameObject building)
    {
        if (firstTime)
        {
            sphereScript = building.GetComponent<BuildingCollision>();
            sphereScript.CreateSphere();
            buildingStats.SetActive(true);
            ToggleStats(building);
            selected = building;
            firstTime = false;
        }
        else
        {
            sphereScript.DeleteSphere();
            buildingStats.SetActive(false);
            sphereScript = building.GetComponent<BuildingCollision>();
            sphereScript.CreateSphere();
            buildingStats.SetActive(true);
            ToggleStats(building);
            selected = building;
        }
    }

    string addUpgradedName(GameObject building, string baseName)
    {
        if(building.GetComponent<BuildingCollision>().upgradeState != 1)
        {
            baseName = baseName + " " + building.GetComponent<BuildingCollision>().upgradeState.ToString();
        }
        return baseName;
    }

    string GetBuildingName(GameObject building)
    {
        //WARNING: Only gives the base upgrade state
        string name = "";
        if (building.GetComponent<TurretShooting>() != null)
        {
            name = addUpgradedName(building, "Helium Turret");
        }
        if (building.GetComponent<HydroGenProduction>() != null)
        {
            name = addUpgradedName(building, "Hydrogen Generator");
        }
        if (building.GetComponent<HeliumGenProduction>() != null)
        {
            name = addUpgradedName(building, "Helium Generator");
        }
        if (building.GetComponent<HydrogenTurretShooting>() != null)
        {
            name = addUpgradedName(building, "Hydrogen Turret");
        }
        return name;
    }

    void ToggleStats(GameObject building)
    {
        string name = GetBuildingName(building);
        if(building.GetComponent<TurretShooting>() != null)
        {
            atkImage.sprite = atkIcon;
            buildingImage.sprite = turret;
            
        }
        if (building.GetComponent<HydroGenProduction>() != null)
        {
            atkImage.sprite = hydrogenIcon;
            buildingImage.sprite = hydrogen;
        }
        if (building.GetComponent<HeliumGenProduction>() != null)
        {
            atkImage.sprite = heliumIcon;
            buildingImage.sprite = helium;
        }
        if (building.GetComponent<HydrogenTurretShooting>() != null)
        {
            atkImage.sprite = atkIcon;
            buildingImage.sprite = hydrogenTurret;
        }
        if(stats.buidlings[name]["upgrade"].ToString() == "MAX") {
            upgradeText.SetText("Upgrade: MAX");
            atkText.SetText(stats.buidlings[name]["damage"].ToString());
            cooldownText.SetText(stats.buidlings[name]["cooldown"].ToString());
            rangeText.SetText(stats.buidlings[name]["range"].ToString());
        } else
        {
            string upgradedName = stats.buidlings[name]["upgrade"].ToString();
            int upgradeCost = (int) stats.buidlings[upgradedName]["cost"];
            upgradeText.SetText("Upgrade: " + upgradeCost);
            atkText.SetText(stats.buidlings[name]["damage"].ToString() + " -> " + stats.buidlings[upgradedName]["damage"].ToString());
            cooldownText.SetText(stats.buidlings[name]["cooldown"].ToString() + " -> " + stats.buidlings[upgradedName]["cooldown"].ToString());
            rangeText.SetText(stats.buidlings[name]["range"].ToString() + " -> " + stats.buidlings[upgradedName]["range"].ToString());
        }
        if (stats.buidlings[name]["currency"].ToString() == "h")
        {
            currencyIcon.sprite = hydrogenIcon;

        } else
        {
            currencyIcon.sprite = heliumIcon;
        }
        buildingName.SetText(name);
        
    }
    public void Upgrade()
    {
        string name = GetBuildingName(selected);
        string upgradedName = stats.buidlings[name]["upgrade"].ToString();
        if (upgradedName != "MAX" && checkUpgradeCostAndPay(upgradedName)) //TODO add currency for cost so we know 
        {
            GameObject upgradedBuilding = (GameObject) stats.buidlings[upgradedName]["object"];
            GameObject temp = Instantiate(upgradedBuilding, selected.transform.position, selected.transform.rotation);
            temp.tag = "Building"; ;
            temp.GetComponent<BuildingCollision>().upgradeState = selected.GetComponent<BuildingCollision>().upgradeState + 1;
            Destroy(selected);
        }
        buildingStats.SetActive(false);
        sphereScript.DeleteSphere();
        firstTime = true;
        hoverUpgrade = false;
    }
    public void hoverUpgradeEnter()
    {
        hoverUpgrade = true;
    }
    public void hoverUpgradeExit()
    {
        hoverUpgrade = false;
    }
    private bool checkUpgradeCostAndPay(string upgradedName)
    {
        int upgradeCost = (int)stats.buidlings[upgradedName]["cost"];
        if (stats.buidlings[upgradedName]["currency"].ToString() == "h")
        {
            if (stats.getHydrogen() >= upgradeCost)
            {
                stats.setHydrogen(stats.getHydrogen() - upgradeCost);
                return true;
            } else
            {
                return false;
            }
        }
        else
        {
            if (stats.getHelium() >= upgradeCost)
            {
                stats.setHelium(stats.getHelium() - upgradeCost);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public GameObject getPlatform()
    {
        return selectedPlatform;
    }
    public void switchToPlatformState()
    {
        platformButtons.SetActive(true);
        buildingButtons.SetActive(false);
    }
}
