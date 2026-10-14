using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Class for handling UI Elements
public class UIManager : Singleton<UIManager>
{
    public IntVariable gameScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI colorScoreText;
    public GameObject gameOverScreen;
    // public GameObject otherUI;
    // private List<String> colorScoreStrings;

    // Update is called once per frame
    void Update()
    {
        UpdateScoreUI();
    }

    #region Methods for Score UI Updates
    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + gameScore.previousHighestValue.ToString();
    }

    public void UpdateColorScoreUI(int colorIndex)
    {
        String regex = @"alpha=#(\w+)>";
        int count = -1;

        String newColorScoreText = Regex.Replace(colorScoreText.text, regex, replace =>
        {
            count++;
            if (count == colorIndex || replace.Value.Equals("alpha=#FF>")) { return "alpha=#FF>"; }
            else { return "alpha=#00>"; }
        });

        colorScoreText.text = newColorScoreText;
    }
    #endregion

    public void GameOver(Boolean active)
    {
        // otherUI.SetActive(!active);
        gameOverScreen.SetActive(active);
    }

    public void RestartButtonCallback()
    {
        Debug.Log("Restart");
        GameManager.instance.ResetGame();
    }

    public void ResetUI()
    {
        scoreText.text = "Score: 0";
        colorScoreText.text = @"<color=red><alpha=#00>R</color><color=orange><alpha=#00>O</color><color=yellow><alpha=#00>Y</color><color=green><alpha=#00>G</color><color=blue><alpha=#00>B</color><color=#00008b><alpha=#00>I</color><color=purple><alpha=#00>V</color>";
    }
}
