using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    public float life = 9f;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        life -= Time.unscaledDeltaTime;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}