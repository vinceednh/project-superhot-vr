using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;
    public int currentWave = 0;
    public int enemiesAlive = 0;

    bool waveInProgress = false;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        if (waveInProgress) yield break; // stop if wave already running
        waveInProgress = true;

        yield return new WaitForSeconds(spawnInterval);
        currentWave++;
        SpawnWave(currentWave);
    }

    void SpawnWave(int enemyCount)
    {
        enemiesAlive = enemyCount;
        for (int i = 0; i < enemyCount; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        waveInProgress = false;
    }

    public void OnEnemyDied()
    {
        enemiesAlive = Mathf.Max(0, enemiesAlive - 1);
        if (enemiesAlive <= 0)
            StartCoroutine(StartNextWave()); // only triggers when all dead
    }
}