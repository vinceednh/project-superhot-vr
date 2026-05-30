using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;

public class EndSceneManager : MonoBehaviour
{
    public CanvasGroup endGroup;
    public TMP_Text scoreText;
    public TMP_Text waveText; 
    public float fadeDuration = 2f;

    void Start()
    {
        endGroup.alpha = 0f;
        
        scoreText.text = "score: " + ScoreManager.instance.GetScore();
        
        WaveManager waveManager = FindAnyObjectByType<WaveManager>();
        if (waveManager != null)
            waveText.text = "wave: " + waveManager.currentWave;
        
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            endGroup.alpha = elapsed / fadeDuration;
            yield return null;
        }
        endGroup.alpha = 1f;
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One) || Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            PlayAgain();
        }
    }

    public void PlayAgain()
    {
        if (ScoreManager.instance != null)
            ScoreManager.instance.ResetScore();
        SceneManager.LoadScene(1);
    }
}