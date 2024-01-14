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


    public Dictionary<string, Dictionary<string, object>> buidlings = new();

    private void Awake()
    {
        //damage is also production and generation for generators
        //helium turret
        Dictionary<string, object> stats = new()
        {
            ["damage"] = 5,
            ["range"] = 15f,
            ["cooldown"] = 5f
        };
        buidlings.Add("Helium Turret", stats);
        //hydrogen turret
        stats = new()
        {
            ["damage"] = 10,
            ["range"] = 30f,
            ["cooldown"] = 8f
        };
        buidlings.Add("Hydrogen Turret", stats);
        //hydrogen
        stats = new()
        {
            ["damage"] = 2,
            ["range"] = 5f,
            ["cooldown"] = 5f
        };
        buidlings.Add("Hydrogen Generator", stats);
        //helium
        stats = new()
        {
            ["damage"] = 2,
            ["range"] = 5f,
            ["cooldown"] = 5f
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
}
