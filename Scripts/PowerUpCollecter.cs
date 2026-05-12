using UnityEngine;
using System.Collections;

public class PowerUpCollecter : MonoBehaviour
{
    [SerializeField] string powerUpName = null;
    [SerializeField] int powerUpID = 0;
    int previousID;

    [SerializeField] UIUpdater uiUpdater;
    [SerializeField] int maxHP;
    
    [SerializeField] float healthDeserved;

    Shooter PlayerShooter;
    DamageDealer PlayerDamageDealer;
    ShieldActivation shieldActivation;
    Health healthScript;

    [SerializeField] float defaultFireRate;
    [SerializeField] int defaultDamage;

    [Header("Buff projectile")]
    [SerializeField] GameObject NormalBlue;
    [SerializeField] GameObject NormalGreen;
    [SerializeField] GameObject NormalOrange;
    [SerializeField] GameObject BlueBuff;
    [SerializeField] GameObject GreenBuff;
    [SerializeField] GameObject OrangeBuff;


    [Header("Timers")]
    [SerializeField] GameObject TimerFR;
    [SerializeField] GameObject TimerEXD;
    [SerializeField] float coolDownFR;
    [SerializeField] float coolDownExtraDamage;



    void Awake()
    {
        TimerFR.SetActive(false);
        TimerEXD.SetActive(false);
    }

    void Start()
    {
        //the word transform only search in the children
        shieldActivation = transform.Find("Shield").GetComponent<ShieldActivation>(); 
        StartCoroutine(WaitToTakeInfo());
        
        powerUpName = null;
        powerUpID = 0;
    }

    public void PowerUpIDTaker(int ID)
    {
        if(ID == 1) //when it hits the lighting object
        {
            powerUpName = "Fire Rate";
            powerUpID = 1;
            previousID = 1;
            uiUpdater.PowerUpUpdater(powerUpID);
        }
        else if(ID == 2) //when it hits the fist object
        {
            powerUpName = "Extra Damage";
            powerUpID = 2;
            previousID = 2;
            uiUpdater.PowerUpUpdater(powerUpID);
        }
        else if(ID == 3) //when taking the shield, no need to put it on effect instantly play the shield
        {
            shieldActivation.PlayShieldIn();
            if (previousID == 0)
            {
                powerUpID = 0;
                powerUpName = null;
            }
            else
                powerUpID = previousID;
        }
        else if(ID == 4)
        {
            if(healthScript.GetHealth() >= maxHP)
            {
                if(previousID == 0){
                powerUpID = 0;
                powerUpName = null;
                }
                else{powerUpID = previousID;}
                
                return;
            }
            healthDeserved = maxHP * 0.2f;  //it always send 20% health 
            healthScript.HealPlayer((int)healthDeserved);

            if(previousID == 0){
                powerUpID = 0;
                powerUpName = null;
            }
        }
    }


    public void PowerTrigger() //this is the trigger that sets the game object On Or Off to start Timers
    {
        if(powerUpID == 0) // No Power Up Stored
        {
            Debug.Log("No powers");
        }
        else if(powerUpID == 1) //Use FireRate
        {
            TimerFR.SetActive(true);            //Set Time on
            uiUpdater.PowerUpUpdater(0);        // Remove FireRate Image From UI
            powerUpName = null;                 //reset the collector to have no powers -- 
            powerUpID = 0;                      //also here
            previousID = 0;
            PowerUpEffect(1); 
        }
        else if(powerUpID == 2)// Extra Damage
        {
            TimerEXD.SetActive(true);           //Set Time on
            uiUpdater.PowerUpUpdater(0);        // Remove ExtraDamage Image From UI
            powerUpName = null;                 //reset the collector to have no powers -- 
            powerUpID = 0;                      //also here
            previousID = 0;
            PowerUpEffect(3);
        }
    }

    public void PowerUpEffect(int ID) // applying the effect Triggered
    {
        if(ID == 0) //reset FR
        {
            PlayerShooter.SetMinimumFR(defaultFireRate);
        }
        else if(ID == 1) //Increase FR
        {
            PlayerShooter.SetMinimumFR(defaultFireRate * 0.5f);
            uiUpdater.TimeResetWhenNeeded(0);
        }
        else if (ID == 2) //Reset Damage
        {
            PlayerDamageDealer.SetDamage(defaultDamage);
            if(healthScript.GetID() == 1)//to reset the bullets prefap
            {
                PlayerShooter.SetShotObject(NormalBlue);
            }
            else if (healthScript.GetID() == 2)
            {
                PlayerShooter.SetShotObject(NormalGreen);
            }
            else if (healthScript.GetID() == 3)
            {
                PlayerShooter.SetShotObject(NormalOrange);
            }
        }
        else if(ID == 3) //Increase Damage
        {
            PlayerDamageDealer.SetDamage(defaultDamage * 2);
            uiUpdater.TimeResetWhenNeeded(1);
            if(healthScript.GetID() == 1)//to change the bullet the bullets prefap
            {
                PlayerShooter.SetShotObject(BlueBuff);
            }
            else if (healthScript.GetID() == 2)
            {
                PlayerShooter.SetShotObject(GreenBuff);
            }
            else if (healthScript.GetID() == 3)
            {
                PlayerShooter.SetShotObject(OrangeBuff);
            }
        }

    }

    public IEnumerator WaitToTakeInfo() //Waiting for the Apply changes for Player Ship Info Damage Image and all
    {
        yield return new WaitForSeconds(0.2f);
        PlayerShooter = GetComponent<Shooter>();            //shooter for fireRate
        PlayerDamageDealer = GetComponent<DamageDealer>();  //DD for Damage
        healthScript = GetComponent<Health>();              //Health for HP 

        maxHP = healthScript.GetHealth();
        defaultFireRate = PlayerShooter.GetMinimumFR(); //getting the D Firerate 
        defaultDamage = PlayerDamageDealer.GetDamage(); //getting the D Damage
    }
}
