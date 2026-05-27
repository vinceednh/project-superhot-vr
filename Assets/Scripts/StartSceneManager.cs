using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class StartSceneManager : MonoBehaviour
{
    public CanvasGroup titleGroup;
    public float fadeDuration = 2f;

    void Start()
    {
        titleGroup.alpha = 0f;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            titleGroup.alpha = elapsed / fadeDuration;
            yield return null;
        }
        titleGroup.alpha = 1f;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}