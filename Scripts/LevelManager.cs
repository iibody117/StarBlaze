using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDeley = 2f;
    [SerializeField] ScoreKeeper scoreKeeper;
    [SerializeField] GSID gsID;

    [SerializeField] GameObject levelsButtons;
    [SerializeField] GameObject startMenuButtons;
    AircraftInfo aircraftInfo;

    private void Awake() {
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
    }
   
    void Start()
    {   
        // when game scene load, find the object and do the changes 
        if(SceneManager.GetActiveScene().name == "GameScene 1" || 
           SceneManager.GetActiveScene().name == "GameScene 2" || SceneManager.GetActiveScene().name == "GameScene 3") 
        {
            aircraftInfo = GameObject.Find("AircraftInfo").GetComponent<AircraftInfo>();
            aircraftInfo.SetChangesOnPlayer();
        }

        if(SceneManager.GetActiveScene().name == "MainMenu") //
        {
            gsID = GameObject.Find("GS(ID)").GetComponent<GSID>();
            startMenuButtons = GameObject.Find("StartMenuButtonGroup");
            levelsButtons = GameObject.Find("LevelsButtons");
            HideLevelsShowMain();
        }
        if(SceneManager.GetActiveScene().name == "GameOver")
        {
            gsID = GameObject.Find("GS(ID)").GetComponent<GSID>();
        }
    }


    public void LoadConrols()
    {
        gsID.SetsceneID(1);
        SceneManager.LoadScene("ControlsShowingScene");
    }
    
    // Getting called from other bottons in unity To load Scenes or Quit.
   public void LoadGame(int ID)
    {
        if(ID == 1){
            gsID.SetsceneID(ID);
            SceneManager.LoadScene("GameScene " + ID.ToString());
            scoreKeeper.ResetScore();
            aircraftInfo.SetChangesOnPlayer();
        }
        else if(ID == 2)
        {
            gsID.SetsceneID(ID);
            SceneManager.LoadScene("GameScene " + ID.ToString());
            scoreKeeper.ResetScore();
            aircraftInfo.SetChangesOnPlayer();
        }
        else if(ID == 3)
        {
            gsID.SetsceneID(ID);
            SceneManager.LoadScene("GameScene " + ID.ToString());
            scoreKeeper.ResetScore();
            aircraftInfo.SetChangesOnPlayer();
        }
    }

   public void LoadGameOver()
    {
        Debug.Log("Check Load Scene");
        StartCoroutine(WaitAndLoad("GameOver", sceneLoadDeley)); // To Load delay between game and gameover screens 
    }

   public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void LoadSelectionScene()
    {
        SceneManager.LoadScene("SelectionScene");
    }

    // A Delay Time after Player Dies
    public IEnumerator WaitAndLoad(string sceneName, float delay)// this is the method that uses the delay
    {
        Debug.Log("Delay Time is working and scene name = "+ sceneName);
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    } 

    public void HideLevelsShowMain()
    {
        levelsButtons.SetActive(false);
        startMenuButtons.SetActive(true);
    }

    public void ShowLevelsHideMain()
    {
        levelsButtons.SetActive(true);
        startMenuButtons.SetActive(false);
    }
    
    public void GameOverPlayAgain()
    {
        Debug.Log("GSID = "+ gsID);
        StartCoroutine(WaitAndLoad("GameScene " + gsID.GetsceneID().ToString(), 0.1f));
    }
}