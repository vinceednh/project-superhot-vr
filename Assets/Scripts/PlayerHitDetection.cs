using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHitDetection : MonoBehaviour
{
    public Transform centerEyeAnchor;
    CapsuleCollider capsule;
    bool isDead = false;

    void Start()
    {
        capsule = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && !isDead)
        {
            Destroy(other.gameObject);
            PlayerDie();
        }
    }

    void PlayerDie()
    {
        isDead = true;
        OVRScreenFade.instance.FadeOut();
        StartCoroutine(RestartAfterDelay());
    }

    System.Collections.IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        OVRScreenFade.instance.FadeIn();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}