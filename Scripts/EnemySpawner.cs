using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] WaveConfigSO[] waveConfigs;
    [SerializeField] float timeBetweenWaves = 1f;
    [SerializeField] bool isLooping;
    WaveConfigSO currentWave;

    [SerializeField] public int waveCounter = 0;
    [SerializeField] private AstroidShooting astroidShooting;
    
    public void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
 
    IEnumerator SpawnEnemies() // Function that spawn enemies, We used IEnumerator to make delay time between each spawn
    {
        do
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {// for each Loop Enemy Spawns until Time of wave reaches 0
                    Instantiate(currentWave.GetEnemyPrefab(i),
                    currentWave.GetStartingPoint().position,
                    quaternion.identity, transform);
                    yield return new WaitForSeconds(currentWave.GetRandomEnemySpawnTime());
                }
                waveCounter++;
                yield return new WaitForSeconds(timeBetweenWaves);
            }
            Debug.Log("foreach Done"); 
            if(waveCounter >= 2)
            {
                Debug.Log("time to break" + waveCounter);
                break;
            }
        }
        while (isLooping);
        isLooping = false;
        Debug.Log("Loop Broke");
        yield return new WaitForSeconds(timeBetweenWaves);
        astroidShooting.astroidsShot = 0;
        astroidShooting.AISet(true);
        astroidShooting.Start();




    }
    public WaveConfigSO GetCurrentWave() // return the wave chosen
    {
        return currentWave;
    }

    public void isLoopingSet(bool value)
    {
        isLooping = value;
    }

}
