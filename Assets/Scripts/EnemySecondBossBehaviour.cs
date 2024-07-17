using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySecondBossBehaviour : EnemyBehaviour
{
    private bool canShoot;
    [SerializeField] private float reloadTime;
    private float reloadCounter;
    [SerializeField] private int damage;
    public Animator enemyAnimator;

    private StatsHolder healthUI;

    // Start is called before the first frame update

    public override void enemySetup()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        sun = GameObject.FindGameObjectWithTag("Sun");
        targetPosition = sun.transform.position;
        healthScript = gameController.GetComponent<GameState>();
    }

    void Start()
    {
        enemySetup();
        healthUI = GameObject.FindGameObjectWithTag("GameController").GetComponent<StatsHolder>();
        healthUI.bossInfo.SetActive(true);
        healthUI.bossHeading.SetText("Alduous the Assassin");
        updateHealth();
        canShoot = false;
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
        if (dis >= 70f)
        {
            Vector3 direction = targetPosition - currentPos;
            direction.Normalize();
            transform.LookAt(targetPosition);
            rb.MovePosition(currentPos + (speed * Time.fixedDeltaTime * direction));
        }
        else
        {
            canShoot = true;
        }
    }
    public override void Attack()
    {
        enemyAnimator.Play("Attack");
        healthScript.setHealth(healthScript.getHealth() - (damage * ((400 - StatsHolder.defence) / 400)));
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
    public override void updateHealth()
    {
        if(health <= 0)
        {
            healthUI.bossInfo.SetActive(false);
            Destroy(gameObject);
        }
        float healthPercent = (float)health / maxHealth;
        Vector3 scale = new Vector3(healthPercent, 1, 1);
        healthUI.bossHealthBarTransform.localScale = scale;

        healthUI.bossHealthText.SetText(health.ToString() + " / " + maxHealth.ToString());
    }
}
