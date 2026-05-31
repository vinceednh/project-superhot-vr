using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    public float life = 9f;
    public float hitForce = 8f;

    GameObject owner;
    float ignoreTime = 0.3f;

    public void SetOwner(GameObject ownerObject)
    {
        owner = ownerObject;
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        if (ignoreTime > 0)
            ignoreTime -= Time.unscaledDeltaTime;

        life -= Time.deltaTime;
        if (life <= 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (ignoreTime > 0) return;
        if (owner != null && other.transform.IsChildOf(owner.transform)) return;

        EnemyShooter enemy = other.GetComponentInParent<EnemyShooter>();
        if (enemy != null)
        {
            Rigidbody hitRb = other.GetComponent<Rigidbody>();
            enemy.Die(transform.position, transform.forward, hitForce, hitRb);
            Destroy(gameObject);
            return;
        }

        PlayerHitDetection player = other.GetComponentInParent<PlayerHitDetection>();
        if (player != null)
        {
            player.TakeDamage();
            Destroy(gameObject);
        }
    }
}