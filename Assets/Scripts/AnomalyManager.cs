using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using Unity.VisualScripting;
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
        spawnCooldown = Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
        lastSpawnTime = Time.time;
        int selection = Random.Range(0, anomalies.Count - 1);
        if(anomalies.Count != 0){
            anomalies[selection].gameObject.SetActive(true);
            anomalies.RemoveAt(selection);
            Debug.Log("spawned a thing");
        }
        else
        {
            Debug.Log("nothing to spawn");
        }
    }
}
