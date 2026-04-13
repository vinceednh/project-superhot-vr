using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}