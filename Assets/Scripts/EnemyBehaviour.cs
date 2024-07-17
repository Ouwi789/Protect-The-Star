using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyBehaviour : MonoBehaviour
{
    protected Rigidbody rb;

    protected GameObject gameController;
    protected GameState healthScript;

    protected GameObject sun;
    protected Vector3 targetPosition;

    protected HealthBar healthCanvas;

    
    [SerializeField] protected int maxHealth;
    public int health;
    [SerializeField] protected float speed;
    // Start is called before the first frame update

    private void Awake()
    {
        health = maxHealth;
    }

    public virtual void enemySetup()
    {
        print(maxHealth);
        health = maxHealth;
        rb = GetComponent<Rigidbody>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        sun = GameObject.FindGameObjectWithTag("Sun");
        targetPosition = sun.transform.position;
        healthScript = gameController.GetComponent<GameState>();
        healthCanvas = gameObject.GetComponentInChildren<HealthBar>();
    }


    public virtual void updateHealth()
    {
        if (healthCanvas)
        {
            healthCanvas.updateHealthBar(maxHealth, health);
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    //default movement is kamikaze
    public virtual void moveEnemy()
    {
        Vector3 currentPos = transform.position;
        Vector3 direction = targetPosition - currentPos;
        direction.Normalize();
        transform.LookAt(targetPosition);
        rb.MovePosition(currentPos + (speed * Time.fixedDeltaTime * direction));
    }

    public virtual void onHit(Collider other)
    {
        if (other.tag == "Building" || other.tag == "Sun")
        {
            Destroy(gameObject);
        }
    }

    public virtual void setHealth(int health)
    {
        this.health = health;
    }

    public virtual Transform getPosition()
    {
        return transform;
    }
    //defual attack is kamikaze
    public virtual void Attack()
    {
        healthScript.setHealth(healthScript.getHealth() - (health * ((400 - StatsHolder.defence)/400)));
    }
}
