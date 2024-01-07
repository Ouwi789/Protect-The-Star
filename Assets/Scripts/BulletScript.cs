using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private GameObject[] enemies;
    private GameObject enemyTarget;
    private Transform enemyPos;
    private Rigidbody rb;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        FindEnemy();
    }
    private void Update()
    {
        if (enemyTarget == null)
        {
            FindEnemy();
        }
    }
    private void FixedUpdate()
    {
        enemyPos = enemyTarget.GetComponent<EnemyMovement>().getPosition();
        Vector3 currPos = transform.position;
        Vector3 direction = enemyPos.position - currPos;
        direction.Normalize();
        rb.MovePosition(currPos + (speed * Time.fixedDeltaTime * direction));
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
                enemyPos = enemyTarget.GetComponent<EnemyMovement>().getPosition();
            }
        }
    }
}
