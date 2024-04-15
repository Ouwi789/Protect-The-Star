using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Image healthGreen;

    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    public void updateHealthBar(float maxHealth, float health)
    {
        healthGreen.rectTransform.localScale = new Vector2(health / maxHealth, 1);
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position);
    }

}
