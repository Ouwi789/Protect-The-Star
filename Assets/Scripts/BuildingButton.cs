using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButton : MonoBehaviour
{
    //sun coords for placement
    public GameObject sun;
    private Vector3 centreOfSphere;

    //turret
    public GameObject turret;
    private bool turretPlacing;
    private bool tempTurretPlaced;
    private GameObject tempTurret;

    //hydrogen generator
    public GameObject hydroGen;
    private bool hydroGenPlacing;
    private bool tempHydroGenPlaced;
    private GameObject tempHydroGen;

    //mouse pos
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    public Plane plane = new Plane(Vector3.down, 0);
    public LayerMask hitLayers;

    //can place
    private bool canPlace;
    

    // Start is called before the first frame update
    void Start()
    {
        centreOfSphere = sun.transform.position;

        turretPlacing = false;
        tempTurretPlaced = false;
        hydroGenPlacing = false;
        tempHydroGenPlaced = false;
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
                if (Input.GetMouseButtonDown(0) && canPlace)
                {
                    //mouse has been clicked, place the tower!
                    Instantiate(turret, tempTurret.transform.position, tempTurret.transform.rotation);
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
                if (Input.GetMouseButtonDown(0) && canPlace)
                {
                    Instantiate(hydroGen, tempHydroGen.transform.position, tempHydroGen.transform.rotation);
                    Destroy(tempHydroGen);
                    hydroGenPlacing = false;
                    tempHydroGenPlaced = false;
                }
            }
            canPlace = tempHydroGen.GetComponent<BuildingCollision>().canPlace;
        }
    }


    public void onTurretClick()
    {
        turretPlacing = true;
    }

    public void onHydroGenClick()
    {
        hydroGenPlacing = true;
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
