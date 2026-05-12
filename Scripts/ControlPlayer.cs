using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class ControlPlayer : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float leftBoundPadding;
    [SerializeField] float rightBoundPadding;
    [SerializeField] float downBoundPadding;
    [SerializeField] float upBoundPadding;
    [SerializeField] LevelManager LevelManagerScript;
    Exit exitscript;
    Shooter playerShooter;
    PowerUpCollecter CPowerUp;
    InputAction moveAction;
    InputAction fireAction;
    InputAction gameOver;           //for Editor 
    InputAction restartMainMenu;    //for Editor 
    InputAction usePowerUp;
    InputAction exitButton;
    Vector3 moveVector;
    Vector2 minBound;
    Vector2 maxBound;
    void Start() 
    {
        playerShooter = GetComponent<Shooter>();    
        CPowerUp = GetComponent<PowerUpCollecter>();
        exitscript = GameObject.Find("Exit").GetComponent<Exit>();

        moveAction = InputSystem.actions.FindAction("Move");
        fireAction = InputSystem.actions.FindAction("Fire");
        gameOver = InputSystem.actions.FindAction("GameOver");
        restartMainMenu = InputSystem.actions.FindAction("BackToMainMenu");
        usePowerUp = InputSystem.actions.FindAction("UsePower");
        exitButton = InputSystem.actions.FindAction("ExitButton");

        InitBounds();
    }
    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        //Building boundries for the player
        minBound = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBound = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }


    void Update()
    {
        //Keep checking of the player moves and or shoot
        movePlayer(); 
        FireShooter();
        EditorScenesTrigger();
        PowerUpUsed();
        ExitPressed();
    }
    void movePlayer() //Player moved depends on Axis X Y 
    {

        moveVector = moveAction.ReadValue<Vector2>();
        Vector3 newPos = transform.position += moveVector * moveSpeed * Time.deltaTime;
        newPos.x = Math.Clamp(newPos.x, minBound.x + leftBoundPadding, maxBound.x - rightBoundPadding);
        newPos.y = Math.Clamp(newPos.y, minBound.y + downBoundPadding, maxBound.y - upBoundPadding);
        transform.position = newPos;
        //Debug.Log(moveVector);

    }
    void FireShooter() // Player shoots
    {
        playerShooter.isFiring=fireAction.IsPressed();
    }

    void EditorScenesTrigger() // this is only for Editor usage so we can move between scene to easily
    {
        if (gameOver.IsPressed()) // Game Over
    {
        LevelManagerScript.LoadGameOver();
    }
    else if (restartMainMenu.IsPressed()) // Main Menu
        {
            LevelManagerScript.LoadMainMenu();
        }
    }

    void PowerUpUsed() // uses the power up that is stores 
    {
        if (usePowerUp.triggered)
        {
            CPowerUp.PowerTrigger(); // use the PowerUpCollector Script to do the trigger, CpowerUp for Current Power Up
        }
    }
    void ExitPressed()
    {
        if (exitButton.triggered)
        {
            exitscript.ExitChecker();
        }
    }
    
}
