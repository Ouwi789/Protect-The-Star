using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody rb;

    private GameObject gameController;
    private GameState healthScript;

    private GameObject sun;
    private Vector3 targetPosition;

    private HealthBar healthCanvas;
    
    [SerializeField] private int maxHealth;
    private int health;
    [SerializeField] private float speed;
    // Start is called before the first frame update

    private void Awake()
    {
        health = maxHealth;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        sun = GameObject.FindGameObjectWithTag("Sun");
        targetPosition = sun.transform.position;
        healthScript = gameController.GetComponent<GameState>();
        healthCanvas = gameObject.GetComponentInChildren<HealthBar>();
        healthCanvas.updateHealthBar(maxHealth, health);
    }

    private void Update()
    {
        healthCanvas.updateHealthBar(maxHealth, health);
    }

    private void FixedUpdate()
    {
        Vector3 currentPos = transform.position;
        Vector3 direction = targetPosition - currentPos;
        direction.Normalize();
        transform.LookAt(targetPosition);
        rb.MovePosition(currentPos + (speed * Time.fixedDeltaTime * direction));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Building" || other.tag == "Sun")
        {
            healthScript.setHealth(healthScript.getHealth() - health);
            Destroy(gameObject);
        } else if (other.tag == "Bullet")
        {
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            health -= other.GetComponent<BulletScript>().damage;
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public Transform getPosition()
    {
        return transform;
    }
}
