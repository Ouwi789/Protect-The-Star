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
    private Vector3 placePos;
    public GameObject sun;
    private Rigidbody rb;

    private void Awake()
    {
        canPlace = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        sun = GameObject.FindGameObjectWithTag("Sun");
        rb = GetComponent<Rigidbody>();
        if(gameObject.tag == "Fake")
        {
            isFake = true;
            colorGreen(gameObject);
        }
        if(gameObject.tag == "Building")
        {
            moveBuilding();
        }
    }

    private void FixedUpdate()
    {

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
            sphere.transform.parent = gameObject.transform;
            renderer.material = sphereColor;
            sphere.transform.localScale = new Vector3(range * 2, range * 2, range * 2);
            spherePlaced = true;
        }
    }
    public void DeleteSphere()
    {
        sphere.SetActive(false);
    }
    public void moveBuilding()
    {
        float xPos = gameObject.transform.position.x + (gameObject.transform.position.x - sun.transform.position.x) *0.05f;
        float yPos = gameObject.transform.position.y + (gameObject.transform.position.y - sun.transform.position.y) *0.05f;
        float zPos = gameObject.transform.position.z + (gameObject.transform.position.z - sun.transform.position.z) *0.05f;
        Vector3 position = new(xPos, yPos, zPos);
        rb.position = position;
    }

}
