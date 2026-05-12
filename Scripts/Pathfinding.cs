using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    WaveConfigSO waveConfig;
    EnemySpawner enemySpawner;
    Transform[] waypoints;
    int waypointIndex = 0;
    void Start()
    {
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWayPoints();
        transform.position = waveConfig.GetStartingPoint().position;
    }


    void Update()
    {
        Followpath();
    }
    void Followpath() // enemy follow the path until reaching the last waypoint
    {
        if (waypointIndex < waypoints.Length) //check of it is the last waypoint
        {
            Vector3 targetPostion = waypoints[waypointIndex].position;
            float moveDelta = waveConfig.GetEnemyMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPostion, moveDelta);
            if (transform.position == targetPostion)
            {
                waypointIndex++;
            }
        }
        else // if reached last checkpoint destroy object
        {
            Destroy(gameObject);
        }
    }
}
