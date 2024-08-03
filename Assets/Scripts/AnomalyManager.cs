using System.Collections.Generic;
using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    //list
    [SerializeField] private List<Anomaly> anomalies;
    [SerializeField] private float maxTimeBetweenSpawn;
    [SerializeField] private float minTimeBetweenSpawn;
    private float lastSpawnTime;
    private float spawnCooldown;

    void Start()
    {
        lastSpawnTime = Time.time;
        spawnCooldown = Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
    }

    void Update()
    {
        if (Time.time - lastSpawnTime >= spawnCooldown)
        {
            SpawnAnomaly();
        }
    }

    //pick an anomoly and spawn 
    private void SpawnAnomaly()
    {
        Debug.Log("spawned a thing");
        spawnCooldown = Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
        lastSpawnTime = Time.time;
    }
}
