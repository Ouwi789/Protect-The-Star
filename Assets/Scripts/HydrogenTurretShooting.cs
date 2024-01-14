using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydrogenTurretShooting : MonoBehaviour
{
    public GameObject bullet;
    private StatsHolder stats;
    public LayerMask hitLayers;

    private int damage;
    private float range;
    private float reloadTime;
    private float reloadCounter = 0f;

    private void Awake()
    {
        stats = GameObject.FindGameObjectWithTag("GameController").GetComponent<StatsHolder>();
        damage = (int)stats.buidlings["Hydrogen Turret"]["damage"];
        reloadTime = (float)stats.buidlings["Hydrogen Turret"]["cooldown"];
        range = (float)stats.buidlings["Hydrogen Turret"]["range"];
    }

    void Update()
    {
        if (reloadCounter >= reloadTime)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, hitLayers);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.tag == "Enemy")
                {
                    Instantiate(bullet, transform.position, Quaternion.identity);
                    reloadCounter = 0f;
                }
            }
        }
        reloadCounter += Time.deltaTime;
    }
}
