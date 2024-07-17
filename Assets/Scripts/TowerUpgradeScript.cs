using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TowerUpgradeScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject infoMenu;
    TMP_Text infoHeading;
    TMP_Text infoDescription;
    TMP_Text lockedText;

    [SerializeField] private int cost; // in coins

    [SerializeField] private string towerName;
    [SerializeField] private int upgradeState;

    // Start is called before the first frame update
    private void Awake()
    {
        if (!StatsHolder.boughtOrNotTowers.ContainsKey(towerName + " " + upgradeState))
        {
            if(upgradeState == 1)
            {
                StatsHolder.boughtOrNotTowers.Add(towerName + " " + upgradeState, true);
            } else
            {
                string temp = towerName + " " + upgradeState;
                StatsHolder.boughtOrNotTowers.Add(temp, false);
            }
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        infoMenu = GameObject.FindGameObjectWithTag("TowerInfo");
        infoHeading = GameObject.FindGameObjectWithTag("TN").GetComponent<TMP_Text>();
        infoDescription = GameObject.FindGameObjectWithTag("TC").GetComponent<TMP_Text>();
        lockedText = GameObject.FindGameObjectWithTag("LT").GetComponent<TMP_Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoMenu.SetActive(true);
        infoHeading.SetText(towerName);
        infoDescription.SetText(cost + " Coins");
        if (!StatsHolder.boughtOrNotTowers[towerName + " " + upgradeState])
        {
            lockedText.SetText("Locked");
        }
        else
        {
            lockedText.SetText("Unlocked");
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        infoMenu.SetActive(false);
    }
    private bool parentPurchased()
    {
        if(upgradeState == 1)
        {
            return false;
        }
        return StatsHolder.boughtOrNotTowers[towerName + " " + (upgradeState-1)];
    }

    public void onPurchaseClick()
    {
        if(parentPurchased() && StatsHolder.coins >= cost && !StatsHolder.boughtOrNotTowers[towerName + " " + upgradeState])
        {
            StatsHolder.coins -= cost;
            StatsHolder.boughtOrNotTowers[towerName + " " + upgradeState] = true;
            StartCoroutine(Purchase());
        } else
        {
            StartCoroutine(CannotPurchase());
        }
    }
    IEnumerator Purchase()
    {
        string temp = infoHeading.text;
        infoHeading.SetText("Purchased!");
        yield return new WaitForSeconds(3f);
        infoHeading.SetText(temp);
    }
    IEnumerator CannotPurchase()
    {
        string temp = infoHeading.text;
        infoHeading.SetText("Lol Too Broke!");
        string temp2 = infoDescription.text;
        infoDescription.SetText("Or Already Purchased!");
        string temp3 = lockedText.text;
        lockedText.SetText("Or Previous One Still Locked!");
        yield return new WaitForSeconds(3f);
        infoHeading.SetText(temp);
        infoDescription.SetText(temp2);
        lockedText.SetText(temp3);
    }
}
