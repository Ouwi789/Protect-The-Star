using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterBehaviour : EnemyBehaviour
{
    private bool canShoot;
    private float reloadTime;
    private float reloadCounter;
    [SerializeField] private int damage;
    public GameObject laser;
    // Start is called before the first frame update
    void Start()
    {
        enemySetup();
        canShoot = false;
        reloadTime = 5f;
        reloadCounter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        updateHealth();
        if (canShoot)
        {
            Shoot();
        }
    }
    private void FixedUpdate()
    {
        //not being able to shoot means enemy still has to travel further (no need to call if it is within distance)
        if(!canShoot)
        {
            moveEnemy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        onHit(other);
    }

    public override void moveEnemy()
    {
        Vector3 currentPos = transform.position;
        float dis = Vector3.Distance(targetPosition, currentPos);
        if(dis >= 50f)
        {
            Vector3 direction = targetPosition - currentPos;
            direction.Normalize();
            transform.LookAt(targetPosition);
            rb.MovePosition(currentPos + (speed * Time.fixedDeltaTime * direction));
        } else
        {
            canShoot = true;
        }
    }
    public override void Attack()
    {
        StartCoroutine(AttackAnimation());
        healthScript.setHealth(healthScript.getHealth() - (damage * (StatsHolder.defence / 200)));
    }
    private IEnumerator AttackAnimation()
    {
        GameObject temp = Instantiate(laser, transform.position, Quaternion.identity);
        temp.transform.LookAt(sun.transform);
        yield return new WaitForSeconds(3f);
        Destroy(temp);
        yield return null;
    }
    private void Shoot()
    {
        if(reloadCounter >= reloadTime)
        {
            Attack();
            reloadCounter = 0f;
        } else
        {
            reloadCounter += Time.deltaTime;
        }
    }
}

