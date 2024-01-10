using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSelection : MonoBehaviour
{
    public Vector3 screenPosition;
    public LayerMask hitLayers;
    public GameObject buildingStats;
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
                        firstTime = false;
                    } else
                    {
                        sphereScript.DeleteSphere();
                        buildingStats.SetActive(false);
                        sphereScript = hitData.transform.gameObject.GetComponent<BuildingCollision>();
                        sphereScript.CreateSphere();
                        buildingStats.SetActive(true);
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
}
