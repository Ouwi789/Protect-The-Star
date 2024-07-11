using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuicideBehaviour : EnemyBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        enemySetup();
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
}

