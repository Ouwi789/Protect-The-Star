using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitResume : MonoBehaviour
{
    [SerializeField] Pause pause;

    public void Exit()
    {
        Time.timeScale = 1;
        pause.setExit(true);
        pause.paused = false;
    }

    public void Resume()
    {
        pause.paused = false;
    }

}
