using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameState : MonoBehaviour
{
    public GameObject healthbar;
    private RectTransform healthBarTransform;

    public TMP_Text healthText;

    [SerializeField] private int maxHealth;
    public int health;

    bool isPlaying = true;
    bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBarTransform = healthbar.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        if(health <= 0)
        {
            EndGame();
        }
    }
    void UpdateHealthBar()
    {
        float healthPercent = (float) health / maxHealth;
        Vector3 scale = new Vector3(healthPercent, 1, 1);
        healthBarTransform.localScale = scale;

        healthText.SetText(health.ToString() + " / " + maxHealth.ToString());
    }
    void EndGame()
    {
        //TODO end screen
        isPlaying = false;
        isDead = true;
        print("YOU DIED");
    }
    public void setHealth(int newHealth)
    {
        health = newHealth;
    }
    public int getHealth()
    {
        return health;
    }

}
