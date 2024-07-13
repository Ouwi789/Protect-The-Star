using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text helperText;
    public Animator crossfadeTransition;
    public GameObject startMenu;
    public GameObject upgradesMenu;
    public GameObject towersMenu;
    public GameObject storyMenu;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.tag == "Upgrades")
        {
            helperText.SetText("Upgrades to help you Protect The Star :)");
        }
        else if (gameObject.tag == "Towers")
        {
            helperText.SetText("Tower Upgrades :)");
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(gameObject.tag != "Home")
        {
            helperText.SetText("");
        }
    }
    public void SwitchToUpgrades()
    {
        StartCoroutine(UpgradesMenu());
    }
    public void SwitchToTowers()
    {
        StartCoroutine(TowersMenu());
    }
    public void GoHome()
    {
        StartCoroutine(HomeMenu());
    }
    IEnumerator UpgradesMenu()
    {
        crossfadeTransition.SetBool("End", false);
        crossfadeTransition.SetBool("Start", true);
        yield return new WaitForSeconds(1f);
        startMenu.SetActive(false);
        upgradesMenu.SetActive(true);
        crossfadeTransition.SetBool("End", true);
        crossfadeTransition.SetBool("Start", false);
        yield return new WaitForSeconds(1f);
    }
    IEnumerator TowersMenu()
    {
        crossfadeTransition.SetBool("End", false);
        crossfadeTransition.SetBool("Start", true);
        yield return new WaitForSeconds(1f);
        startMenu.SetActive(false);
        towersMenu.SetActive(true);
        crossfadeTransition.SetBool("End", true);
        crossfadeTransition.SetBool("Start", false);
        yield return new WaitForSeconds(1f);
    }
    IEnumerator HomeMenu()
    {
        crossfadeTransition.SetBool("End", false);
        crossfadeTransition.SetBool("Start", true);
        yield return new WaitForSeconds(1f);
        startMenu.SetActive(true);
        storyMenu.SetActive(false);
        upgradesMenu.SetActive(false);
        // towersMenu.SetActive(false);
        crossfadeTransition.SetBool("End", true);
        crossfadeTransition.SetBool("Start", false);
        yield return new WaitForSeconds(1f);
    }
}
