using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    public GameObject enemyPrefab;
    public float spawnInterval = 5f;
    public int currentWave = 0;
    public int enemiesAlive = 0;

    bool waveInProgress = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    Transform[] GetSpawnPoints()
    {
        GameObject spawners = GameObject.Find("Spawners");
        if (spawners == null) return new Transform[0];
        Transform[] points = new Transform[spawners.transform.childCount];
        for (int i = 0; i < spawners.transform.childCount; i++)
            points[i] = spawners.transform.GetChild(i);
        return points;
    }

    IEnumerator StartNextWave()
    {
        if (waveInProgress) yield break;
        waveInProgress = true;

        yield return new WaitForSeconds(spawnInterval);
        currentWave++;
        SpawnWave(currentWave);
    }

    void SpawnWave(int enemyCount)
    {
        Transform[] spawnPoints = GetSpawnPoints();
        if (spawnPoints.Length == 0) return;

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
            StartCoroutine(StartNextWave());
    }

    public void ResetWaves()
    {
        StopAllCoroutines();
        currentWave = 0;
        enemiesAlive = 0;
        waveInProgress = false;
        StartCoroutine(StartNextWave());
    }
}