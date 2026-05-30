using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text waveText;
    public TMP_Text enemiesText;

    WaveManager waveManager;

    void Start()
    {
        waveManager = FindAnyObjectByType<WaveManager>();
    }

    void Update()
    {
        waveText.text   = "Wave: "     + waveManager.currentWave;
        enemiesText.text = "Enemies: " + waveManager.enemiesAlive;
        scoreText.text  = "Score: "    + ScoreManager.instance.GetScore();
        
    }
}