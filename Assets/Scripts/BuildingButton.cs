using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingButton : MonoBehaviour
{

    //TODO do not allow multiple turrets on platform, have a variable named 'occupied'

    private int platformNum;

    //cancel text
    public GameObject cancelText;
    public GameObject costTextObject;
    public TMP_Text costText;
    //sun coords for placement
    public GameObject sun;
    private Vector3 centreOfSphere;

    //helium platform
    public GameObject heliumPlatform;
    private bool heliumPlatformPlacing;
    private bool tempHeliumPlatformPlaced;
    private GameObject tempHeliumPlatform;

    //hydrogen platform
    public GameObject hydrogenPlatform;
    private bool hydrogenPlatformPlacing;
    private bool tempHydrogenPlatformPlaced;
    private GameObject tempHydrogenPlatform;

    //helium turret
    public GameObject turret;
    private bool turretPlacing;
    private bool tempTurretPlaced;
    private GameObject tempTurret;

    //hydrogen turret
    public GameObject hydrogenTurret;
    private bool hydrogenTurretPlacing;
    private bool temphydrogenTurretPlaced;
    private GameObject temphydrogenTurret;

    //hydrogen generator
    public GameObject hydroGen;
    private bool hydroGenPlacing;
    private bool tempHydroGenPlaced;
    private GameObject tempHydroGen;

    //helium generator
    public GameObject heliumGen;
    private bool heliumGenPlacing;
    private bool tempHeliumGenPlaced;
    private GameObject tempHeliumGen;

    //mouse pos
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    public LayerMask hitLayers;

    //can place
    private bool canPlace;

    //onHover
    public bool hovered;

    //hydrogen and helium stats
    public StatsHolder stats;

    //platformSelection Script
    public BuildingSelection platformSelector;

    // Start is called before the first frame update
    void Start()
    {
        platformNum = 0;
        centreOfSphere = sun.transform.position;
        //platforms
        hydrogenPlatformPlacing = false;
        tempHydrogenPlatformPlaced = false;
        heliumPlatformPlacing = false;
        tempHeliumPlatformPlaced = false;
        //buildings
        turretPlacing = false;
        tempTurretPlaced = false;
        hydroGenPlacing = false;
        tempHydroGenPlaced = false;
        heliumGenPlacing = false;
        tempHeliumGenPlaced = false;
        //hovered
        hovered = false;
    }

    // Update is called once per frame
    void Update()
    {
        screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hitData, 100, hitLayers))
        {
            worldPosition = hitData.point;
        }

        if (turretPlacing)
        {
            GameObject tempPlatform = platformSelector.getPlatform();
            tempPlatform.GetComponent<PlatformBehaviour>().setOccupied(true);
            GameObject temp = Instantiate(turret, tempPlatform.transform.position, tempPlatform.transform.rotation);
            stats.setHelium(stats.getHelium() - (int) (stats.getCost("Helium Turret")  * StatsHolder.heliumDiscount));
            temp.tag = "Building";
            tempPlatform.GetComponent<PlatformBehaviour>().setBuilding(temp);
            Destroy(tempTurret);
            turretPlacing = false;
            tempTurretPlaced = false;
            platformSelector.switchToPlatformState();

            /* if (!tempTurretPlaced)
              {
                  tempTurret = createTempBuilding(turret);
                  tempTurretPlaced = true;
              } else
              {
                  // Vector3 normal = (worldPosition - centreOfSphere).normalized;
                  // Quaternion rotation = Quaternion.LookRotation(normal);
                  moveTempBuilding(tempTurret);
                  if (Input.GetKeyDown(KeyCode.Q))
                  {
                      //cancel placement
                      cancelText.SetActive(false);
                      Destroy(tempTurret);
                      turretPlacing = false;
                      tempTurretPlaced = false;
                  }
                  if (Input.GetMouseButtonDown(0) && canPlace)
                  {
                      //mouse has been clicked, place the tower!

                      //possibly change the position to initially be on Sun and change the building script to animate it to the outside
                      cancelText.SetActive(false);
                      GameObject temp = Instantiate(turret, tempTurret.transform.position, tempTurret.transform.rotation);
                      stats.setHelium(stats.getHelium() - stats.getCost("Helium Turret"));
                      temp.tag = "Building";
                      Destroy(tempTurret);
                      turretPlacing = false;
                      tempTurretPlaced = false;
                  }
              } 
              canPlace = tempTurret.GetComponent<BuildingCollision>().canPlace; */
        } else if (hydroGenPlacing)
        {
            GameObject tempPlatform = platformSelector.getPlatform();
            tempPlatform.GetComponent<PlatformBehaviour>().setOccupied(true);
            GameObject temp = Instantiate(hydroGen, tempPlatform.transform.position, tempPlatform.transform.rotation);
            stats.setHydrogen(stats.getHydrogen() - (int) (stats.getCost("Hydrogen Generator") * StatsHolder.hydrogenDiscount));
            temp.tag = "Building";
            tempPlatform.GetComponent<PlatformBehaviour>().setBuilding(temp);
            Destroy(tempHydroGen);
            hydroGenPlacing = false;
            tempHydroGenPlaced = false;
            platformSelector.switchToPlatformState();
        }
        else if (heliumGenPlacing)
        {
            GameObject tempPlatform = platformSelector.getPlatform();
            tempPlatform.GetComponent<PlatformBehaviour>().setOccupied(true);
            GameObject temp = Instantiate(heliumGen, tempPlatform.transform.position, tempPlatform.transform.rotation);
            stats.setHelium(stats.getHelium() - (int)(stats.getCost("Helium Generator") * StatsHolder.heliumDiscount));
            temp.tag = "Building";
            tempPlatform.GetComponent<PlatformBehaviour>().setBuilding(temp);
            Destroy(tempHeliumGen);
            heliumGenPlacing = false;
            tempHeliumGenPlaced = false;
            platformSelector.switchToPlatformState();
        }
        else if (hydrogenTurretPlacing)
        {
            GameObject tempPlatform = platformSelector.getPlatform();
            tempPlatform.GetComponent<PlatformBehaviour>().setOccupied(true);
            GameObject temp = Instantiate(hydrogenTurret, tempPlatform.transform.position, tempPlatform.transform.rotation);
            stats.setHydrogen(stats.getHydrogen() - (int)(stats.getCost("Hydrogen Turret") * StatsHolder.hydrogenDiscount));
            temp.tag = "Building";
            tempPlatform.GetComponent<PlatformBehaviour>().setBuilding(temp);
            Destroy(temphydrogenTurret);
            hydrogenTurretPlacing = false;
            temphydrogenTurretPlaced = false;
            platformSelector.switchToPlatformState();
        }
        else if (hydrogenPlatformPlacing)
        {
            if (!tempHydrogenPlatformPlaced)
            {
                tempHydrogenPlatform = createTempPlatform(hydrogenPlatform);
                tempHydrogenPlatformPlaced = true;
            }
            else
            {
                moveTempPlatform(tempHydrogenPlatform);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    cancelText.SetActive(false);
                    Destroy(tempHydrogenPlatform);
                    hydrogenPlatformPlacing = false;
                    tempHydrogenPlatformPlaced = false;
                }
                if (Input.GetMouseButtonDown(0) && canPlace)
                {
                    cancelText.SetActive(false);
                    GameObject temp = Instantiate(hydrogenPlatform, tempHydrogenPlatform.transform.position, tempHydrogenPlatform.transform.rotation);
                    platformNum++;
                    stats.setHydrogen(stats.getHydrogen() - (int)(stats.getCost("Hydrogen Platform") * StatsHolder.hydrogenDiscount));
                    temp.tag = "Platform";
                    temp.GetComponent<PlatformBehaviour>().setToHydrogen(true);
                    Destroy(tempHydrogenPlatform);
                    hydrogenPlatformPlacing = false;
                    tempHydrogenPlatformPlaced = false;
                }
            }
            canPlace = tempHydrogenPlatform.GetComponent<BuildingCollision>().canPlace;
        }
        else if (heliumPlatformPlacing)
        {
            if (!tempHeliumPlatformPlaced)
            {
                tempHeliumPlatform = createTempPlatform(heliumPlatform);
                tempHeliumPlatformPlaced = true;
            }
            else
            {
                moveTempPlatform(tempHeliumPlatform);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    cancelText.SetActive(false);
                    Destroy(tempHeliumPlatform);
                    heliumPlatformPlacing = false;
                    tempHeliumPlatformPlaced = false;
                }
                if (Input.GetMouseButtonDown(0) && canPlace)
                {
                    cancelText.SetActive(false);
                    GameObject temp = Instantiate(heliumPlatform, tempHeliumPlatform.transform.position, tempHeliumPlatform.transform.rotation);
                    platformNum++;
                    stats.setHelium(stats.getHelium() - (int)(stats.getCost("Helium Platform") * StatsHolder.heliumDiscount));
                    temp.tag = "Platform";
                    temp.GetComponent<PlatformBehaviour>().setToHydrogen(false);
                    Destroy(tempHeliumPlatform);
                    heliumPlatformPlacing = false;
                    tempHeliumPlatformPlaced = false;
                }
            }
            canPlace = tempHeliumPlatform.GetComponent<BuildingCollision>().canPlace;
        }
    }

    public void onHydrogenPlatClick()
    {
        if (checkCanPlace() && stats.getHydrogen() >= (stats.getCost("Hydrogen Platform") * StatsHolder.hydrogenDiscount) && platformNum < 40)
        {
            cancelText.SetActive(true);
            hydrogenPlatformPlacing = true;
        }
    }

    public void onHeliumPlatClick()
    {
        if (checkCanPlace() && stats.getHelium() >= (stats.getCost("Helium Platform") * StatsHolder.heliumDiscount) && platformNum < 40)
        {
            cancelText.SetActive(true);
            heliumPlatformPlacing = true;
        }
    }


    public void onTurretClick()
    {
        if (checkCanPlace() && stats.getHelium() >= (stats.getCost("Helium Turret") * StatsHolder.heliumDiscount))
        {
            turretPlacing = true;
        }
    }
    public void onHydroTurretClick()
    {
        if (checkCanPlace() && stats.getHydrogen() >= (stats.getCost("Hydrogen Turret") * StatsHolder.hydrogenDiscount))
        {
            hydrogenTurretPlacing = true;
        }
    }

    public void onHydroGenClick()
    {
        if (checkCanPlace() && stats.getHydrogen() >= (stats.getCost("Hydrogen Generator") * StatsHolder.hydrogenDiscount))
        {
            hydroGenPlacing = true;
        }
    }

    public void onHeliumGenClick()
    {
        if (checkCanPlace() && stats.getHelium() >= (stats.getCost("Helium Generator") * StatsHolder.heliumDiscount))
        {
            heliumGenPlacing = true;
        }
    }

    public void OnHydroPlatHover()
    {
        costTextObject.SetActive(true);
        costText.SetText("Hydrogen: " + (stats.getCost("Hydrogen Platform") * StatsHolder.hydrogenDiscount));
    }

    public void OnHeliumPlatHover()
    {
        costTextObject.SetActive(true);
        costText.SetText("Helium: " + (stats.getCost("Helium Platform") * StatsHolder.heliumDiscount));
    }

    public void OnTurretHover()
    {
        costTextObject.SetActive(true);
        costText.SetText("Helium: " + (stats.getCost("Helium Turret") * StatsHolder.heliumDiscount));
        tempTurret = createTempBuilding(turret, platformSelector.getPlatform());
        tempTurretPlaced = true;
        hovered = true;
    }

    public void OnHydroTurretHover()
    {
        costTextObject.SetActive(true);
        costText.SetText("Hydrogen: " + (stats.getCost("Hydrogen Turret") * StatsHolder.hydrogenDiscount));
        temphydrogenTurret = createTempBuilding(hydrogenTurret, platformSelector.getPlatform());
        temphydrogenTurretPlaced = true;
        hovered = true;
    }

    public void OnHydroGenHover()
    {
        costTextObject.SetActive(true);
        costText.SetText("Hydrogen: " + (stats.getCost("Hydrogen Generator") * StatsHolder.hydrogenDiscount));
        tempHydroGen = createTempBuilding(hydroGen, platformSelector.getPlatform());
        tempHydroGenPlaced = true;
        hovered = true;
    }

    public void OnHeliumGenHover()
    {
        costTextObject.SetActive(true);
        costText.SetText("Helium: " + (stats.getCost("Helium Generator") * StatsHolder.heliumDiscount));
        tempHeliumGen = createTempBuilding(heliumGen, platformSelector.getPlatform());
        tempHeliumGenPlaced = true;
        hovered = true;
    }

    public void OnExit()
    {
        costTextObject.SetActive(false);
        hovered = false;
        if (tempTurretPlaced)
        {
            tempTurretPlaced = false;
            Destroy(tempTurret);
        }
        if (tempHydroGenPlaced)
        {
            tempHydroGenPlaced = false;
            Destroy(tempHydroGen);
        }
        if (tempHeliumGenPlaced)
        {
            tempHeliumGenPlaced = false;
            Destroy(tempHeliumGen);
        }
        if (temphydrogenTurretPlaced)
        {
            temphydrogenTurretPlaced = false;
            Destroy(temphydrogenTurret);
        }
    }

    private bool checkCanPlace()
    {
        return !(turretPlacing || hydroGenPlacing || heliumGenPlacing || hydrogenTurretPlacing || heliumPlatformPlacing || hydrogenPlatformPlacing);
    }

    private GameObject createTempPlatform(GameObject building)
    {
        //TODO get mouse pos

        GameObject temp = Instantiate(building, Vector3.zero, Quaternion.identity);
        temp.tag = "Fake";
        canPlace = temp.GetComponent<BuildingCollision>().canPlace;
        return temp;
        
    }
    private GameObject createTempBuilding (GameObject building, GameObject platform)
    {
        GameObject temp = Instantiate(building, platform.transform.position, platform.transform.rotation);
        temp.tag = "Fake";
        canPlace = temp.GetComponent<BuildingCollision>().canPlace;
        return temp;
    }
    private void moveTempPlatform(GameObject building)
    {
        building.transform.position = worldPosition;
        building.transform.LookAt(centreOfSphere);
        building.transform.rotation *= Quaternion.Euler(-90, 0, 0);
    }

}
