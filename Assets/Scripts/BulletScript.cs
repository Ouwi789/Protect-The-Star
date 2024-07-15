using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private GameObject[] enemies;
    private GameObject enemyTarget;
    private Transform enemyPos;
    private Rigidbody rb;
    private float lifetime = 5f;
    private float counter = 0f;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        FindEnemy();
        if (enemyTarget == null)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (enemyTarget == null)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        if (enemyTarget == null)
        {
            Destroy(gameObject);
        }
        if(enemyTarget != null)
        {
            enemyPos = enemyTarget.GetComponent<EnemyBehaviour>().getPosition();
            Vector3 currPos = transform.position;
            if (Vector3.Distance(currPos, enemyPos.position) <= 0.5f)
            {
                Destroy(gameObject);
            }
            Vector3 direction = enemyPos.position - currPos;
            direction.Normalize();
            rb.MovePosition(currPos + (speed * Time.fixedDeltaTime * direction));
            counter += Time.deltaTime;
            if (counter > lifetime)
            {
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
    public void FindEnemy()
    {
        float temp = float.MaxValue;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (temp > distance)
            {
                temp = distance;
                enemyTarget = enemy;
                enemyPos = enemyTarget.GetComponent<EnemyBehaviour>().getPosition();
            }
        }
    }
}
