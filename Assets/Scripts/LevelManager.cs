using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public void NextLevel(int currentRows, int currentColumns)
    {
        if (currentRows == 2 && currentColumns == 3)            //Move from Very Easy -> Easy
        {
            DifficultySettings.rows = 2;
            DifficultySettings.columns = 4;
        }
        else if (currentRows == 2 && currentColumns == 4)     //Move from Easy -> Medium
        {
            DifficultySettings.rows = 3;
            DifficultySettings.columns = 4;
        }
        else if (currentRows == 3 && currentColumns == 4)     //Move from Medium -> Hard
        {
            DifficultySettings.rows = 4;
            DifficultySettings.columns = 5;
        }
        else if (currentRows == 4 && currentColumns == 5)     //Move from Hard -> Very Hard
        {
            DifficultySettings.rows = 5;
            DifficultySettings.columns = 6;
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
            return;
        }


        DifficultySettings.loadPrevious = false;
        SceneManager.LoadScene("GameScene");
    }
}
