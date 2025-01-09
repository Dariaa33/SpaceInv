using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesandPoints : MonoBehaviour
{
    public static LivesandPoints instance;

    public int maxLives;
    public int enemiesPoints;
    public int lives;
    public int highScore;
    public int score;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        lives = maxLives;
        score = 0;
    }

    public void Score()
    {
        score = score + enemiesPoints;
        scoreText.text = score.ToString();
        if (PlayerPrefs.HasKey("highScore"))
        {
            if (score > PlayerPrefs.GetInt("highScore"))
            {
                highScore = score;
                PlayerPrefs.SetInt("highScore", highScore);
                PlayerPrefs.Save();
            }
        }
        else
        {
            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("highScore", highScore);
                PlayerPrefs.Save();
            }
        }
    }

    public void DamageTaken()
    {
        lives = lives - 1;
        if (lives <= 0)
        {
            Menus.instance.GameOver();
        }
    }

    public void RestartPointsLives()
    {
        lives = maxLives;
        score = 0;
        scoreText.text = "0";
    }
}
