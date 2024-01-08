using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody rb;

    private GameObject gameController;
    private GameState healthScript;

    private GameObject sun;
    private Vector3 targetPosition;

    [SerializeField] private int damage;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        sun = GameObject.FindGameObjectWithTag("Sun");
        targetPosition = sun.transform.position;
        healthScript = gameController.GetComponent<GameState>();
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
            healthScript.setHealth(healthScript.getHealth() - damage);
            Destroy(gameObject);
        } else if (other.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
    public Transform getPosition()
    {
        return transform;
    }
}
