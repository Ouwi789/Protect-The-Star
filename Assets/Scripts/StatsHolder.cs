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
