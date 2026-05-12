using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField]int score = 0;
    public static ScoreKeeper instance;

    [SerializeField] GoalChecker goalChecker;
  
  void Awake()
   {
        
        ManageSingleton();
   }

   void ManageSingleton() // Keep ScoorKeeper on "DontDestroyOnLoad" So we can take it to other Scenes
    {
        if(instance != null && instance != this)// check if already exist
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;

        }
        //build a new one
        
            instance = this;
            DontDestroyOnLoad(gameObject);
    }

   public int GetScore() 
    {
        return score;
    }
    public void ModifyScore(int scoreToAdd) //Update Score
    {
        score+=scoreToAdd;
        score=Mathf.Clamp(score,0,int.MaxValue);
        goalChecker.GoalCheck(score);
        Debug.Log("Goal Cscore updated : " + score);
        
    }
    public void ResetScore() 
    {
        score=0;
    }

    //only purpose to be called when game scene loaded so Exit can work
    public void IntroduceGoalKeeper(GoalChecker ValueScript)  
    {
        goalChecker = ValueScript;
    }
}
