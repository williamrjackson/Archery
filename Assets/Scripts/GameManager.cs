using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;

    public static GameManager instance;
    
    public void AddToScore(int newPoints)
    {
        score += newPoints;
        scoreText.text = "Score: " + score;
    }
}
