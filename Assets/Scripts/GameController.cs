using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    int m_score;
    bool m_isGameover;

    private UIManager m_ui;
    SpawnerWithResources m_spawnerRs;
    
    // Start is called before the first frame update
    void Start()
    {
        
        m_ui = FindObjectOfType<UIManager>();
        m_ui.SetScoreText("Score: " + m_score);
        m_spawnerRs = FindObjectOfType<SpawnerWithResources>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetIsGameover())
        {
            m_ui.ShowGameOverPanel(GetIsGameover());
            
            // Thông báo cho spawner dừng việc sinh ra đối tượng
            if (m_spawnerRs != null)
            {
                m_spawnerRs.StopSpawning();
            }
        }
        
    }

    public void Replay()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void SetScore(int score)
    {
        m_score += score;
    }

    public int GetScore()
    {
        return m_score;
    }

    public void ScoreIncrease()
    {
        if (m_isGameover)
            return;
        m_ui.SetScoreText("Score: " + GetScore());
    }

    public bool GetIsGameover()
    {
        return m_isGameover;
    }

    public void SetIsGameover(bool isGameover)
    {
        m_isGameover = isGameover;
    }
    
}
