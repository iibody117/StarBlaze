using UnityEngine;
using TMPro;

public class GoalChecker : MonoBehaviour
{
    public static GoalChecker instance;
    [SerializeField] int goalScore = 0;
    [SerializeField] TextMeshProUGUI goalText;
    bool goalAchievement;

    SpriteRenderer exitSpriteRenderer;
    BoxCollider2D exitBoxCollider2D;
    ScoreKeeper scoreKeeper;


    void Start()
    {
        goalAchievement = false;
        exitSpriteRenderer = GameObject.Find("Exit").GetComponent<SpriteRenderer>();
        exitBoxCollider2D = GameObject.Find("Exit").GetComponent<BoxCollider2D>();
        scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();

        

        exitSpriteRenderer.enabled = false; //set exit sprite as Deactivated
        exitBoxCollider2D.enabled = false; //set exit collider as Deactivated

        

        //check if the scorekeeper can't find goalChecker because it's DontDestroyOnLoad
        if (ScoreKeeper.instance != null) 
        {
            ScoreKeeper.instance.IntroduceGoalKeeper(this);// This as This Script
        }

        SetGoalText();
    }
 
    public void GoalCheck(int  currenScore) //when goal Reached Activate sprite and collider of exit
    {
        if(goalScore <= currenScore && !goalAchievement)
        {
            goalAchievement = true;
            Debug.Log("Goal Reached");
            goalText.text = "GOAL: Reached";
            goalText.color = Color.green;

            exitSpriteRenderer.enabled = true; 
            exitBoxCollider2D.enabled = true;
            
        }
    }

    public void SetGoalText()// to write down the Goal Score
    {
        goalText.text ="GOAL: " + goalScore;
        Debug.Log("Goal has be set");
    }
}
