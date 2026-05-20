using UnityEngine;
using UnityEngine.AI;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float fireRate = 2f;
    public float stopDistance = 5f;

    float nextFireTime;
    Transform player;
    Animator animator;
    NavMeshAgent agent;
    bool isDead = false;

    void Start()
    {
        OVRCameraRig rig = FindAnyObjectByType<OVRCameraRig>();
        player = rig.centerEyeAnchor;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            agent.isStopped = false;
            agent.updateRotation = false;
            agent.SetDestination(player.position);
            animator.SetBool("isWalking", true);
            animator.SetBool("isShooting", false);

            // rotate to match movement direction
            if (agent.velocity.sqrMagnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
        else
        {
            agent.isStopped = true;
            agent.updateRotation = false;
            animator.SetBool("isWalking", false);
            animator.SetBool("isShooting", true);

            Vector3 faceDirection = player.position - transform.position;
            faceDirection.y = 0;
            if (faceDirection != Vector3.zero)
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(faceDirection),
                    Time.unscaledDeltaTime * 5f
                );

            if (Time.unscaledTime >= nextFireTime)
            {
                nextFireTime = Time.unscaledTime + fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || spawnPoint == null) return;

        Vector3 directionToPlayer = (player.position - spawnPoint.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(directionToPlayer);
        Instantiate(bulletPrefab, spawnPoint.position, rotation);
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        agent.enabled = false;
        animator.SetBool("isWalking", false);
        animator.SetBool("isShooting", false);
        animator.SetBool("Died", true);
        Destroy(gameObject, 3f);
    }
}   