using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text helperText;
    public TMP_Text coinText;
    public TMP_Text xpText;
    public GameObject startMenu;
    public GameObject storyMenu;
    public Animator crossfadeTransition;
    private void Awake()
    {
        UpdateStats();
    }

    public void Update()
    {
        UpdateStats();
        if(GameState.wonLastGame)
        {
            StatsHolder.xp += (int) (StatsHolder.rewardsForEachLevel[SpawnEnemy.storyLevel]["xp"] * StatsHolder.xpMultiplier);
            StatsHolder.coins += (int) (StatsHolder.rewardsForEachLevel[SpawnEnemy.storyLevel]["coins"] * StatsHolder.coinMultiplier);
            GameState.wonLastGame = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.tag == "Story")
        {
            helperText.SetText("The story version of Protect The Star. Enemy waves are finite and gameplay is accompanied by a story progression.");
        } else if (gameObject.tag == "Infinite")
        {
            helperText.SetText("The infinite version of Protect The Star. Enemy waves are infinite and there is no story.");
        } else if (gameObject.tag == "Tutorial")
        {
            helperText.SetText("To learn how to play Protect The Star :)");
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject.tag == "Story")
        {
            helperText.SetText("");
        }
        else if (gameObject.tag == "Infinite")
        {
            helperText.SetText("");
        }
        else if (gameObject.tag == "Tutorial")
        {
            helperText.SetText("");
        }
    }
    private void UpdateStats()
    {
        coinText.SetText(StatsHolder.coins.ToString());
        xpText.SetText(StatsHolder.xp.ToString());
    }
    public void SwitchToStory()
    {
        StartCoroutine(StoryMenu());
    }
    IEnumerator StoryMenu()
    {
        crossfadeTransition.SetBool("End", false);
        crossfadeTransition.SetBool("Start", true);
        yield return new WaitForSeconds(1f);
        startMenu.SetActive(false);
        storyMenu.SetActive(true);
        crossfadeTransition.SetBool("End", true);
        crossfadeTransition.SetBool("Start", false);
        yield return new WaitForSeconds(1f);
    }
}
