using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuButtons;//All menu buttons
    [SerializeField] private GameObject difficultyButtons;//All menu buttons
    public void StartGame()
    {
        difficultyButtons.SetActive(true);//unhide levels buttons
        menuButtons.SetActive(false);//hide menu buttons
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void VeryEasy()
    {
        DifficultySettings.rows = 2;
        DifficultySettings.columns = 3;
        DifficultySettings.loadPrevious = false;
        SceneManager.LoadScene("GameScene");
    }

    public void Easy()
    {
        DifficultySettings.rows = 2;
        DifficultySettings.columns = 4;
        DifficultySettings.loadPrevious = false;
        SceneManager.LoadScene("GameScene");
    }
    public void Medium()
    {
        DifficultySettings.rows = 3;
        DifficultySettings.columns = 4;
        DifficultySettings.loadPrevious = false;
        SceneManager.LoadScene("GameScene");
    }
    public void Hard()
    {
        DifficultySettings.rows = 4;
        DifficultySettings.columns = 5;
        DifficultySettings.loadPrevious = false;
        SceneManager.LoadScene("GameScene");
    }

    public void VeryHard()
    {
        DifficultySettings.rows = 5;
        DifficultySettings.columns = 6;
        DifficultySettings.loadPrevious = false;
        SceneManager.LoadScene("GameScene");
    }

    public void Back()
    {
        difficultyButtons.SetActive(false);//unhide levels buttons
        menuButtons.SetActive(true);//hide menu buttons
    }

    public void LoadGame()
    {
        if(PlayerPrefs.HasKey("MatchSaved"))
        {
            DifficultySettings.loadPrevious = true;
            SceneManager.LoadScene("GameScene");
        }
    }
}