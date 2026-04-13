using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float life = 3f;

    void Update()
    {
        float timeFactor = Time.timeScale; 
        transform.position += transform.forward * speed * timeFactor * Time.deltaTime;

        life -= Time.deltaTime;
        if (life <= 0)
            Destroy(gameObject);
    }
}