using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsHolder : MonoBehaviour
{
    public TMP_Text hydrogenCount;
    public TMP_Text heliumCount;
    public int hydrogenAmount = 0;
    public int heliumAmount = 0;

    private void Update()
    {
        hydrogenCount.SetText(hydrogenAmount.ToString());
        heliumCount.SetText(heliumAmount.ToString());
    }
}
