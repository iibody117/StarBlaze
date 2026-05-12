using UnityEngine;

public class PowerUpIdentity : MonoBehaviour
{
    [SerializeField] int powerUpID = 0;
    [SerializeField] string powerUpName = null;

    PowerUpCollecter powerUpCollecter; //because it's a prefap, you can't add a game object in scene to a prefap
    //you have to add it in the start method and fine it 


    
    void Start()
    {
        // so it finds the player on start 
        powerUpCollecter = powerUpCollecter = GameObject.Find("Player").GetComponent<PowerUpCollecter>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player") //puches identity to player power up collector
        {
            powerUpCollecter.PowerUpIDTaker(powerUpID);
            Destroy(gameObject);
        }
    }
}
