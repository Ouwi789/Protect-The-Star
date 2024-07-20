using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public bool paused;
    public bool exit;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }
        if (paused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        } else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void setExit(bool temp)
    {
        exit = temp;
    }
}
