using UnityEngine;

public class AircraftInfo : MonoBehaviour
{
    
    // the purpose of this script to take the selection manager info and when the game loads, it finds the player, 
    // on awake it chnages the apearance and the info of the aircraft

    public static AircraftInfo instance;
    [Header("AirCraft Info")]
    [SerializeField] GameObject Projectile;
    [SerializeField] Sprite aircraftImage = null;
    [SerializeField] int ID = 0;
    [SerializeField] string color = null;
    [SerializeField] string name = null;
    [SerializeField] int damage = 0;
    [SerializeField] int health = 0;
    [SerializeField] float FR = 0.1f;

    [Header("Outside Sources needed")]
    [SerializeField] GameObject playerPrefap;
    [SerializeField] Health playerPrefapHPScript; //health and id
    [SerializeField] Shooter playerPrefapFRScript; //FR
    [SerializeField] DamageDealer playerPrefapDMGScript;//DMG

    SpriteRenderer PlayerSprite;


    
    void Awake()
    {
        ManageSingleton();
        if(ID == 0) //setting the Aircraft To Default Prefap for player
        {
            PlayerSprite = playerPrefap.GetComponentInChildren<SpriteRenderer>();

            SetSprite(PlayerSprite.sprite);
            SetID(playerPrefapHPScript.GetID());
            SetColor(playerPrefapHPScript.GetColor());
            SetName(playerPrefapHPScript.GetName());
            SetDamage(playerPrefapDMGScript.GetDamage());
            SetHealth(playerPrefapHPScript.GetHealth());
            SetMinimumFR(playerPrefapFRScript.GetMinimumFR());
            SetPorjectile(playerPrefapFRScript.GetShotObject());
            Debug.Log("Info at default");
        }
    }
    void ManageSingleton() // Keep ScoorKeeper on "DontDestroyOnLoad" So we can take it to other Scenes
    {
        if(instance != null && instance != this)// check if already exist
        {
            Debug.Log("if worked");
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;

        }
        //build a new one
        
            Debug.Log("else if worked");
            instance = this;
            DontDestroyOnLoad(gameObject);
    }


    //setters       
    public void SetPorjectile(GameObject value){Projectile = value;}                  
    public void SetSprite(Sprite image){aircraftImage = image;}
    public void SetID(int value){ID = value;}
    public void SetColor(string value){color = value;}
    public void SetName(string value){name = value;}
    public void SetDamage(int value){damage = value;}
    public void SetHealth(int value){health = value;}
    public void SetMinimumFR(float value){FR = value;}


    //getters
    public GameObject GetShotPrefap(){return Projectile;}
    public Sprite GetSprite() { return aircraftImage; }
    public int GetID() { return ID; }
    public string GetColor() { return color; }
    public string GetName() { return name; }
    public int GetDamage() { return damage; }
    public int GetHealth() { return health; }
    public float GetMinimumFR() { return FR; }


    public void SetChangesOnPlayer()
    {
        //find the player in the scene to do the changes
        PlayerSprite = GameObject.Find("Player").GetComponentInChildren<SpriteRenderer>();
        playerPrefapHPScript = GameObject.Find("Player").GetComponent<Health>();
        playerPrefapFRScript = GameObject.Find("Player").GetComponent<Shooter>();
        playerPrefapDMGScript = GameObject.Find("Player").GetComponent<DamageDealer>();

        //setting and getting
            PlayerSprite.sprite = GetSprite();
            playerPrefapHPScript.SetID(GetID());
            playerPrefapHPScript.SetColor(GetColor());
            playerPrefapHPScript.SetName(GetName());
            playerPrefapDMGScript.SetDamage(GetDamage());
            playerPrefapHPScript.SetHP(GetHealth());
            playerPrefapFRScript.SetMinimumFR(GetMinimumFR());
            playerPrefapFRScript.SetShotObject(GetShotPrefap());
            Debug.Log("changes has madet");
    }
}
