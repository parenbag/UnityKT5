using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject scoreLegacyText;
    public GameObject legacyTextGO;

    private bool isGameOver = false;

    void Update()
    {
        // Проверяем, нажата ли клавиша "R" для перезапуска игры
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            // Деактивируем пулю
            other.gameObject.SetActive(false);

            health--;
            UpdateUI();

            if (health <= 0)
            {
                GameOver();
            }
        }
    }

    void UpdateUI()
    {
        switch (health)
        {
            case 2:
                heart1.SetActive(false);
                break;
            case 1:
                heart2.SetActive(false);
                break;
            case 0:
                heart3.SetActive(false);
                scoreLegacyText.SetActive(false);
                legacyTextGO.SetActive(true);
                break;
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f; // Останавливаем время в игре
    }

    void RestartGame()
    {
        Time.timeScale = 1f; // Возобновляем время
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Перезагружаем текущую сцену
    }
}