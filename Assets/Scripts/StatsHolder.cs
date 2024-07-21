using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsHolder : MonoBehaviour
{
    public TMP_Text hydrogenCount;
    public TMP_Text heliumCount;
    public int hydrogenAmount = 20;
    public int heliumAmount = 20;

    //boss healthbar
    public GameObject bossInfo;
    public TMP_Text bossHeading;
    public TMP_Text bossHealthText;
    public RectTransform bossHealthBarTransform;

    //Tower Objects
    public GameObject heliumTurret1;
    public GameObject heliumTurret2;
    public GameObject heliumTurret3;
    public GameObject heliumTurret4;
    public GameObject heliumTurret5;

    public GameObject hydrogenTurret1;
    public GameObject hydrogenTurret2;
    public GameObject hydrogenTurret3;
    public GameObject hydrogenTurret4;
    public GameObject hydrogenTurret5;

    public GameObject heliumGen1;
    public GameObject hydroGen1;
    public GameObject hydroPlat1;
    public GameObject heliumPlat1;

    //Items
    public static int xp = 0;
    public static int coins = 0;

    //Upgrades
    public static float attackMultiplier = 1;
    public static float attackRangedMultiplier = 1;
    public static float attackMeleeMultiplier = 1;

    public static int health = 100;
    public static int regen = 0;
    public static float defence = 0;

    public static float coinMultiplier = 1;
    public static float xpMultiplier = 1;

    public static int hydrogenGain = 0;
    public static int heliumGain = 0;

    public static float hydrogenDiscount = 1;
    public static float heliumDiscount = 1;

    //Tower and Reward Mapping
    public Dictionary<string, Dictionary<string, object>> buidlings = new();
    public static Dictionary<int, Dictionary<string, int>> rewardsForEachLevel;
    public static Dictionary<string, bool> boughtOrNotUpgrades;
    public static Dictionary<string, bool> boughtOrNotTowers;
    public static Dictionary<int, bool> levelCompleted;

    private void Awake()
    {
        if(boughtOrNotUpgrades == null)
        {
            boughtOrNotUpgrades = new();
        }
        if(boughtOrNotTowers == null)
        {
            boughtOrNotTowers = new();
        }
        if(levelCompleted == null)
        {
            levelCompleted = new();
        }
        if(rewardsForEachLevel == null)
        {
            rewardsForEachLevel = new();
            Dictionary<string, int> tempLevel = new()
            {
                ["xp"] = 5,
                ["coins"] = 5
            };
            //infinte not included here
            rewardsForEachLevel.Add(1, tempLevel);
            //TODO add appropriate rewards to each story level so that the game is balanced
            tempLevel = new()
            {
                ["xp"] = 10,
                ["coins"] = 10
            };
            rewardsForEachLevel.Add(2, tempLevel);
            tempLevel = new()
            {
                ["xp"] = 15,
                ["coins"] = 15
            };
            rewardsForEachLevel.Add(3, tempLevel);
            tempLevel = new()
            {
                ["xp"] = 20,
                ["coins"] = 20
            };
            rewardsForEachLevel.Add(4, tempLevel);
            tempLevel = new()
            {
                ["xp"] = 30,
                ["coins"] = 30
            };
            rewardsForEachLevel.Add(5, tempLevel);
            tempLevel = new()
            {
                ["xp"] = 40,
                ["coins"] = 40
            };
            rewardsForEachLevel.Add(6, tempLevel);
            tempLevel = new()
            {
                ["xp"] = 60,
                ["coins"] = 60
            };
            rewardsForEachLevel.Add(7, tempLevel);
            tempLevel = new()
            {
                ["xp"] = 70,
                ["coins"] = 70
            };
            rewardsForEachLevel.Add(8, tempLevel);
            tempLevel = new()
            {
                ["xp"] = 85,
                ["coins"] = 85
            };
            rewardsForEachLevel.Add(9, tempLevel);
            tempLevel = new()
            {
                ["xp"] = 100,
                ["coins"] = 100
            };
            rewardsForEachLevel.Add(10, tempLevel);
        }


        //damage is also production and generation for generators
        //helium turret
        Dictionary<string, object> stats = new()
        {
            ["damage"] = 8,
            ["range"] = 35f,
            ["cooldown"] = 4f,
            ["cost"] = 5,
            ["object"] = heliumTurret1,
            ["currency"] = "he",
            ["upgrade"] = "Helium Turret 2"
        };
        buidlings.Add("Helium Turret", stats);
        //helium turret 2
        stats = new()
        {
            ["damage"] = 16,
            ["range"] = 40f,
            ["cooldown"] = 3f,
            ["cost"] = 10,
            ["object"] = heliumTurret2,
            ["currency"] = "he",
            ["upgrade"] = "Helium Turret 3"
        };
        buidlings.Add("Helium Turret 2", stats);
        //helium turret 3
        stats = new()
        {
            ["damage"] = 48,
            ["range"] = 45f,
            ["cooldown"] = 2f,
            ["cost"] = 20,
            ["object"] = heliumTurret3,
            ["currency"] = "he",
            ["upgrade"] = "Helium Turret 4"
        };
        buidlings.Add("Helium Turret 3", stats);
        //helium turret 4
        stats = new()
        {
            ["damage"] = 64,
            ["range"] = 70f,
            ["cooldown"] = 1.5f,
            ["cost"] = 40,
            ["object"] = heliumTurret4,
            ["currency"] = "he",
            ["upgrade"] = "Helium Turret 5"
        };
        buidlings.Add("Helium Turret 4", stats);
        //final helium turret
        stats = new()
        {
            ["damage"] = 80,
            ["range"] = 100f,
            ["cooldown"] = 1f,
            ["cost"] = 80,
            ["object"] = heliumTurret5,
            ["currency"] = "he",
            ["upgrade"] = "MAX"
        };
        buidlings.Add("Helium Turret 5", stats);
        //hydrogen turret
        stats = new()
        {
            ["damage"] = 16,
            ["range"] = 60f,
            ["cooldown"] = 6f,
            ["cost"] = 6,
            ["object"] = hydrogenTurret1,
            ["currency"] = "h",
            ["upgrade"] = "Hydrogen Turret 2"
        };
        buidlings.Add("Hydrogen Turret", stats);
        //hydrogen turret 2
        stats = new()
        {
            ["damage"] = 48,
            ["range"] = 70f,
            ["cooldown"] = 5.5f,
            ["cost"] = 20,
            ["object"] = hydrogenTurret2,
            ["currency"] = "h",
            ["upgrade"] = "Hydrogen Turret 3"
        };
        buidlings.Add("Hydrogen Turret 2", stats);
        //hydrogen turret 3
        stats = new()
        {
            ["damage"] = 96,
            ["range"] = 90f,
            ["cooldown"] = 5f,
            ["cost"] = 30,
            ["object"] = hydrogenTurret3,
            ["currency"] = "h",
            ["upgrade"] = "Hydrogen Turret 4"
        };
        buidlings.Add("Hydrogen Turret 3", stats);
        //hydrogen turret 4
        stats = new()
        {
            ["damage"] = 192,
            ["range"] = 110f,
            ["cooldown"] = 4.5f,
            ["cost"] = 50,
            ["object"] = hydrogenTurret4,
            ["currency"] = "h",
            ["upgrade"] = "Hydrogen Turret 5"
        };
        buidlings.Add("Hydrogen Turret 4", stats);
        //final hydrogen turret
        stats = new()
        {
            ["damage"] = 400,
            ["range"] = 150f,
            ["cooldown"] = 4f,
            ["cost"] = 100,
            ["object"] = hydrogenTurret5,
            ["currency"] = "h",
            ["upgrade"] = "MAX"
        };
        buidlings.Add("Hydrogen Turret 5", stats);
        //hydrogen
        stats = new()
        {
            ["damage"] = 1,
            ["range"] = 5f,
            ["cooldown"] = 2f,
            ["cost"] = 10,
            ["object"] = hydroGen1,
            ["currency"] = "h",
            ["upgrade"] = "MAX"
        };
        buidlings.Add("Hydrogen Generator", stats);
        //helium
        stats = new()
        {
            ["damage"] = 1,
            ["range"] = 5f,
            ["cooldown"] = 2f,
            ["cost"] = 10,
            ["object"] = heliumGen1,
            ["currency"] = "he",
            ["upgrade"] = "MAX"
        };
        buidlings.Add("Helium Generator", stats);
        //hydro Platform
        stats = new()
        {
            ["damage"] = 0,
            ["range"] = 0,
            ["cooldown"] = 0,
            ["cost"] = 5,
            ["object"] = hydroPlat1,
            ["currency"] = "h",
            ["upgrade"] = "MAX"
        };
        buidlings.Add("Hydrogen Platform", stats);
        //helium Platform
        stats = new()
        {
            ["damage"] = 0,
            ["range"] = 0,
            ["cooldown"] = 0,
            ["cost"] = 5,
            ["object"] = heliumPlat1,
            ["currency"] = "he",
            ["upgrade"] = "MAX"
        };
        buidlings.Add("Helium Platform", stats);
         
    }

    private void Update()
    {
        if(hydrogenCount != null && heliumCount != null)
        {
            hydrogenCount.SetText(hydrogenAmount.ToString());
            heliumCount.SetText(heliumAmount.ToString());
        }
    }
    public int getHydrogen()
    {
        return hydrogenAmount;
    }
    public void setHydrogen(int amount)
    {
        hydrogenAmount = amount;
    }
    public int getHelium()
    {
        return heliumAmount;
    }
    public void setHelium(int amount)
    {
        heliumAmount = amount;
    }
    public int getCost(string building)
    {
        return (int)buidlings[building]["cost"];
    }
}
