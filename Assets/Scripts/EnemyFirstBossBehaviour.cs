using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFirstBossBehaviour : EnemyBehaviour
{
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

    private StatsHolder healthUI;

    void Start()
    {
        enemySetup();
        healthUI = GameObject.FindGameObjectWithTag("GameController").GetComponent<StatsHolder>();
        healthUI.bossInfo.SetActive(true);
        healthUI.bossHeading.SetText("Prince Frederick III");
        updateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        updateHealth();
    }
    private void FixedUpdate()
    {
        moveEnemy();
    }
    private void OnTriggerEnter(Collider other)
    {
        onHit(other);
    }
    public override void onHit(Collider other)
    {
        if (other.tag == "Building" || other.tag == "Sun")
        {
            Attack();
            Destroy(gameObject);
        }
    }
    public override void updateHealth()
    {
        if (health <= 0)
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
