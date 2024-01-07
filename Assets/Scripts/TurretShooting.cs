using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    public GameObject bullet;
    public float reloadTime = 6f;
    private float reloadCounter = 0f;

    void Update()
    {
        if(reloadCounter >= reloadTime)
        {
            reloadCounter = 0f;
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
        reloadCounter += Time.deltaTime;
    }
}
