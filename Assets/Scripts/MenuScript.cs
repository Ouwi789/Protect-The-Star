using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text helperText;
    public GameObject startMenu;
    public GameObject storyMenu;
    public Animator crossfadeTransition;

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
    public void SwitchToStory()
    {
        StartCoroutine(StoryMenu());
    }
    IEnumerator StoryMenu()
    {
        crossfadeTransition.SetBool("Start", true);
        yield return new WaitForSeconds(1f);
        startMenu.SetActive(false);
        storyMenu.SetActive(true);
        crossfadeTransition.SetBool("End", true);
        crossfadeTransition.SetBool("Start", false);
        yield return new WaitForSeconds(1);
        crossfadeTransition.SetBool("Start", false);
    }
}
