using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    public GameObject bullet;
    private StatsHolder stats;
    private BuildingCollision upgradeStat;
    public LayerMask hitLayers;

    private int damage;
    private float range;
    private float reloadTime;
    private float reloadCounter = 0f;

    private void Awake()
    {
        string name = "Helium Turret";
        upgradeStat = GetComponent<BuildingCollision>();
        if(upgradeStat == null)
        {
            //smaller cube, child of the main turret
            upgradeStat = transform.parent.gameObject.GetComponent<BuildingCollision>();
        }
        stats = GameObject.FindGameObjectWithTag("GameController").GetComponent<StatsHolder>();
        if(upgradeStat.upgradeState > 1)
        {
            name += " " + upgradeStat.upgradeState;
        }
        damage = (int) stats.buidlings[name]["damage"];
        reloadTime = (float)stats.buidlings[name]["cooldown"];
        range = (float)stats.buidlings[name]["range"];
    }

    void Update()
    {
        if(reloadCounter >= reloadTime)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, hitLayers);
            foreach (var hitCollider in hitColliders)
            {
                if(hitCollider.tag == "Enemy")
                {
                    hitCollider.gameObject.GetComponent<EnemyMovement>().health -= damage;
                    GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
                    temp.GetComponent<BulletScript>().damage = damage;
                    reloadCounter = 0f;
                }
            }
        }
        reloadCounter += Time.deltaTime;
    }
}
