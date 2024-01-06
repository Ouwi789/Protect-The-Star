using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCollision : MonoBehaviour
{
    bool isFake;
    public bool canPlace;


    private void Awake()
    {
        canPlace = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag == "Fake")
        {
            isFake = true;
            colorGreen(gameObject);
        } else
        {
            isFake = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isFake && other.tag != "Sun")
        {
            colorRed(gameObject);
            canPlace = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isFake)
        {
            colorGreen(gameObject);
            canPlace = true;
        }
    }

    private void colorGreen(GameObject building)
    {
        Transform parent = building.transform;
        foreach (Transform child in parent)
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
