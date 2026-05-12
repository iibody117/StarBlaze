using UnityEngine;

public class DelayToGame : MonoBehaviour
{
    LevelManager levelManager;
    [SerializeField] float delayTime = 4f;

    void Start()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
        StartCoroutine(levelManager.WaitAndLoad("GameScene 1" ,delayTime));
    }
}
