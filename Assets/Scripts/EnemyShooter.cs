using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float fireRate = 2f;
    public float moveSpeed = 2f;
    public float stopDistance = 5f;

    float nextFireTime;
    Transform player;
    Animator animator;
    bool isDead = false;

    void Start()
    {
        player = FindFirstObjectByType<OVRCameraRig>().transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);

        if (distance > stopDistance)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            animator.SetBool("isWalking", true);
            animator.SetBool("isShooting", false);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isShooting", true);

            if (Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || spawnPoint == null) return;
        Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isShooting", false);
        animator.SetBool("Died", true);
        Destroy(gameObject, 3f); // cleanup after death animation
    }
}