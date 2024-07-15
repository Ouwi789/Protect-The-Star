using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydrogenTurretShooting : MonoBehaviour
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
        if (upgradeStat == null)
        {
            //the gun barrel within main gun within the object
            upgradeStat = transform.parent.parent.gameObject.GetComponent<BuildingCollision>();
        }
        stats = GameObject.FindGameObjectWithTag("GameController").GetComponent<StatsHolder>();
        if (upgradeStat.upgradeState > 1)
        {
            name += " " + upgradeStat.upgradeState;
        }
        damage = (int)stats.buidlings[name]["damage"] / (upgradeStat.upgradeState * 2);
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
                        //rotate the gun
                        if (hitCollider.gameObject != null)
                        {
                            StartCoroutine(moveTurrets(transform.parent.gameObject, hitCollider.gameObject));
                        }


                        GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
                        reloadCounter = 0f;
                    }
                }
            }
            reloadCounter += Time.deltaTime;
        }
    }
    IEnumerator moveTurrets(GameObject gun, GameObject target)
    {
        gun.transform.LookAt(target.transform, Vector3.back);
        gun.transform.rotation = Quaternion.Euler(0, transform.parent.rotation.y, 0);
        yield return new WaitForSeconds(2f);
        gun.transform.rotation = new Quaternion(0, 0, 0, 0);
        yield return null;
    }
}
