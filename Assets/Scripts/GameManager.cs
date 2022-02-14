using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameStatus;
    private int score;
    public bool gameStop = false;

    void Start()
    {
        UpdateScore(0); // At start score is zero
        gameStatus.gameObject.SetActive(false); // Game Over text disabled
    }
    private void Update()
    {
        MakeTheGameStop();
    }
    public void UpdateScore(int addToScore) // Updating the player score
    {
        score += addToScore;
        scoreText.text = "Score: " + score;
    }
    private void MakeTheGameStop() // Updating the Game status
    {
        if (gameStop == true)
        {
            gameStatus.gameObject.SetActive(true);
        }
    }
}
