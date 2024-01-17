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


    public Dictionary<string, Dictionary<string, object>> buidlings = new();

    private void Awake()
    {
        //damage is also production and generation for generators
        //helium turret
        Dictionary<string, object> stats = new()
        {
            ["damage"] = 5,
            ["range"] = 15f,
            ["cooldown"] = 5f,
            ["cost"] = 5,
            ["object"] = heliumTurret1,
            ["upgrade"] = "Helium Turret 2"
        };
        buidlings.Add("Helium Turret", stats);
        //helium turret 2
        stats = new()
        {
            ["damage"] = 8,
            ["range"] = 20f,
            ["cooldown"] = 3.5f,
            ["cost"] = 10,
            ["object"] = heliumTurret2,
            ["upgrade"] = "MAX" //wont' be final evolution, just for testing
        };
        buidlings.Add("Helium Turret 2", stats);
        //hydrogen turret
        stats = new()
        {
            ["damage"] = 10,
            ["range"] = 30f,
            ["cooldown"] = 8f,
            ["cost"] = 6,
            ["object"] = hydrogenTurret1,
            ["upgrade"] = "Hydrogen Turret 2"
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
            ["upgrade"] = "Hydrogen Generator 2"
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
            ["upgrade"] = "Helium Generator 2"
        };
        buidlings.Add("Helium Generator", stats);
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
