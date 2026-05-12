using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    [Header("Base Variables")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFireRate = 0.2f;

    [Header("AI Variables")]
    [HideInInspector] public bool isFiring;
    [SerializeField] bool useAI;
    [SerializeField] float minimumFireRate = 0.2f;
    [SerializeField] float fireRateVariance = 0;
    Coroutine fireCoroutine;
    AudioManager audioManager;
    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();

        if (useAI) // if the check box True enemy shoot else enemy don't shoot
        {
            isFiring = true;
        }
    }
    void Update()
    {
        Fire();
    }
    void Fire()
    { // Check the player if he is holding the Shooting Botton down
        if (isFiring && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }
    IEnumerator FireContinuously()
    {
        while (true)
        {
            //The player Shooting Code + speed assigned + Postion it's moving
            GameObject projectile = Instantiate(projectilePrefab, transform.position, quaternion.identity);
            projectile.transform.rotation = transform.rotation;
            Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();
            projectileRB.linearVelocity = transform.up * projectileSpeed;
            Destroy(projectile, projectileLifetime);

            //Randomizting enemy shooting time
            float waitTime = Random.Range(baseFireRate - fireRateVariance, baseFireRate + fireRateVariance);
            waitTime = math.clamp(waitTime, minimumFireRate, float.MaxValue);
            audioManager.PlayShootingSFX();
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void SetMinimumFR(float vlaue) //Setter for fireRate
    {
        minimumFireRate = vlaue;
    }
    public float GetMinimumFR()
    {
        return minimumFireRate;
    }

    public void SetShotObject(GameObject value)
    {
        projectilePrefab = value;
    }
    public GameObject GetShotObject()
    {
        return projectilePrefab;
    }
    
}
