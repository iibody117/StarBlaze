using UnityEngine;


public class Exit : MonoBehaviour
{
    LevelManager levelManager;
    UIUpdater uIUpdater;
    CircleCollider2D playerCircleHitBox;
    bool playerIsIn;

    
 
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        playerCircleHitBox = GameObject.Find("Player").GetComponent<CircleCollider2D>();
        uIUpdater = GameObject.Find("Canvas").GetComponent<UIUpdater>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // only works when the exit is touched by player Circle Collider
        if(other.gameObject.name == "Player" && other == playerCircleHitBox) 
        {
            uIUpdater.ShowingTextOfExit(1);
            playerIsIn = true;
        }
    }
   
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name == "Player" && other == playerCircleHitBox) 
        {
            uIUpdater.ShowingTextOfExit(0);
            playerIsIn = false;
        }
    }

    public void ExitChecker() // if the player is in the Exit Sign it will Load game over with pressed button E
    {
        Debug.Log("player is in = " + playerIsIn);
        if (playerIsIn)
        {
            levelManager.LoadGameOver();
        }
    }
}
