using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    
 [SerializeField] TextMeshProUGUI scoreText;
 
 ScoreKeeper scoreKeeper;

void Awake() { //get the scorekeeper
    scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
 }

void Start() { //print of Game Over
    scoreText.text = "FINAL SCORE:\n " + scoreKeeper.GetScore();
 }
}
