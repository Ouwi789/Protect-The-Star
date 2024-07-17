using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Image healthGreen;

    [SerializeField] private GameObject healthTextObject;
    private TMP_Text healthText;

    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
        healthText = healthTextObject.GetComponent<TMP_Text>();
    }

    public void updateHealthBar(float maxHealth, float health)
    {
        if(health / maxHealth >= 0)
        {
            healthGreen.rectTransform.localScale = new Vector2(health / maxHealth, 1);
        } else
        {
            healthGreen.rectTransform.localScale = new Vector2(0, 1);
        }
        healthText.SetText(health + "/" + maxHealth);
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position);
    }

}
