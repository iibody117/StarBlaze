using UnityEngine;
using TMPro;
using UnityEditor;

public class SelectionManager : MonoBehaviour
{

    public static SelectionManager instace;

    [Header("all bullets")]
    [SerializeField] GameObject projectilePrefabBlue;
    [SerializeField] GameObject projectilePrefabGreen;
    [SerializeField] GameObject projectilePrefabOrange;

    [Header("Outside Sources needed")] //Scripts and other things components
    [SerializeField] Health playerHealthScript;
    [SerializeField] Shooter playerShooterScript;
    [SerializeField] DamageDealer shotDDScript;
    [SerializeField] AircraftInfo aircraftInfo;

    //things need when chnaging aircraft
    GameObject chosenProjectile;
    SpriteRenderer PlayerSprite;
    int ChosenID = 0;
    string ChosenColor;
    string ChosenName;
    int ChosenDamage = 0;
    int ChosenHealth =0;
    float ChosenFR = 0.1f;


    [Header("Prefaps needed")] //Prefaps
    [SerializeField] GameObject playerPrefap;
    [SerializeField] GameObject playerShotPrefap;


    [Header("Images of AirCrafts")]
    [SerializeField] Sprite blueAirctaftImage;
    [SerializeField] Sprite greenAirctaftImage;
    [SerializeField] Sprite orangeAirctaftImage;


    [Header("Text Data needed Data To Show")] // all Text Boxes in Scene
    [SerializeField] TextMeshProUGUI shipName;
    [SerializeField] TextMeshProUGUI shipDamage;
    [SerializeField] TextMeshProUGUI shipHealth;
    [SerializeField] TextMeshProUGUI shipFireRate;
    [SerializeField] TextMeshProUGUI shipSelected;
    

    void Start()
    {   
        //set current ID & Color for change ship usage
        ChosenID = playerHealthScript.GetID();
        ChosenColor = playerHealthScript.GetColor();
        ChosenName = playerHealthScript.GetName();
        
        // Get The Player Sprite from the Prefap
        PlayerSprite = playerPrefap.GetComponentInChildren<SpriteRenderer>();

        //Write down the Player Current Ship selected
        shipSelected.text = "SELECTED SPACECRAFT:\n(" + ChosenColor +") " + ChosenName;

        //show the Player Current ship info instead of Default Writing
        ShowInfo(playerHealthScript.GetID());
    }

    public void ShowInfo(int ShipID)
    {
        if(ShipID == 1){ //Blue
            shipName.text = "Name\nGX0993" ; // total 3 point
            shipDamage.text = "Damage\nLow"; // 0 points (Low - Medium - High)       10 DMG
            shipHealth.text = "Health\nMedium" ; // 1 point (Low - Medium - High)    50 HP
            shipFireRate.text = "FIre Rate\nHigh" ; // 2 point (Low - Medium - High) 0.2f

            //ID & Name & Color
            ChosenID = ShipID;
            ChosenName = "GX0993";
            ChosenColor = "Blue";

            //Damage & Health & FR
            chosenProjectile = projectilePrefabBlue;
            ChosenDamage = 10;
            ChosenHealth = 50;
            ChosenFR = 0.2f;

        }
        else if (ShipID == 2){ //green
            shipName.text = "Name\nXF9706" ; // total 4 points
            shipDamage.text = "Damage\nMedium"; // 1 points         20 DMG
            shipHealth.text = "Health\nHigh" ; // 2 point           100 HP
            shipFireRate.text = "FIre Rate\nMedium" ; // 1 point    0.4f

            //ID & Name & Color
            ChosenID = ShipID;
            ChosenName = "XF9706";
            ChosenColor = "Green";

            //Damage & Health & FR
            chosenProjectile = projectilePrefabGreen;
            ChosenDamage = 20;
            ChosenHealth = 100;
            ChosenFR = 0.4f;
        }
        else if (ShipID == 3){ //organe
            shipName.text = "Name\nRA9X22" ; // total 4 points
            shipDamage.text = "Damage\nHigh"; // 2 points           35 DMG
            shipHealth.text = "Health\nHigh" ; // 2 point           100 HP
            shipFireRate.text = "FIre Rate\nLow" ; // 0 point       0.6f

            //ID & Name & Color & FR
            ChosenID = ShipID;
            ChosenName = "RA9X22";
            ChosenColor = "Orange";

            //Damage
            chosenProjectile = projectilePrefabOrange;
            ChosenDamage = 35;
            ChosenHealth = 100;
            ChosenFR = 0.6f;
        }
    }


    public void OnConfirmClick()
    {
        if(ChosenID != aircraftInfo.GetID())
        {
            aircraftInfo.SetPorjectile(chosenProjectile);
            aircraftInfo.SetID(ChosenID);             //SetID
            aircraftInfo.SetName(ChosenName);         //setName
            aircraftInfo.SetColor(ChosenColor);       //SetColor
            aircraftInfo.SetDamage(ChosenDamage);     //SetDamage
            aircraftInfo.SetHealth(ChosenHealth);    //SetHP
            aircraftInfo.SetMinimumFR(ChosenFR);     //SetFR

            ChangeAirCraftAppearance(ChosenID);
            Debug.Log("Aircraft Selected");                 //To confirm changes
            shipSelected.text = "SELECTED SPACECRAFT:\n(" + ChosenColor +") " + ChosenName;
        }
    }

    public void ChangeAirCraftAppearance(int IDIndex)
    {
        if(IDIndex == 1)
        {
            aircraftInfo.SetSprite(blueAirctaftImage);
        }
        else if(IDIndex == 2)
        {
            aircraftInfo.SetSprite(greenAirctaftImage) ;
        }
        else if(IDIndex == 3)
        {
            aircraftInfo.SetSprite(orangeAirctaftImage);
        }
    }
}
