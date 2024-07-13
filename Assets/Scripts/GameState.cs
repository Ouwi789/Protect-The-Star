using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public GameObject healthbar;
    public GameObject win;
    public GameObject lose;
    public static bool wonLastGame;
    private RectTransform healthBarTransform;
    public Animator crossfadeTransition;

    public TMP_Text healthText;

    private float maxHealth;
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = StatsHolder.health;
        health = maxHealth;
        healthBarTransform = healthbar.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        if(health <= 0)
        {
            StartCoroutine(EndGame());
        }
    }
    void UpdateHealthBar()
    {
        float healthPercent = (float) health / maxHealth;
        Vector3 scale = new Vector3(healthPercent, 1, 1);
        healthBarTransform.localScale = scale;

        healthText.SetText(health.ToString() + " / " + maxHealth.ToString());
    }
    public IEnumerator EndGame()
    {
        //TODO end screen
        lose.SetActive(true);
        wonLastGame = false;
        yield return new WaitForSeconds(3f);
        lose.SetActive(false);
        crossfadeTransition.SetBool("Start", true);
        SceneManager.LoadScene("StartMenu");
        yield return new WaitForSeconds(1f);
        crossfadeTransition.SetBool("End", true);
        crossfadeTransition.SetBool("Start", false);
        yield return null;
    }
    public void setHealth(float newHealth)
    {
        health = newHealth;
    }
    public float getHealth()
    {
        return health;
    }
    public IEnumerator WinGame()
    {
        //TODO end screen
        win.SetActive(true);
        wonLastGame = true;
        yield return new WaitForSeconds(3f);
        win.SetActive(false);
        crossfadeTransition.SetBool("Start", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("StartMenu");
        crossfadeTransition.SetBool("End", true);
        crossfadeTransition.SetBool("Start", false);
        yield return null;
    }

}
