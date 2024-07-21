using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillTreeScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject infoMenu;
    TMP_Text infoHeading;
    TMP_Text infoDescription;
    TMP_Text lockedText;

    [SerializeField] Sprite unlockedVer;
    [SerializeField] int tier;
    [SerializeField] string type;
    [SerializeField] string description;
    [SerializeField] int cost; //upgrades (not tower ones) are always in XP

    void Start()
    {

        if (!StatsHolder.boughtOrNotUpgrades.ContainsKey(type + " " + tier))
        {
            StatsHolder.boughtOrNotUpgrades.Add(type + " " + tier, false);
        } else if (StatsHolder.boughtOrNotUpgrades[type + " " + tier])
        {
            GetComponent<Image>().sprite = unlockedVer;
        }

        infoMenu = GameObject.FindGameObjectWithTag("Info");
        infoHeading = GameObject.FindGameObjectWithTag("InfoH").GetComponent<TMP_Text>();
        infoDescription = GameObject.FindGameObjectWithTag("InfoD").GetComponent<TMP_Text>();
        lockedText = GameObject.FindGameObjectWithTag("InfoL").GetComponent<TMP_Text>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        infoMenu.SetActive(true);
        string temptier = "";
        for(int i = 0; i < tier; i++)
        {
            temptier += "I";
        }
        infoHeading.SetText(type + " " + temptier);
        infoDescription.SetText(description);
        if(StatsHolder.boughtOrNotUpgrades[type + " " + tier])
        {
            lockedText.SetText("(Unlocked)");
        } else
        {
            lockedText.SetText("(Locked, " + cost + "XP)");
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        infoMenu.SetActive(false);
    }
    private bool IfParentPurchased()
    {
        if (type.Equals("Base"))
        {
            return true;
        }
        if(tier == 1)
        {
            if (type.Equals("Helium") || type.Equals("Hydrogen"))
            {
                return StatsHolder.boughtOrNotUpgrades["Farming 2"];
            }
            else if (type.Equals("Melee") || type.Equals("Ranged"))
            {
                return StatsHolder.boughtOrNotUpgrades["Damage 2"];
            }
            else if (type.Equals("XP") || type.Equals("Coins"))
            {
                return StatsHolder.boughtOrNotUpgrades["Rewards 3"];
            }
            else if (type.Equals("Regeneration") || type.Equals("Defence"))
            {
                return StatsHolder.boughtOrNotUpgrades["Health 3"];
            } else if(type.Equals("Damage") || type.Equals("Farming") || type.Equals("Rewards") || type.Equals("Health"))
            {
                return StatsHolder.boughtOrNotUpgrades["Base 0"];
            }
        } else
        {
            int temp = tier - 1;
            return StatsHolder.boughtOrNotUpgrades[type + " " + temp];
        }
        return false;
    }
    public void PurchaseUpgrade()
    {
        if(StatsHolder.xp >= cost && !StatsHolder.boughtOrNotUpgrades[type + " " + tier] && IfParentPurchased())
        {
            StatsHolder.xp -= cost;
            StatsHolder.boughtOrNotUpgrades[type + " " + tier] = true;
            GetComponent<Image>().sprite = unlockedVer;
            StartCoroutine(Purchase());
            switch (type)
            {
                case "Base":
                    break;
                case "Damage":
                    if(tier == 1)
                    {
                        StatsHolder.attackMultiplier = 1.1f;
                    } else if (tier == 2)
                    {
                        StatsHolder.attackMultiplier = 1.3f;
                    }
                    break;
                case "Melee":
                    if (tier == 1)
                    {
                        StatsHolder.attackMeleeMultiplier = 1.2f;
                    }
                    else if (tier == 2)
                    {
                        StatsHolder.attackMeleeMultiplier = 1.5f;
                    }
                    break;
                case "Ranged":
                    if (tier == 1)
                    {
                        StatsHolder.attackRangedMultiplier = 1.2f;
                    }
                    else if (tier == 2)
                    {
                        StatsHolder.attackRangedMultiplier = 1.5f;
                    }
                    break;
                case "Farming": //H & He per wave
                    if (tier == 1)
                    {
                        StatsHolder.hydrogenGain = 3;
                        StatsHolder.heliumGain = 3;
                    }
                    else if (tier == 2)
                    {
                        StatsHolder.hydrogenGain = 7;
                        StatsHolder.heliumGain = 7;
                    }
                    break;
                case "Hydrogen":
                    if (tier == 1)
                    {
                        StatsHolder.hydrogenDiscount = 0.8f;
                    }
                    else if (tier == 2)
                    {
                        StatsHolder.hydrogenDiscount = 0.6f;
                    }
                    break;
                case "Helium":
                    if (tier == 1)
                    {
                        StatsHolder.heliumDiscount = 0.8f;
                    }
                    else if (tier == 2)
                    {
                        StatsHolder.heliumDiscount = 0.6f;
                    }
                    break;
                case "Rewards": //xp & coins
                    if (tier == 1)
                    {
                        StatsHolder.coinMultiplier = 1.2f;
                        StatsHolder.xpMultiplier = 1.2f;
                    }
                    else if (tier == 2)
                    {
                        StatsHolder.coinMultiplier = 1.4f;
                        StatsHolder.xpMultiplier = 1.4f;
                    } else if (tier == 3)
                    {
                        StatsHolder.coinMultiplier = 1.6f;
                        StatsHolder.xpMultiplier = 1.6f;
                    }
                    break;
                case "XP":
                    if (tier == 1)
                    {
                        StatsHolder.xpMultiplier = 1.8f;
                    }
                    else if (tier == 2)
                    {
                        StatsHolder.xpMultiplier = 2f;
                    }
                    else if (tier == 3)
                    {
                        StatsHolder.xpMultiplier = 2.2f;
                    }
                    break;
                case "Coins":
                    if (tier == 1)
                    {
                        StatsHolder.coinMultiplier = 1.8f;
                    }
                    else if (tier == 2)
                    {
                        StatsHolder.coinMultiplier = 2f;
                    }
                    else if (tier == 3)
                    {
                        StatsHolder.coinMultiplier = 2.2f;
                    }
                    break;
                case "Health":
                    if (tier == 1)
                    {
                        StatsHolder.health = 120;
                    }
                    else if (tier == 2)
                    {
                        StatsHolder.health = 150;
                    }
                    else if (tier == 3)
                    {
                        StatsHolder.health = 200;
                    }
                    break;
                case "Regeneration":
                    if (tier == 1)
                    {
                        StatsHolder.regen = 10;
                    }
                    else if (tier == 2)
                    {
                        StatsHolder.regen = 25;
                    }
                    else if (tier == 3)
                    {
                        StatsHolder.regen = 50;
                    }
                    break;
                case "Defence":
                    if (tier == 1)
                    {
                        StatsHolder.defence = 20;
                    }
                    else if (tier == 2)
                    {
                        StatsHolder.defence = 40;
                    }
                    else if (tier == 3)
                    {
                        StatsHolder.defence = 100;
                    }
                    break;
            }
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
