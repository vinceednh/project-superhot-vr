using UnityEngine;

public class PlayerHitDetection : MonoBehaviour
{
    public Transform centerEyeAnchor;
    CapsuleCollider capsule;

    void Start()
    {
        capsule = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (centerEyeAnchor != null)
        {
            Vector3 localHead = transform.InverseTransformPoint(centerEyeAnchor.position);
            capsule.center = new Vector3(0, localHead.y, localHead.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            PlayerDie();
        }
    }

    void PlayerDie()
    {
        Debug.Log("Player died!");
        // TODO: game over screen
    }
}