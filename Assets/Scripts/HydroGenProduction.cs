using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydroGenProduction : MonoBehaviour
{
    private float counter = 0f;
    private GameObject gameController;
    private StatsHolder script;
    private bool isFake;

    private StatsHolder stats;
    private int hydrogenGenerated;
    private float range;
    private float generateTime;

    private void Awake()
    {
        stats = GameObject.FindGameObjectWithTag("GameController").GetComponent<StatsHolder>();
        hydrogenGenerated = (int)stats.buidlings["Hydrogen Generator"]["damage"];
        generateTime = (float)stats.buidlings["Hydrogen Generator"]["cooldown"];
        range = (float)stats.buidlings["Hydrogen Generator"]["range"];
    }

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        script = gameController.GetComponent<StatsHolder>();
        if(gameObject.tag == "Fake")
        {
            isFake = true;
        } else
        {
            isFake = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFake)
        {
            counter += Time.deltaTime;
            if (counter >= generateTime)
            {
                script.setHydrogen(script.getHydrogen() + hydrogenGenerated);
                counter = 0f;
            }
        }
    }
}
