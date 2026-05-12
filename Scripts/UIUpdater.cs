using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIUpdater : MonoBehaviour
{

    [Header("Scripts needed")]
    [SerializeField] Health playerHealth;
    

    [Header("Health & status")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Image currentPowerUpImage;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Images of the Power-Ups")]
    [SerializeField] Sprite fireRateImage;
    [SerializeField] Sprite extraDamage;


    [Header("Timers")]
    [SerializeField] GameObject FillFR;
    [SerializeField] GameObject FillEXD;
    [SerializeField] Image fillAmountFR;
    [SerializeField] Image fillAmountEXD;
    [SerializeField] float Cooldown = 2.5f;
    float currentTimeFR;
    float currentTimeEXD;

    PowerUpCollecter powerUpCollector;
    [Header("Exit")]
    [SerializeField] TextMeshProUGUI pressToExitText;



    void Start() //get Components
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        powerUpCollector = GameObject.Find("Player").GetComponent<PowerUpCollecter>();

        currentPowerUpImage.sprite = null;
        currentPowerUpImage.enabled = false;

        currentTimeFR = Cooldown;
        currentTimeEXD = Cooldown;

        pressToExitText.enabled = false;

        StartCoroutine(HealthSliderDelay());
    }


    void Update() //Update Health & Score
    {
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
        healthSlider.value = playerHealth.GetHealth();


        if(currentTimeFR != 0 || currentTimeEXD != 0) // Check of the Power Up is on
        {
            TimerDecreasing(); 
        }
        TimeChecker();
        
    }

    public void PowerUpUpdater(int ID) //it only update the sprite on the UI
    {
        if(ID == 0) // when power up used
        {
            currentPowerUpImage.sprite = null;
            currentPowerUpImage.enabled = false;
        }
        else if(ID == 1) // fire rate
        {
            currentPowerUpImage.sprite = fireRateImage;
            currentPowerUpImage.enabled = true;
            Debug.Log("Updater FR triggers");
        }
        else if(ID == 2) // extra damage
        {
            currentPowerUpImage.sprite = extraDamage;
            currentPowerUpImage.enabled = true;
            Debug.Log("Updater EXD triggers");
        }
    }


    private void TimerDecreasing() //Decreasing the power up time with seconds
    {
        if (FillFR.activeSelf) //FireRate
        {
            currentTimeFR -= Time.deltaTime;
            fillAmountFR.fillAmount = currentTimeFR / Cooldown;
        }
        if (FillEXD.activeSelf) // ExtraDamage
        {
            currentTimeEXD -= Time.deltaTime;
            fillAmountEXD.fillAmount = currentTimeEXD / Cooldown;
        }
    }

    void TimeChecker() //Check if the timer reaches 0 Deactivate the Times Image and Reset Timer for later use
    {
        if(currentTimeFR <= 0f && FillFR.activeSelf)
        {
            Debug.Log("FR is Done");
            FillFR.SetActive(false);
            currentTimeFR = Cooldown;
            powerUpCollector.PowerUpEffect(0);
        }
        if(currentTimeEXD <= 0f && FillEXD.activeSelf)
        {
            Debug.Log("EXD is Done");
            FillEXD.SetActive(false);
            currentTimeEXD = Cooldown;
            powerUpCollector.PowerUpEffect(2);
        }
    }
    
    //check for the power up time is still on And used the same powerUp reset Timer
    public void TimeResetWhenNeeded(int ID) 
    {
        if(ID == 0)//FR reset if needed 
        {
            if (FillFR.activeSelf)
            {
                currentTimeFR = Cooldown;
            }
        }
        else if(ID == 1)//EXD rest if needed
        {
            if (FillEXD.activeSelf)
            {
                currentTimeEXD = Cooldown;
            }
        }
    }

    public void ShowingTextOfExit(int ID) // this function to show the Text of An Exit Button to press
    {
        Debug.Log("Showing is recieving Signal");
        if(ID == 0) // hide when player is away 
        {
            pressToExitText.enabled = false;
        }
        else if(ID == 1) // show when player is close
        {
            pressToExitText.enabled = true;
        }
    }
    
    public IEnumerator HealthSliderDelay()
    {
        yield return new WaitForSeconds(0.2f);
        healthSlider.maxValue = playerHealth.GetHealth();
    }
}
