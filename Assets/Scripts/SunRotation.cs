using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour
{
    float zDegree = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        zDegree += 0.001f;
        Rotate();
    }
    void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, zDegree));
    }
}
