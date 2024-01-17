using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildingSelection : MonoBehaviour
{
    public Vector3 screenPosition;
    public LayerMask hitLayers;

    //statsStorage
    public StatsHolder stats;

    //building stats
    public GameObject buildingStats;
    public Image atkImage;
    public TMP_Text atkText;
    public TMP_Text rangeText;
    public TMP_Text cooldownText;
    public Image buildingImage;
    public TMP_Text buildingName;
    public TMP_Text upgradeText;

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
                    //he has clicked on the building, create sphere
                    if(firstTime)
                    {
                        sphereScript = hitData.transform.gameObject.GetComponent<BuildingCollision>();
                        sphereScript.CreateSphere();
                        buildingStats.SetActive(true);
                        ToggleStats(hitData.transform.gameObject);
                        selected = hitData.transform.gameObject;
                        firstTime = false;
                    } else
                    {
                        sphereScript.DeleteSphere();
                        buildingStats.SetActive(false);
                        sphereScript = hitData.transform.gameObject.GetComponent<BuildingCollision>();
                        sphereScript.CreateSphere();
                        buildingStats.SetActive(true);
                        ToggleStats(hitData.transform.gameObject);
                        selected = hitData.transform.gameObject;
                    }
                }
            } else
            {
                if (Input.GetMouseButtonDown(0) && !firstTime && !hoverUpgrade)
                {
                    sphereScript.DeleteSphere();
                    buildingStats.SetActive(false);
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
        buildingName.SetText(name);
        atkText.SetText(stats.buidlings[name]["damage"].ToString());
        cooldownText.SetText(stats.buidlings[name]["cooldown"].ToString());
        rangeText.SetText(stats.buidlings[name]["range"].ToString());
    }
    public void Upgrade()
    {
        print("A");
        string name = GetBuildingName(selected);
        string upgradedName = stats.buidlings[name]["upgrade"].ToString();
        if(upgradedName != "MAX")
        {
            GameObject upgradedBuilding = (GameObject) stats.buidlings[upgradedName]["object"];
            upgradedBuilding.tag = "Building";
            upgradedBuilding.GetComponent<BuildingCollision>().upgradeState = selected.GetComponent<BuildingCollision>().upgradeState + 1;
            //TODO add in costs
            Instantiate(upgradedBuilding, selected.transform.position, selected.transform.rotation);
            Destroy(selected);
        }
        buildingStats.SetActive(false);
        sphereScript.DeleteSphere();
    }
    public void hoverUpgradeEnter()
    {
        hoverUpgrade = true;
    }
    public void hoverUpgradeExit()
    {
        hoverUpgrade = false;
    }
}
