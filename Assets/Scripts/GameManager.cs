using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public EnemyManager enemyManager;
    public TMP_Text scoreText;
    public GameObject wonPanel;

    private int score = 0;
    private bool gameWon = false;

    void Start()
    {
        enemyManager.OnEnemyDeactivated += AddScore;
        wonPanel.SetActive(false);
    }

    void Update()
    {
        if (enemyManager.AllEnemiesDeactivated() && !gameWon)
        {
            GameWon();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    void AddScore()
    {
        score += 50;
        scoreText.text = "Score: " + score;
    }

    void GameWon()
    {
        gameWon = true;
        wonPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        score = 0;
        scoreText.text = "Score: " + score;
        wonPanel.SetActive(false);
        gameWon = false;
        enemyManager.ResetEnemies();
    }
}