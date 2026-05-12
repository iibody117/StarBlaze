using UnityEngine;

public class PowerUpMover : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 5f;


void Start() // the Object Spawns Destroy it after lifeTime;
{
     Destroy(gameObject, lifeTime);
}

    void Update() // move Down with speed; 
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    
}
