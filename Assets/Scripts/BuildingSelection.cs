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

    private BuildingCollision sphereScript;
    bool firstTime = true;


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
                        firstTime = false;
                    } else
                    {
                        sphereScript.DeleteSphere();
                        buildingStats.SetActive(false);
                        sphereScript = hitData.transform.gameObject.GetComponent<BuildingCollision>();
                        sphereScript.CreateSphere();
                        buildingStats.SetActive(true);
                        ToggleStats(hitData.transform.gameObject);
                    }
                }
            } else
            {
                if (Input.GetMouseButtonDown(0) && !firstTime)
                {
                    sphereScript.DeleteSphere();
                    buildingStats.SetActive(false);
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && !firstTime)
            {
                sphereScript.DeleteSphere();
                buildingStats.SetActive(false);
            }
        }
    }

    void ToggleStats(GameObject building)
    {
        string name = "";
        if(building.GetComponent<TurretShooting>() != null)
        {
            name = "Turret";
            atkImage.sprite = atkIcon;
            buildingImage.sprite = turret;
            
        }
        if (building.GetComponent<HydroGenProduction>() != null)
        {
            name = "Hydrogen Generator";
            atkImage.sprite = hydrogenIcon;
            buildingImage.sprite = hydrogen;
        }
        if (building.GetComponent<HeliumGenProduction>() != null)
        {
            name = "Helium Generator";
            atkImage.sprite = heliumIcon;
            buildingImage.sprite = helium;
        }
        buildingName.SetText(name);
        atkText.SetText(stats.buidlings[name]["damage"].ToString());
        cooldownText.SetText(stats.buidlings[name]["cooldown"].ToString());
        rangeText.SetText(stats.buidlings[name]["range"].ToString());
    }

}
