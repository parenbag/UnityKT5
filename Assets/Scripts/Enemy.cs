using UnityEngine;

public class Enemy : MonoBehaviour
{
    

    public Vector3 initialPosition;
    //public EnemyManager enemyManager;
    void Start()
    {
        initialPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {

            EnemyManager.Instance.DeactivateEnemy(transform);
            //gameObject.SetActive(false);
            //Destroy(other.gameObject);

            
            
        }
    }
}