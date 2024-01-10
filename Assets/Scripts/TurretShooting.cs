using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    public GameObject bullet;
    public float reloadTime = 6f;
    private float reloadCounter = 0f;
    public LayerMask hitLayers;
    public float range = 15f;

    void Update()
    {
        if(reloadCounter >= reloadTime)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, hitLayers);
            foreach (var hitCollider in hitColliders)
            {
                if(hitCollider.tag == "Enemy")
                {
                    Instantiate(bullet, transform.position, Quaternion.identity);
                    reloadCounter = 0f;
                }
            }
        }
        reloadCounter += Time.deltaTime;
    }
}
