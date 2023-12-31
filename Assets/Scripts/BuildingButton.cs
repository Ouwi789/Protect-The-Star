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

    //mouse pos
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    public Plane plane = new Plane(Vector3.down, 0);
    public LayerMask hitLayers;
    

    // Start is called before the first frame update
    void Start()
    {
        centreOfSphere = sun.transform.position;

        turretPlacing = false;
        tempTurretPlaced = false;
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
                tempTurret = createBuilding(turret);
                colorGreen(tempTurret);
                tempTurretPlaced = true;
            } else
            {
               // Vector3 normal = (worldPosition - centreOfSphere).normalized;
               // Quaternion rotation = Quaternion.LookRotation(normal);
                tempTurret.transform.position = worldPosition;
                tempTurret.transform.LookAt(centreOfSphere);
                tempTurret.transform.rotation *= Quaternion.Euler(-90, 0, 0);
                if (Input.GetMouseButtonDown(0))
                {
                    //mouse has been clicked, place the tower!
                    Instantiate(turret, tempTurret.transform.position, tempTurret.transform.rotation);
                    Destroy(tempTurret);
                    turretPlacing = false;
                    tempTurretPlaced = false;
                }
            }
        }
    }


    public void onTurretClick()
    {
        turretPlacing = true;
    }
    private GameObject createBuilding(GameObject building)
    {
        //TODO get mouse pos

        GameObject temp = Instantiate(building, Vector3.zero, Quaternion.identity);
        return temp;
        
    }
    private void colorGreen(GameObject building)
    {
        Transform parent = building.transform;
        foreach(Transform child in parent)
        {
            Renderer tempRenderer = child.GetComponent<Renderer>();
            tempRenderer.material.color = Color.green;
        }
    }
    private void colorRed(GameObject building)
    {
        Transform parent = building.transform;
        foreach (Transform child in parent)
        {
            Renderer tempRenderer = child.GetComponent<Renderer>();
            tempRenderer.material.color = Color.red;
        }
    }
}
