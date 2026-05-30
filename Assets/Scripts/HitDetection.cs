using UnityEngine;

public class HitDetection : MonoBehaviour
{
    bool hit = false;

    void OnTriggerEnter(Collider other)
    {
        if (hit) return;
        if (other.CompareTag("Bullet"))
        {
            hit = true;
            Vector3 hitPoint = other.transform.position;
            Vector3 hitDirection = other.transform.forward;
            Rigidbody hitRb = GetComponent<Rigidbody>();

            Destroy(other.gameObject);

            EnemyShooter enemy = GetComponentInParent<EnemyShooter>();
            if (enemy != null)
                enemy.Die(hitPoint, hitDirection, 5f, hitRb);

            if (ScoreManager.instance != null)
                ScoreManager.instance.AddScore(1);

            WaveManager waveManager = FindAnyObjectByType<WaveManager>();
            if (waveManager != null)
                waveManager.OnEnemyDied();
        }
    }
}