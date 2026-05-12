using Unity.Mathematics;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Selection needed")]
    [SerializeField] public int ID = 0; // added new for the selection menu usage
    [SerializeField] string Name = " "; // added new for the selection menu usage
    [SerializeField] string Color = " "; // added new for the selection menu usage

    [Header("The Rest Of the Things")]
    [SerializeField] bool isPlayer;
    [SerializeField] int scoreValue = 50;
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] bool applyCameraShake;


    CameraShake cameraShake;
    AudioManager audioManager;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    ShieldActivation shieldActivation;

    void Start() // get all the component needed
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioManager = FindAnyObjectByType<AudioManager>();
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        levelManager = FindAnyObjectByType<LevelManager>();  

        if (gameObject.name == "Player")
        {
            shieldActivation = transform.Find("Shield").GetComponent<ShieldActivation>();
        }

    }
    void OnTriggerEnter2D(Collider2D other) //Damage indecator in general.
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        //make sure it's only counting Damage when it touches the Hit box of the player which is Circle
        if(other.IsTouching(GetComponent<CircleCollider2D>())){
            if (damageDealer != null)
            {
                TakeDamage(damageDealer.GetDamage());
                PlayHitParticles();
                damageDealer.Hit(other);
                audioManager.PlayDamageSFX();
                if (applyCameraShake)
                {
                    cameraShake.Play();
                }
                
            }
        }
        //if it touches the shield of the player
        else if (other.IsTouching(transform.Find("Shield").GetComponent<EdgeCollider2D>())) 
        {
            damageDealer.Hit(other);
            shieldActivation.PlayerShieldOut();
            shieldActivation.SetShieldIsActive(false);
        }
    }
    void TakeDamage(int damage) // Reduce Health when taking damage 
    {
        health -= damage;     
        
        if(health <= 0)
        {
            die();
        }  
    }
    void die() // when health reaches 0 
    {
        if (isPlayer){ // if the player Dies Load Game over
            levelManager.LoadGameOver();
        }
        else { // if none player dies Update Score
            scoreKeeper.ModifyScore(scoreValue);
        }
        Destroy(gameObject);
    }
    void PlayHitParticles() // Partical animation when anybody hit with bullets
    {
        if (hitParticles != null)
        {
            ParticleSystem particles = Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(particles, particles.main.duration + particles.main.startLifetime.constantMax);
        }
    }
 
    //setters and getters
    public int GetHealth(){ 
        return health;
    }

    public string GetColor()
    {
        return Color;
    }
    public void SetColor(string value)
    {
        Color = value;
    }

    public string GetName()
    {
        return Name;
    }

    public void SetName(string value)
    {
        Name = value;
    }

    public int GetID()
    {
        return ID;
    }
    public void SetID(int value)
    {
        ID = value;
    }

    public void SetHP(int value)
    {
        health = value;
    }

    public void HealPlayer(int value)
    {
        health += value;
    }

}
