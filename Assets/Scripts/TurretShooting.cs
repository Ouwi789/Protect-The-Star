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

    [SerializeField] bool forShow;

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
        damage = (int) stats.buidlings[name]["damage"] / upgradeStat.upgradeState;
        reloadTime = (float)stats.buidlings[name]["cooldown"];
        range = (float)stats.buidlings[name]["range"];
    }

    void Update()
    {
        if (!forShow)
        {
            if (reloadCounter >= reloadTime)
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, hitLayers);
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.tag == "Enemy")
                    {
                        if (hitCollider.GetComponent<EnemySuicideBehaviour>() != null)
                        {
                            hitCollider.gameObject.GetComponent<EnemyBehaviour>().setHealth((int)(hitCollider.gameObject.GetComponent<EnemyBehaviour>().health - damage * StatsHolder.attackMultiplier * StatsHolder.attackMeleeMultiplier));
                        }
                        else
                        {
                            hitCollider.gameObject.GetComponent<EnemyBehaviour>().setHealth((int)(hitCollider.gameObject.GetComponent<EnemyBehaviour>().health - damage * StatsHolder.attackMultiplier * StatsHolder.attackRangedMultiplier));
                        }

                        GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
                        reloadCounter = 0f;
                    }
                }
            }
            reloadCounter += Time.deltaTime;
        }
    } 
}
