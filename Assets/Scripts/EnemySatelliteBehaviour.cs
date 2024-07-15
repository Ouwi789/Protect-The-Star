using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySatelliteBehaviour : EnemyBehaviour
{
    private bool canShoot;
    [SerializeField] private float reloadTime;
    private float reloadCounter;
    [SerializeField] private int damage;
    public Animator enemyAnimator;
    // Start is called before the first frame update
    void Start()
    {
        enemySetup();
        canShoot = false;
        reloadCounter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        updateHealth();
        if(canShoot)
        {
           Shoot();
        }
    }

    private void FixedUpdate()
    {
        moveEnemy();
    }

    private void OnTriggerEnter(Collider other)
    {
        onHit(other);
    }

    public override void moveEnemy()
    {
        Vector3 currentPos = transform.position;
        float dis = Vector3.Distance(targetPosition, currentPos);
        if (dis >= 50f)
        {
            Vector3 direction = targetPosition - currentPos;
            direction.Normalize();
            transform.LookAt(targetPosition);
            rb.MovePosition(currentPos + (speed * Time.fixedDeltaTime * direction));
        }
        else
        {
            canShoot = true;
            transform.RotateAround(sun.transform.position, Vector3.forward, speed * Time.fixedDeltaTime * 0.7f);
        }
    }
    public override void Attack()
    {
        enemyAnimator.Play("Shoot");
        healthScript.setHealth(healthScript.getHealth() - (damage*(StatsHolder.defence / 200)));
    }
    private void Shoot()
    {
        if (reloadCounter >= reloadTime)
        {
            Attack();
            reloadCounter = 0f;
        }
        else
        {
            reloadCounter += Time.deltaTime;
        }
    }
}
