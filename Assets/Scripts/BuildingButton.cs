using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingButton : MonoBehaviour
{
    
    //cancel text
    public GameObject cancelText;
    public GameObject costTextObject;
    public TMP_Text costText;
    //sun coords for placement
    public GameObject sun;
    private Vector3 centreOfSphere;

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

    //hydrogen and helium stats
    public StatsHolder stats;
    private int turretCost = 5; //helium
    private int hydrogenTurretCost = 6; //hydrogen
    private int hydrogenCost = 5; //hydrogen
    private int heliumCost = 5; //helium

    // Start is called before the first frame update
    void Start()
    {

        centreOfSphere = sun.transform.position;

        turretPlacing = false;
        tempTurretPlaced = false;
        hydroGenPlacing = false;
        tempHydroGenPlaced = false;
        heliumGenPlacing = false;
        tempHeliumGenPlaced = false;
    }

    // Update is called once per frame
    void Update()
    {
        screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if(Physics.Raycast(ray, out RaycastHit hitData, 100, hitLayers))
        {
            worldPosition = hitData.point;
        }

        if (turretPlacing)
        {
            if(!tempTurretPlaced)
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
                    cancelText.SetActive(false);
                    GameObject temp = Instantiate(turret, tempTurret.transform.position, tempTurret.transform.rotation);
                    stats.setHelium(stats.getHelium() - turretCost);
                    temp.tag = "Building";
                    Destroy(tempTurret);
                    turretPlacing = false;
                    tempTurretPlaced = false;
                }
            }
            canPlace = tempTurret.GetComponent<BuildingCollision>().canPlace;
        } else if (hydroGenPlacing)
        {
            if (!tempHydroGenPlaced)
            {
                tempHydroGen = createTempBuilding(hydroGen);
                tempHydroGenPlaced = true;
            }
            else
            {
                moveTempBuilding(tempHydroGen);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    cancelText.SetActive(false);
                    Destroy(tempHydroGen);
                    hydroGenPlacing = false;
                    tempHydroGenPlaced = false;
                }
                if (Input.GetMouseButtonDown(0) && canPlace)
                {
                    cancelText.SetActive(false);
                    GameObject temp = Instantiate(hydroGen, tempHydroGen.transform.position, tempHydroGen.transform.rotation);
                    stats.setHydrogen(stats.getHydrogen() - hydrogenCost);
                    temp.tag = "Building";
                    Destroy(tempHydroGen);
                    hydroGenPlacing = false;
                    tempHydroGenPlaced = false;
                }
            }
            canPlace = tempHydroGen.GetComponent<BuildingCollision>().canPlace;
        }
        else if (heliumGenPlacing)
        {
            if (!tempHeliumGenPlaced)
            {
                tempHeliumGen = createTempBuilding(heliumGen);
                tempHeliumGenPlaced = true;
            }
            else
            {
                moveTempBuilding(tempHeliumGen);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    cancelText.SetActive(false);
                    Destroy(tempHeliumGen);
                    heliumGenPlacing = false;
                    tempHeliumGenPlaced = false;
                }
                if (Input.GetMouseButtonDown(0) && canPlace)
                {
                    cancelText.SetActive(false);
                    GameObject temp = Instantiate(heliumGen, tempHeliumGen.transform.position, tempHeliumGen.transform.rotation);
                    stats.setHelium(stats.getHelium() - heliumCost);
                    temp.tag = "Building";
                    Destroy(tempHeliumGen);
                    heliumGenPlacing = false;
                    tempHeliumGenPlaced = false;
                }
            }
            canPlace = tempHeliumGen.GetComponent<BuildingCollision>().canPlace;
        }
        else if (hydrogenTurretPlacing)
        {
            if (!temphydrogenTurretPlaced)
            {
                temphydrogenTurret = createTempBuilding(hydrogenTurret);
                temphydrogenTurretPlaced = true;
            }
            else
            {
                moveTempBuilding(temphydrogenTurret);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    cancelText.SetActive(false);
                    Destroy(temphydrogenTurret);
                    hydrogenTurretPlacing = false;
                    temphydrogenTurretPlaced = false;
                }
                if (Input.GetMouseButtonDown(0) && canPlace)
                {
                    cancelText.SetActive(false);
                    GameObject temp = Instantiate(hydrogenTurret, temphydrogenTurret.transform.position, temphydrogenTurret.transform.rotation);
                    stats.setHydrogen(stats.getHydrogen() - hydrogenTurretCost);
                    temp.tag = "Building";
                    Destroy(temphydrogenTurret);
                    hydrogenTurretPlacing = false;
                    temphydrogenTurretPlaced = false;
                }
            }
            canPlace = temphydrogenTurret.GetComponent<BuildingCollision>().canPlace;
        }
    }


    public void onTurretClick()
    {
        if (checkCanPlace() && stats.getHelium() >= turretCost)
        {
            cancelText.SetActive(true);
            turretPlacing = true;
        }
    }
    public void onHydroTurretClick()
    {
        if (checkCanPlace() && stats.getHydrogen() >= turretCost)
        {
            cancelText.SetActive(true);
            hydrogenTurretPlacing = true;
        }
    }

    public void onHydroGenClick()
    {
        if (checkCanPlace() && stats.getHydrogen() >= hydrogenCost)
        {
            cancelText.SetActive(true);
            hydroGenPlacing = true;
        }
    }

    public void onHeliumGenClick()
    {
        if (checkCanPlace() && stats.getHelium() >= heliumCost)
        {
            cancelText.SetActive(true);
            heliumGenPlacing = true;
        }
    }

    public void OnTurretHover()
    {
        costTextObject.SetActive(true);
        costText.SetText("Helium: " + turretCost);
    }

    public void OnHydroTurretHover()
    {
        costTextObject.SetActive(true);
        costText.SetText("Hydrogen: " + hydrogenTurretCost);
    }

    public void OnHydroGenHover()
    {
        costTextObject.SetActive(true);
        costText.SetText("Hydrogen: " + hydrogenCost);
    }

    public void OnHeliumGenHover()
    {
        costTextObject.SetActive(true);
        costText.SetText("Helium: " + heliumCost);
    }

    public void OnExit()
    {
        costTextObject.SetActive(false);
    }

    private bool checkCanPlace()
    {
        return !(turretPlacing || hydroGenPlacing || heliumGenPlacing);
    }

    private GameObject createBuilding(GameObject building)
    {
        //TODO get mouse pos

        GameObject temp = Instantiate(building, Vector3.zero, Quaternion.identity);
        return temp;
        
    }
    private GameObject createTempBuilding (GameObject building)
    {
        GameObject temp = createBuilding(building);
        temp.tag = "Fake";
        canPlace = temp.GetComponent<BuildingCollision>().canPlace;
        return temp;
    }
    private void moveTempBuilding(GameObject building)
    {
        building.transform.position = worldPosition;
        building.transform.LookAt(centreOfSphere);
        building.transform.rotation *= Quaternion.Euler(-90, 0, 0);
    }
}
