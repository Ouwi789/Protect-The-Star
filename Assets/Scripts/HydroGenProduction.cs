using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydroGenProduction : MonoBehaviour
{
    public float generateTime = 5f;
    private float counter = 0f;
    private GameObject gameController;
    private StatsHolder script;
    private bool isFake;
    

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
                script.hydrogenAmount++;
                counter = 0f;
            }
        }
    }
}
