using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game_UI_Controller : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private Line endLine;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private TextMeshProUGUI endGameText;

    public void ShowEndGameWindow(bool win, Color[] correctColors)
    {
        for (int i = 0; i < 3; i++)
        {
            endLine.SetCellColor(i, correctColors[i]);
        }

        endGameText.text = win ? "YOU WIN" : "YOU LOSE";
        endGamePanel.SetActive(true);
    }

    public void ChangeScenePressed(string sceneName) => Scene_Loader.LoadSceneByName(sceneName);
}
