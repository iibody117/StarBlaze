using UnityEngine;
using Random = UnityEngine.Random;

public class AstroidMover : MonoBehaviour
{
    private float speed = 8f;
    private float lifeTime = 5f;
    [SerializeField] GameObject powerup1;
    [SerializeField] GameObject powerup2;
    [SerializeField] GameObject powerup3;

    public void SetMoveData(float newSpeed, float newLifeTime)
    {
        speed = newSpeed;
        lifeTime = newLifeTime;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    
    void OnDestroy()
    {
        if (gameObject.scene.isLoaded){ // needed this to make sure going to another scene won't spawn the power up
            int Rvalue = Random.Range(0,4);
            if (Rvalue == 1) // extra damage
            {
                Instantiate(powerup1, transform.position, Quaternion.identity);
            }
            else if (Rvalue == 2) // extra fire rate
            {
                Instantiate(powerup2, transform.position, Quaternion.identity);
            }
            else if (Rvalue == 3)// Shield 
            {
                Instantiate(powerup3, transform.position, Quaternion.identity);
            }
            else // nothing
            Debug.Log(Rvalue);
        }
    }
}