using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryButton : MonoBehaviour
{
    public int storyLevel;
    public Animator crossfadeTransition;

    public void switchToStoryGame()
    {
        StartCoroutine(StoryMenu(storyLevel));
    }
    IEnumerator StoryMenu(int level)
    {

        crossfadeTransition.SetBool("Start", true);
        yield return new WaitForSeconds(1f);
        SpawnEnemy.storyLevel = level;
        SceneManager.LoadScene("Gameplay");
        crossfadeTransition.SetBool("End", true);
        crossfadeTransition.SetBool("Start", false);
    }
}
