using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text waveText;
    public TMP_Text enemiesText;

    void Update()
    {
        if (WaveManager.instance != null)
        {
            waveText.text = "Wave: " + WaveManager.instance.currentWave;
            enemiesText.text = "Enemies: " + WaveManager.instance.enemiesAlive;
        }
        if (ScoreManager.instance != null)
            scoreText.text = "Score: " + ScoreManager.instance.GetScore();
    }
}