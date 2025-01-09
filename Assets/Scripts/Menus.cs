using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menus : MonoBehaviour
{
    public static Menus instance;
    
    [SerializeField]
    GameObject gameOverBG;
    [SerializeField]
    private float gameOverTime;
    [SerializeField]
    GameObject nextLevelBG;
    [SerializeField]
    private float nextLevelTime;
    [SerializeField]
    private TextMeshProUGUI gameOverScoreText;
    [SerializeField]
    private TextMeshProUGUI highScoreText;
    [SerializeField]
    private GameObject loadingScreen;

    [SerializeField]
    LeanTweenType gameOverAnimation;
    [SerializeField]
    LeanTweenType nextLevelAnimation;

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
    private void Start()
    {
        LeanTween.moveLocalX(loadingScreen, -1950f, 0.4f).setEase(nextLevelAnimation);
    }
    public void NextLevel()
    {
        LeanTween.moveLocalY(nextLevelBG, 0f, nextLevelTime).setEase(nextLevelAnimation).setOnComplete(() =>
        {
            AllEnemies.instance.RestartGame();
            LeanTween.moveLocalY(nextLevelBG, 1200f, nextLevelTime).setEase(nextLevelAnimation);
        });
    }

    public void GameOver()
    {
        gameOverScoreText.text = "Tu puntuación fue: " + LivesandPoints.instance.score.ToString();
        highScoreText.text = "Puntuación más alta fue: " + PlayerPrefs.GetInt("highScore").ToString();
        LeanTween.moveLocalY(gameOverBG, 0f, gameOverTime).setEase(gameOverAnimation);
    }

    public void Restart()
    {
        AllEnemies.instance.RestartGame();
        LivesandPoints.instance.RestartPointsLives();
        LeanTween.moveLocalY(gameOverBG, 1200f, gameOverTime).setEase(gameOverAnimation);
    }
}
