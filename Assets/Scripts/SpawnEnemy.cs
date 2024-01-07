using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject defaultEnemy;
    [SerializeField] private float timeToSpawn;
    private float enemyCounter = 0f;

    void Update()
    {
        if(enemyCounter >= timeToSpawn)
        {
            Vector3 spawnPlace = new Vector3(Random.Range(-100, 100), Random.Range(-100, 200));
            Instantiate(defaultEnemy, spawnPlace, Quaternion.identity);
            enemyCounter = 0f;
        }
        enemyCounter += Time.deltaTime;
    }
}
