using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StoryButton : MonoBehaviour
{
    public int storyLevel;
    public Animator crossfadeTransition;

    [SerializeField] private GameObject locked;

    private void Start()
    {
        if (!(storyLevel == 0) && !StatsHolder.levelCompleted.ContainsKey(storyLevel))
        {
            StatsHolder.levelCompleted.Add(storyLevel, false);
        }
    }
    public void switchToStoryGame()
    {
        if(storyLevel == 1 || storyLevel == 0 || StatsHolder.levelCompleted[storyLevel - 1])
        {
            StartCoroutine(StoryMenu(storyLevel));
        } else
        {
            StartCoroutine(Deny());
        }

        
    }
    IEnumerator StoryMenu(int level)
    {

        crossfadeTransition.SetBool("Start", true);
        yield return new WaitForSeconds(1f);
        SpawnEnemy.storyLevel = level;
        SceneManager.LoadScene("Gameplay");
        crossfadeTransition.SetBool("End", true);
    }
    IEnumerator Deny()
    {
        locked.SetActive(true);
        locked.GetComponent<TMP_Text>().SetText("Please complete Level " + (storyLevel - 1) + " before attempting this level!");
        yield return new WaitForSeconds(2f);
        locked.SetActive(false);
        yield return null;
    }
}
