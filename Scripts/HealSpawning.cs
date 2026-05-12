using UnityEngine;

public class HealSpawning : MonoBehaviour
{
    [SerializeField] GameObject healthPrefap;

    void OnDestroy()
    {
        if (gameObject.scene.isLoaded && healthPrefap != null)
        {
            if (Random.value <= 0.15f) //15% to drop health
            {

                Instantiate(healthPrefap, transform.position, transform.rotation);
            } 
        }
    }
}
