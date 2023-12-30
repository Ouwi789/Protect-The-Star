using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Camera mainCam;
    [SerializeField] private float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += mainCam.transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= mainCam.transform.right * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= mainCam.transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += mainCam.transform.right * speed;
        }
    }
}
