using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyThirdBossBehaviour : EnemyBehaviour
{
    //necromancer boss
    [SerializeField] GameObject UFO;
    [SerializeField] GameObject Satellite;
    [SerializeField] GameObject Kamikaze;
    [SerializeField] string bossName;
    private StatsHolder healthUI;


    private float spawnTime = 3f;
    private float counter = 0f;

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
        healthUI.bossHeading.SetText(bossName);
        updateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        updateHealth();
        if(counter >= spawnTime)
        {
            spawnEnemy();
            counter = 0f;
        }
        counter += Time.deltaTime;
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
    private void spawnEnemy()
    {
        int temp = Random.Range(1, 4);
        switch (temp)
        {
            case 1:
                Instantiate(UFO, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(Satellite, transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(Kamikaze, transform.position, Quaternion.identity);
                break;
        }
    }
    public override void updateHealth()
    {
         float healthPercent = (float)health / maxHealth;
         Vector3 scale = new Vector3(healthPercent, 1, 1);
         healthUI.bossHealthBarTransform.localScale = scale;

         healthUI.bossHealthText.SetText(health.ToString() + " / " + maxHealth.ToString());
    }
}
