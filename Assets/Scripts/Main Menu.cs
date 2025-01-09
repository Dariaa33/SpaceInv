using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private LeanTweenType loadLevelAnimation;

    public void StartGame()
    {
        LeanTween.moveLocalX(loadingScreen, 0f, 0.4f).setEase(loadLevelAnimation).setOnComplete(() =>
        {
            SceneManager.LoadScene("Level");
        });
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
