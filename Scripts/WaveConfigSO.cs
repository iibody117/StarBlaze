using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "WaveConfig", menuName = "New WaveConfigSO")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float enemyMoveSpeed = 5f;
    [SerializeField] float timeBetweenEnemySpawns = 1f;
    [SerializeField] float enemySpawnVariance = 0f;
    [SerializeField] float minimumSpawnTime = 0.2f;

    //Getters
    public int GetEnemyCount()
    {
        return enemyPrefabs.Length;
    }
    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }
    public Transform GetStartingPoint()
    {
        return pathPrefab.GetChild(0);
    }
    public float GetEnemyMoveSpeed()
    {
        return enemyMoveSpeed;
    }
    public Transform[] GetWayPoints() //get children of path (enemies) 
    {
        Transform[] waypoints = new Transform[pathPrefab.childCount];
        for (int i = 0; i < pathPrefab.childCount; i++)
        {
            waypoints[i] = pathPrefab.GetChild(i);
        }
        return waypoints;
    }
    public float GetRandomEnemySpawnTime()// Spawn enemies in Random times
    {
        float spawnTime= Random.Range(timeBetweenEnemySpawns-enemySpawnVariance,
         timeBetweenEnemySpawns+ enemySpawnVariance);

         spawnTime= math.clamp(spawnTime, minimumSpawnTime, float.MaxValue);
         return spawnTime;
    }
}
