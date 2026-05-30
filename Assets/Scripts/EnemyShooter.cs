using UnityEngine;
using UnityEngine.AI;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float stopDistance = 5f;

    Transform player;
    Animator animator;
    NavMeshAgent agent;
    Rigidbody[] rBodies;
    bool isDead = false;

    void Start()
    {
        OVRCameraRig rig = FindAnyObjectByType<OVRCameraRig>();
        if (rig != null)
            player = rig.centerEyeAnchor;
        else
            player = Camera.main.transform;
        
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        rBodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rBodies)
        {
            rb.isKinematic = true;
            rb.linearDamping = 2f;
        }
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
                    Time.deltaTime * 5f
                );
        }
    }

    public void EnemyFire()
    {
        if (bulletPrefab == null || spawnPoint == null) return;
        Vector3 directionToPlayer = (player.position - spawnPoint.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(directionToPlayer);
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, rotation);
        bullet.layer = LayerMask.NameToLayer("EnemyBullet");
    }

    public void Die(Vector3 hitPoint, Vector3 hitDirection, float force, Rigidbody hitRb)
    {
        if (isDead) return;
        isDead = true;
        agent.enabled = false;
        animator.enabled = false;

        foreach (Rigidbody rb in rBodies)
        {
            rb.isKinematic = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Rigidbody target = hitRb ?? rBodies[0];
        if (target != null)
            target.AddForce(hitDirection * force, ForceMode.Impulse);

        Destroy(gameObject, 3f);
    }
}