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

    //Tower Objects
    public GameObject heliumTurret1;
    public GameObject heliumTurret2;
    public GameObject hydrogenTurret1;
    public GameObject heliumGen1;
    public GameObject hydroGen1;
    public GameObject hydroPlat1;
    public GameObject heliumPlat1;

    //Items
    public static int xp = 200;
    public static int coins = 200;

    //Upgrades
    public static float attackMultiplier = 1;
    public static float attackRangedMultiplier = 1;
    public static float attackMeleeMultiplier = 1;

    public static int health = 100;
    public static int regen = 0;
    public static int defence = 0;

    public static float coinMultiplier = 1;
    public static float xpMultiplier = 1;

    public static int hydrogenGain = 0;
    public static int heliumGain = 0;

    public static int hydrogenDiscount = 0;
    public static int heliumDiscount = 0;

    //Tower and Reward Mapping
    public Dictionary<string, Dictionary<string, object>> buidlings = new();
    public static Dictionary<int, Dictionary<string, int>> rewardsForEachLevel = new();
    private static bool rewardsMapped = false;

    private void Awake()
    {
        //damage is also production and generation for generators
        //helium turret
        Dictionary<string, object> stats = new()
        {
            ["damage"] = 5,
            ["range"] = 35f,
            ["cooldown"] = 5f,
            ["cost"] = 5,
            ["object"] = heliumTurret1,
            ["currency"] = "he",
            ["upgrade"] = "Helium Turret 2"
        };
        buidlings.Add("Helium Turret", stats);
        //helium turret 2
        stats = new()
        {
            ["damage"] = 8,
            ["range"] = 40f,
            ["cooldown"] = 3.5f,
            ["cost"] = 10,
            ["object"] = heliumTurret2,
            ["currency"] = "he",
            ["upgrade"] = "MAX" //wont' be final evolution, just for testing (same with other towers)
        };
        buidlings.Add("Helium Turret 2", stats);
        //hydrogen turret
        stats = new()
        {
            ["damage"] = 10,
            ["range"] = 50f,
            ["cooldown"] = 8f,
            ["cost"] = 6,
            ["object"] = hydrogenTurret1,
            ["currency"] = "h",
            ["upgrade"] = "MAX"
        };
        buidlings.Add("Hydrogen Turret", stats);
        //hydrogen
        stats = new()
        {
            ["damage"] = 2,
            ["range"] = 5f,
            ["cooldown"] = 5f,
            ["cost"] = 5,
            ["object"] = hydroGen1,
            ["currency"] = "h",
            ["upgrade"] = "MAX"
        };
        buidlings.Add("Hydrogen Generator", stats);
        //helium
        stats = new()
        {
            ["damage"] = 2,
            ["range"] = 5f,
            ["cooldown"] = 5f,
            ["cost"] = 5,
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
        Dictionary<string, int> tempLevel = new()
        {
            ["xp"] = 5,
            ["coins"] = 5
        };
        if(!rewardsMapped)
        {
            //-1 is tutorial and 0 is infinite
            rewardsForEachLevel.Add(-1, tempLevel);
            rewardsForEachLevel.Add(0, tempLevel);
            rewardsForEachLevel.Add(1, tempLevel);
            //TODO add appropriate rewards to each story level so that the game is balanced
            rewardsForEachLevel.Add(2, tempLevel);
            rewardsForEachLevel.Add(3, tempLevel);
            rewardsForEachLevel.Add(4, tempLevel);
            rewardsForEachLevel.Add(5, tempLevel);
            rewardsForEachLevel.Add(6, tempLevel);
            rewardsForEachLevel.Add(7, tempLevel);
            rewardsForEachLevel.Add(8, tempLevel);
            rewardsForEachLevel.Add(9, tempLevel);
            rewardsForEachLevel.Add(10, tempLevel);
            rewardsMapped = true;
        }
    }

    private void Update()
    {
        hydrogenCount.SetText(hydrogenAmount.ToString());
        heliumCount.SetText(heliumAmount.ToString());
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
