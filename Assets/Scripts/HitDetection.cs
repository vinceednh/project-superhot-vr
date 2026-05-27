using UnityEngine;

public class HitDetection : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            GetComponent<EnemyShooter>().Die();
            ScoreManager.instance.AddScore(5);
        }
    }
}