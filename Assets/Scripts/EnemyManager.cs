using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] initialEnemies;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public TMP_Text scoreText;

    private List<Transform> activeEnemies;
    private bool movingRight = true;
    private float boundary = 5f;
    private int score = 0;

    public event Action OnEnemyDeactivated;

    void Start()
    {
        activeEnemies = new List<Transform>(initialEnemies);
        InvokeRepeating("FireRandomEnemy", 2f, 2f);
        UpdateScoreText();
    }

    void Update()
    {
        MoveEnemies();
    }

    void MoveEnemies()
    {
        activeEnemies.RemoveAll(enemy => enemy == null || !enemy.gameObject.activeSelf);

        if (activeEnemies.Count == 0) return;

        float step = speed * Time.deltaTime;
        Vector3 direction = movingRight ? Vector3.right : Vector3.left;

        foreach (Transform enemy in activeEnemies)
        {
            if (enemy != null && enemy.gameObject.activeSelf)
            {
                enemy.Translate(direction * step);
            }
        }

        if (activeEnemies.Count > 0 &&
            (activeEnemies[0].position.x > boundary || activeEnemies[activeEnemies.Count - 1].position.x < -boundary))
        {
            movingRight = !movingRight;
            foreach (Transform enemy in activeEnemies)
            {
                if (enemy != null && enemy.gameObject.activeSelf)
                {
                    enemy.position += Vector3.down;
                }
            }
        }
    }

    public void DeactivateEnemy(Transform enemy)
    {
        Debug.Log("DeactivateEnemy called for: " + enemy.name);
        if (enemy != null && enemy.gameObject.activeSelf)
        {
            enemy.gameObject.SetActive(false);
            activeEnemies.Remove(enemy);
            score += 50;
            UpdateScoreText();
            OnEnemyDeactivated?.Invoke();
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public bool AllEnemiesDeactivated()
    {
        return activeEnemies.Count == 0;
    }

    public void ResetEnemies()
    {
        foreach (Transform enemy in initialEnemies)
        {
            if (enemy != null)
            {
                enemy.gameObject.SetActive(true);
                enemy.position = enemy.GetComponent<Enemy>().initialPosition;
            }
        }
        activeEnemies = new List<Transform>(initialEnemies);
    }

    void FireRandomEnemy()
    {
        activeEnemies.RemoveAll(enemy => enemy == null || !enemy.gameObject.activeSelf);

        if (activeEnemies.Count == 0) return;

        int randomIndex = UnityEngine.Random.Range(0, activeEnemies.Count);
        Transform randomEnemy = activeEnemies[randomIndex];

        GameObject bullet = Instantiate(bulletPrefab, randomEnemy.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * bulletSpeed;
        }
        Destroy(bullet, 4f);
    }
}