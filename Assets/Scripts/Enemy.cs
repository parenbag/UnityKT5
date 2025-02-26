using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}