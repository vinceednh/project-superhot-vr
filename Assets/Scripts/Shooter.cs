using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float shootCooldown = 0.5f;
    public Image reloadIndicator;

    float cooldownTimer = 0f;

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            if (reloadIndicator != null)
                reloadIndicator.fillAmount = 1f - (cooldownTimer / shootCooldown);
        }
        else
        {
            if (reloadIndicator != null)
                reloadIndicator.fillAmount = 1f;
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (cooldownTimer <= 0)
                Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || spawnPoint == null) return;
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        bullet.layer = LayerMask.NameToLayer("PlayerBullet");
        bullet.GetComponent<Bullet>().SetOwner(gameObject);
        cooldownTimer = shootCooldown;
        if (reloadIndicator != null)
            reloadIndicator.fillAmount = 0f;
    }
}