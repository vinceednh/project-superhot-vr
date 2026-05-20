using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float life = 3f;

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