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
    private bool inOrbit;
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
        } else
        {
            isFake = false;
            placePos = addDistance(sun.transform, gameObject.transform);
            print(placePos);
        }
    }

    private void FixedUpdate()
    {
        if (!isFake)
        {
             if(!checkDistance(transform.position, placePos))
             {
                inOrbit = false;
                Vector3 currentPos = transform.position;
                Vector3 direction = placePos - currentPos;
                direction.Normalize();
                rb.MovePosition(currentPos + (10f * Time.fixedDeltaTime * direction));
             }
             else
             {
                 if (!inOrbit)
                 {
                    inOrbit = true;
                    transform.parent = sun.transform;
                 }
             }
        }
    }

    private bool checkDistance(Vector3 currPos, Vector3 targetPos) {
        if(Mathf.Abs(currPos.x - targetPos.x) < 0.2 && Mathf.Abs(currPos.y - targetPos.y) < 0.2 && Mathf.Abs(currPos.z - targetPos.z) < 0.2)
        {
            return true;
        }
        return false;
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

    public Vector3 addDistance(Transform originPos, Transform currPos)
    {
        float x = currPos.position.x - (originPos.position.x - currPos.position.x);
        float y = currPos.position.y - (originPos.position.y - currPos.position.y);
        float z = currPos.position.z - (originPos.position.z - currPos.position.z);
        return new Vector3(x, y, z);
    }

}
