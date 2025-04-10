using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public GameObject gameOverPanel;
    // Start is called before the first frame update

    public void SetScoreText(string score)
    {
        if (scoreText)
        {
            scoreText.text = score;    
        }
    }

    public void ShowGameOverPanel(bool isGameOver)
    {
        if (gameOverPanel)
        {
            gameOverPanel.SetActive(isGameOver);
        }
    }
}
