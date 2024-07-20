using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject defaultUFO;
    public GameObject fastUFO;
    public GameObject slowUFO;

    public GameObject kamikaze;
    public GameObject fastKamikaze;
    public GameObject slowKamikaze;

    public GameObject satellite;
    public GameObject fastSatellite;
    public GameObject slowSatellite;

    public GameObject boss1;
    public GameObject boss2;
    public GameObject boss3;
    public GameObject boss4;

    public GameState gameState;
    public static int storyLevel; //from 1 - 10, put 0 if its infinite mode
    public static int wave = 1; //for infite mode
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
                int level = 1;
                while (gameState.health >= 0)
                {
                    if(level % 10 == 0)
                    {
                        int temp1 = UnityEngine.Random.Range(1, 5);
                        switch (temp1)
                        {
                            case 1:
                                spawnEnemies(1, boss1, true);
                                break;
                            case 2:
                                spawnEnemies(1, boss2, true);
                                break;
                            case 3:
                                spawnEnemies(1, boss3, true);
                                break;
                            case 4:
                                spawnEnemies(1, boss4, true);
                                break;
                        }
                    }
                    waveText.SetText("Wave: " + level);
                    int temp = UnityEngine.Random.Range(1, 4);
                    switch (temp)
                    {
                        case 1:
                            spawnEnemies(2 * level, defaultUFO, true);
                            spawnEnemies(2 * level, fastKamikaze, true);
                            spawnEnemies(2 * level, slowSatellite, true);
                            break;
                        case 2:
                            spawnEnemies(2 * level, fastUFO, true);
                            spawnEnemies(2 * level, slowKamikaze, true);
                            spawnEnemies(2 * level, satellite, true);
                            break;
                        case 3:
                            spawnEnemies(2 * level, slowUFO, true);
                            spawnEnemies(2 * level, kamikaze, true);
                            spawnEnemies(2 * level, fastSatellite, true);
                            break;
                    }
                    yield return waitWaveFinish();
                    yield return startWave();
                    level++;
                    wave = level;
                }
                break;
            case 1:
                waveText.SetText("Wave: 1");
                spawnEnemies(2, defaultUFO, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 2");
                spawnEnemies(3, defaultUFO, true);
                spawnEnemies(1, slowUFO, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 3");
                spawnEnemies(5, fastUFO, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 4");
                spawnEnemies(5, slowUFO, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 5");
                spawnEnemies(3, defaultUFO, true);
                spawnEnemies(2, slowUFO, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 6");
                spawnEnemies(1, defaultUFO, true);
                spawnEnemies(1, slowUFO, false);
                spawnEnemies(5, fastUFO, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 7");
                spawnEnemies(10, defaultUFO, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 8");
                spawnEnemies(5, fastUFO, true);
                spawnEnemies(2, slowUFO, false);
                spawnEnemies(1, defaultUFO, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 9");
                spawnEnemies(7, slowUFO, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 10");
                spawnEnemies(5, defaultUFO, false);
                spawnEnemies(1, boss1, true);
                yield return waitWaveFinish(); 
                break;
            case 2:
                waveText.SetText("Wave: 1");
                spawnEnemies(2, kamikaze, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 2");
                spawnEnemies(3, kamikaze, true);
                spawnEnemies(1, slowKamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 3");
                spawnEnemies(5, fastKamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 4");
                spawnEnemies(5, slowKamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 5");
                spawnEnemies(3, kamikaze, true);
                spawnEnemies(2, slowKamikaze, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 6");
                spawnEnemies(1, kamikaze, true);
                spawnEnemies(1, slowKamikaze, false);
                spawnEnemies(5, fastKamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 7");
                spawnEnemies(10, kamikaze, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 8");
                spawnEnemies(5, fastKamikaze, true);
                spawnEnemies(2, slowKamikaze, false);
                spawnEnemies(1, kamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 9");
                spawnEnemies(7, slowKamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 10");
                spawnEnemies(5, kamikaze, false);
                spawnEnemies(1, boss1, true);
                yield return waitWaveFinish();
                break;
            case 3:
                waveText.SetText("Wave: 1");
                spawnEnemies(2, satellite, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 2");
                spawnEnemies(3, satellite, true);
                spawnEnemies(1, slowSatellite, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 3");
                spawnEnemies(5, fastSatellite, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 4");
                spawnEnemies(5, slowSatellite, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 5");
                spawnEnemies(3, satellite, true);
                spawnEnemies(2, slowSatellite, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 6");
                spawnEnemies(1, satellite, true);
                spawnEnemies(1, slowSatellite, false);
                spawnEnemies(5, fastSatellite, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 7");
                spawnEnemies(10, satellite, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 8");
                spawnEnemies(5, fastSatellite, true);
                spawnEnemies(2, slowSatellite, false);
                spawnEnemies(1, satellite, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 9");
                spawnEnemies(7, slowSatellite, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 10");
                spawnEnemies(5, satellite, false);
                spawnEnemies(1, boss1, true);
                yield return waitWaveFinish();
                break;
            case 4:
                waveText.SetText("Wave: 1");
                spawnEnemies(3, defaultUFO, true);
                spawnEnemies(1, kamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 2");
                spawnEnemies(5, slowKamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 3");
                spawnEnemies(5, fastSatellite, false);
                spawnEnemies(2, slowSatellite, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 4");
                spawnEnemies(8, slowUFO, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 5");
                spawnEnemies(10, fastKamikaze, true);
                spawnEnemies(1, satellite, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 6");
                spawnEnemies(10, satellite, true);
                spawnEnemies(5, kamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 7");
                spawnEnemies(10, satellite, true);
                spawnEnemies(10, defaultUFO, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 8");
                spawnEnemies(12, slowUFO, false);
                spawnEnemies(1, satellite, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 9");
                spawnEnemies(5, kamikaze, false);
                spawnEnemies(4, slowUFO, false);
                spawnEnemies(6, fastKamikaze, true);
                spawnEnemies(3, slowSatellite, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 10");
                spawnEnemies(5, satellite, false);
                spawnEnemies(5, defaultUFO, true);
                spawnEnemies(1, boss2, true);
                yield return waitWaveFinish();
                break;
            case 5:
                waveText.SetText("Wave: 1");
                spawnEnemies(4, defaultUFO, true);
                spawnEnemies(2, slowKamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 2");
                spawnEnemies(10, fastKamikaze, false);
                spawnEnemies(2, defaultUFO, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 3");
                spawnEnemies(5, fastUFO, false);
                spawnEnemies(5, slowSatellite, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 4");
                spawnEnemies(8, fastUFO, false);
                spawnEnemies(6, slowSatellite, true);
                spawnEnemies(1, kamikaze, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 5");
                spawnEnemies(20, fastKamikaze, true);
                spawnEnemies(1, satellite, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 6");
                spawnEnemies(10, satellite, true);
                spawnEnemies(10, slowKamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 7");
                spawnEnemies(12, satellite, true);
                spawnEnemies(10, slowUFO, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 8");
                spawnEnemies(12, slowUFO, false);
                spawnEnemies(10, fastKamikaze, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 9");
                spawnEnemies(8, kamikaze, false);
                spawnEnemies(2, slowUFO, false);
                spawnEnemies(8, fastKamikaze, true);
                spawnEnemies(5, slowSatellite, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 10");
                spawnEnemies(5, satellite, false);
                spawnEnemies(5, defaultUFO, true);
                spawnEnemies(5, slowKamikaze, true);
                spawnEnemies(1, boss2, true);
                yield return waitWaveFinish();
                break;
            case 6:
                waveText.SetText("Wave: 1");
                spawnEnemies(5, fastUFO, true);
                spawnEnemies(2, slowKamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 2");
                spawnEnemies(10, slowKamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 3");
                spawnEnemies(5, fastUFO, false);
                spawnEnemies(7, slowSatellite, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 4");
                spawnEnemies(10, fastUFO, false);
                spawnEnemies(4, slowSatellite, true);
                spawnEnemies(3, slowKamikaze, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 5");
                spawnEnemies(20, kamikaze, true);
                spawnEnemies(3, satellite, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 6");
                spawnEnemies(10, fastUFO, true);
                spawnEnemies(15, slowKamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 7");
                spawnEnemies(15, satellite, true);
                spawnEnemies(10, slowUFO, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 8");
                spawnEnemies(9, slowUFO, false);
                spawnEnemies(20, fastKamikaze, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 9");
                spawnEnemies(10, kamikaze, false);
                spawnEnemies(9, slowUFO, false);
                spawnEnemies(8, fastKamikaze, true);
                spawnEnemies(5, slowSatellite, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 10");
                spawnEnemies(8, satellite, false);
                spawnEnemies(10, defaultUFO, true);
                spawnEnemies(5, slowKamikaze, true);
                spawnEnemies(1, boss2, true);
                yield return waitWaveFinish();
                break;
            case 7:
                waveText.SetText("Wave: 1");
                spawnEnemies(5, slowUFO, true);
                spawnEnemies(2, fastKamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 2");
                spawnEnemies(13, slowKamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 3");
                spawnEnemies(10, fastUFO, false);
                spawnEnemies(10, slowSatellite, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 4");
                spawnEnemies(10, fastUFO, false);
                spawnEnemies(10, slowSatellite, true);
                spawnEnemies(8, slowKamikaze, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 5");
                spawnEnemies(25, fastKamikaze, true);
                spawnEnemies(9, slowSatellite, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 6");
                spawnEnemies(12, fastUFO, true);
                spawnEnemies(15, slowKamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 7");
                spawnEnemies(15, slowSatellite, true);
                spawnEnemies(10, fastUFO, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 8");
                spawnEnemies(11, slowUFO, false);
                spawnEnemies(25, fastKamikaze, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 9");
                spawnEnemies(10, kamikaze, false);
                spawnEnemies(10, slowUFO, false);
                spawnEnemies(9, fastKamikaze, true);
                spawnEnemies(7, slowSatellite, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 10");
                spawnEnemies(8, satellite, false);
                spawnEnemies(10, defaultUFO, true);
                spawnEnemies(5, slowKamikaze, true);
                spawnEnemies(1, boss3, true);
                yield return waitWaveFinish();
                break;
            case 8:
                waveText.SetText("Wave: 1");
                spawnEnemies(5, slowUFO, true);
                spawnEnemies(2, fastKamikaze, false);
                spawnEnemies(2, satellite, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 2");
                spawnEnemies(18, slowKamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 3");
                spawnEnemies(12, fastUFO, false);
                spawnEnemies(10, slowSatellite, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 4");
                spawnEnemies(10, fastUFO, false);
                spawnEnemies(10, slowSatellite, true);
                spawnEnemies(8, slowKamikaze, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 5");
                spawnEnemies(30, fastKamikaze, true);
                spawnEnemies(9, slowSatellite, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 6");
                spawnEnemies(20, fastUFO, true);
                spawnEnemies(15, slowKamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 7");
                spawnEnemies(15, slowSatellite, true);
                spawnEnemies(25, fastUFO, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 8");
                spawnEnemies(11, slowUFO, false);
                spawnEnemies(30, fastKamikaze, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 9");
                spawnEnemies(10, kamikaze, false);
                spawnEnemies(10, slowUFO, false);
                spawnEnemies(12, fastKamikaze, true);
                spawnEnemies(15, slowSatellite, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 10");
                spawnEnemies(8, slowUFO, false);
                spawnEnemies(10, fastSatellite, true);
                spawnEnemies(20, fastKamikaze, true);
                spawnEnemies(1, boss3, true);
                yield return waitWaveFinish();
                break;
            case 9:
                waveText.SetText("Wave: 1");
                spawnEnemies(10, fastUFO, true);
                spawnEnemies(2, slowSatellite, false);
                spawnEnemies(4, slowKamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 2");
                spawnEnemies(23, slowKamikaze, false);
                spawnEnemies(3, fastUFO, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 3");
                spawnEnemies(20, fastUFO, false);
                spawnEnemies(20, slowSatellite, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 4");
                spawnEnemies(20, fastUFO, false);
                spawnEnemies(10, slowSatellite, true);
                spawnEnemies(10, slowKamikaze, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 5");
                spawnEnemies(30, fastKamikaze, true);
                spawnEnemies(13, slowSatellite, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 6");
                spawnEnemies(29, fastUFO, true);
                spawnEnemies(20, slowKamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 7");
                spawnEnemies(20, slowSatellite, true);
                spawnEnemies(35, fastUFO, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 8");
                spawnEnemies(20, slowUFO, false);
                spawnEnemies(30, fastKamikaze, true);
                spawnEnemies(5, slowSatellite, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 9");
                spawnEnemies(20, kamikaze, false);
                spawnEnemies(20, slowUFO, false);
                spawnEnemies(15, fastKamikaze, true);
                spawnEnemies(15, slowSatellite, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 10");
                spawnEnemies(8, slowUFO, false);
                spawnEnemies(20, fastSatellite, true);
                spawnEnemies(20, fastKamikaze, true);
                spawnEnemies(1, boss3, true);
                yield return waitWaveFinish();
                break;
            case 10:
                waveText.SetText("Wave: 1");
                spawnEnemies(10, fastUFO, true);
                spawnEnemies(10, slowSatellite, false);
                spawnEnemies(10, kamikaze, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 2");
                spawnEnemies(30, slowKamikaze, false);
                spawnEnemies(10, fastUFO, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 3");
                spawnEnemies(25, fastUFO, false);
                spawnEnemies(25, slowSatellite, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 4");
                spawnEnemies(20, fastUFO, false);
                spawnEnemies(20, slowSatellite, true);
                spawnEnemies(15, slowKamikaze, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 5");
                spawnEnemies(30, defaultUFO, true);
                spawnEnemies(15, slowSatellite, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 6");
                spawnEnemies(29, fastUFO, true);
                spawnEnemies(20, slowKamikaze, false);
                spawnEnemies(2, satellite, false);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 7");
                spawnEnemies(20, slowSatellite, true);
                spawnEnemies(35, fastUFO, false);
                spawnEnemies(10, fastKamikaze, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 8");
                spawnEnemies(30, slowUFO, false);
                spawnEnemies(40, fastKamikaze, true);
                spawnEnemies(10, slowSatellite, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 9");
                spawnEnemies(30, kamikaze, false);
                spawnEnemies(40, slowUFO, false);
                spawnEnemies(20, fastKamikaze, true);
                spawnEnemies(20, slowSatellite, true);
                yield return waitWaveFinish();
                yield return startWave();
                waveText.SetText("Wave: 10");
                spawnEnemies(50, slowSatellite, false);
                spawnEnemies(1, boss4, true);
                yield return waitWaveFinish();
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
            yield return new WaitForSeconds(1);
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
