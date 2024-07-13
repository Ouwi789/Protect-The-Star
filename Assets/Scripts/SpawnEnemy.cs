using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject defaultEnemy;
    public GameState gameState;
    public static int storyLevel; //from 1 - 10, put 0 if its infinite mode and -1 for tutorial
    public int wave = 1; //will need to design an enemy spawn system
    private Transform[] spawnPositions;
    public TMP_Text waveText;
    public StatsHolder stats;

    private void Start()
    {
        spawnPositions = GetComponentsInChildren<Transform>();
        for(int i = 0; i < spawnPositions.Length; i++)
        {
            if(spawnPositions[i].position == Vector3.zero)
            {
                RemoveAt(ref spawnPositions, i);
                break;
            }
        }
        StartCoroutine(playGame());
    }
    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        for (int a = index; a < arr.Length - 1; a++)
        {
            // moving elements downwards, to fill the gap at [index]
            arr[a] = arr[a + 1];
        }
        // finally, let's decrement Array's size by one
        Array.Resize(ref arr, arr.Length - 1);
    }

    IEnumerator playGame()
    {
        yield return new WaitForSeconds(2);
        yield return startGame();

        switch (storyLevel)
        {
            case 0:
                yield return new WaitForSeconds(60f);
                break;
            case 1:
                waveText.SetText("Wave: 1");
                spawnEnemies(5, defaultEnemy, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 2");
                spawnEnemies(10, defaultEnemy, false);
                yield return waitWaveFinish();
                yield return startWave(); 
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
        }

        yield return gameState.WinGame();
        yield return null;
    }

    IEnumerator wait(float waitTime)
    {
        float counter = 0;

        while (counter < waitTime)
        {
            //Increment Timer until counter >= waitTime
            counter += Time.deltaTime;
        }
        yield return null;
    }
    IEnumerator waitWaveFinish()
    {
        while (!checkForNoEnemies())
        {
            yield return wait(1);
        }
        yield return null;
    }
    IEnumerator startGame()
    {
        waveText.SetText("Game Starting In 3 ...");
        yield return new WaitForSeconds(1);
        waveText.SetText("Game Starting In 2 ...");
        yield return new WaitForSeconds(1);
        waveText.SetText("Game Starting In 1 ...");
        yield return new WaitForSeconds(1);
        yield return null;
    }
    IEnumerator startWave()
    {
        gameState.setHealth(gameState.getHealth() + StatsHolder.regen);
        stats.setHelium(stats.getHelium() + StatsHolder.heliumGain);
        stats.setHydrogen(stats.getHydrogen() + StatsHolder.hydrogenGain);
        waveText.SetText("Next Wave Starting In 3 ...");
        yield return new WaitForSeconds(1);
        waveText.SetText("Next Wave Starting In 2 ...");
        yield return new WaitForSeconds(1);
        waveText.SetText("Next Wave Starting In 1 ...");
        yield return new WaitForSeconds(1);
        yield return null;
    }
    void spawnEnemies(int num, GameObject enemy, bool up)
    {
        if (up)
        {
            for (int i = 0; i < num; i++)
            {
                Instantiate(enemy, spawnPositions[UnityEngine.Random.Range(0, 4)].position, Quaternion.identity);
            }
        } else
        {
            for (int i = 0; i < num; i++)
            {
                Instantiate(enemy, spawnPositions[UnityEngine.Random.Range(4, 8)].position, Quaternion.identity);
            }
        }
        
    }
    bool checkForNoEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return (enemies.Length == 0);
    }
}
