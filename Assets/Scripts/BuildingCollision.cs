using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCollision : MonoBehaviour
{
    bool isFake;
    public bool canPlace;
    public Material sphereColor;
    public float range;
    public GameObject sphere;
    bool spherePlaced = false;
    public int upgradeState = 1;

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
        if (isFake && other.tag == "Building")
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

    public void CreateSphere()
    {
        if (spherePlaced)
        {
            sphere.SetActive(true);
        } else
        {
            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Renderer renderer = sphere.GetComponent<Renderer>();
            sphere.layer = 1;
            sphere.transform.position = transform.position;
            renderer.material = sphereColor;
            sphere.transform.localScale = new Vector3(range * 2, range * 2, range * 2);
            spherePlaced = true;
        }
    }
    public void DeleteSphere()
    {
        sphere.SetActive(false);
    }
}
