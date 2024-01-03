using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomChange;
    public float smoothChange;
    public float minSize, maxSize;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            cam.fieldOfView -= zoomChange * Time.deltaTime * smoothChange;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            cam.fieldOfView += zoomChange * Time.deltaTime * smoothChange;
        }
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minSize, maxSize);
    }
}
