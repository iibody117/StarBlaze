using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class AstroidShooting : MonoBehaviour
{
    [Header("AI")]
    [SerializeField] private bool AI = true;

    [Header("Shooters")]
    [SerializeField] private Transform shooter1;
    [SerializeField] private Transform shooter2;
    [SerializeField] private Transform shooter3;

    [Header("Astroids")]
    [SerializeField] private GameObject astroid1Prefab;
    [SerializeField] private GameObject astroid2Prefab;
    [SerializeField] private GameObject astroid3Prefab;

    [Header("Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private float timeBetweenShots = 1f;

    [SerializeField] public int astroidsShot = 0;

    [SerializeField] EnemySpawner enemySpawner;
    
    private Transform[] shooters;
    private GameObject[] astroidPrefabs;

    void Awake() // getting the shooter and asstroids prefaps on awaking the scene
    {
        shooters = new Transform[] { shooter1, shooter2, shooter3 };
        astroidPrefabs = new GameObject[] { astroid1Prefab, astroid2Prefab, astroid3Prefab };
    }

    public void Start() 
    {
        astroidsShot = 0;
        if (AI) //check if it's able to shoot or no 
        {
            StartCoroutine(ShootingRoutine());
        }
    }

    private IEnumerator ShootingRoutine()
    {
        while (AI) //as long is it's able to shoot astroid shoot
        {
            ShootRandomAstroid();
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    private void ShootRandomAstroid()
    {
        if (shooters.Length == 0 || astroidPrefabs.Length == 0) //check of the arraies is missing
        {
            Debug.LogWarning("Shooters or Astroid Prefabs are missing.");
            return;
        }

        Transform randomShooter = shooters[Random.Range(0, shooters.Length)]; // making the shooters on array
        GameObject randomAstroidPrefab = astroidPrefabs[Random.Range(0, astroidPrefabs.Length)]; //same as astroid

        Quaternion shootRotation = Quaternion.Euler(0f, 0f, 180f); // rotate every astroids on spawn so we can transform correctly


        //making a game object in spawning
        GameObject spawnedAstroid = Instantiate(randomAstroidPrefab,randomShooter.position,shootRotation);

        //making a mover as public script
        AstroidMover mover = spawnedAstroid.GetComponent<AstroidMover>();


        if (mover != null)
        {   //calling the mover method from the script
            mover.SetMoveData(speed, lifeTime);
            astroidsShot++;
        }
        else
        {   //check if the component listed is missing
            Debug.LogWarning("AstroidMover component is missing on asteroid prefab: " + randomAstroidPrefab.name);
        }

        if(astroidsShot >= 18)
        {
            StartCoroutine(Wait());
            AI = false;
            enemySpawner.isLoopingSet(true);
            enemySpawner.waveCounter = 0;
            enemySpawner.Start();
        }
    }

    public void AISet(bool value)
    {
        AI = value;
    }

    private IEnumerator Wait()
    {
        Debug.Log("Wait for 2 second");
        yield return new WaitForSeconds(2f);
    }

}
