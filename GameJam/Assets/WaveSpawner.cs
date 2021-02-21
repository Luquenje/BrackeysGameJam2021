using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Wave{
    public string waveName;
    public int noOfEnemies;
    public GameObject enemy;
    public float spawnInterval;
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;

    private Wave currentWave;
    private int currentWaveNumber;
    private bool canSpawn = true;
    private float nextSpawnTime = 0;
    public TextMeshProUGUI text;
    public GameObject waveComplete;
    public AudioSource wavedone;
    public AudioClip wave1;
    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length == 0 & !canSpawn)
        {
            wavedone.PlayOneShot(wave1, 1f);
            waveComplete.SetActive(false);
            waveComplete.SetActive(true);
            text.text = "Wave: " + waves[currentWaveNumber + 1].waveName;
            SpawnNextWave();
        }
    }

    void SpawnNextWave(){
        currentWaveNumber++;
        canSpawn = true;

    }

    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(currentWave.enemy, randomPoint.position, Quaternion.identity);
        currentWave.noOfEnemies--;
        nextSpawnTime = Time.time + currentWave.spawnInterval;
        if (currentWave.noOfEnemies == 0){
            canSpawn = false;
        }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
