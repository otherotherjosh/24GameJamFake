using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    public static AnomalyManager Instance;

    //list
    [SerializeField] private List<Anomaly> anomalies = new List<Anomaly>();
    [SerializeField] private float maxTimeBetweenSpawn;
    [SerializeField] private float minTimeBetweenSpawn;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private AudioClip shootSound;
    private List<Anomaly> activeAnomalies = new List<Anomaly>();
    [SerializeField] private float initialGracePeriod;
    [SerializeField] private int anomalyLoseCount;

    private float lastSpawnTime;
    private float spawnCooldown;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        lastSpawnTime = Time.time;
        spawnCooldown = initialGracePeriod;
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
            EnableAnomaly(anomalies[selection]);
            Debug.Log("Enabled en enomaly");
            CheckAnomalyCount();
        }
        else
        {
            //Debug.Log("All anomalies have been enabled");
        }
    }

    public void EnableAnomaly(Anomaly anomalyToEnable)
    {
        anomalyToEnable.bulletPrefab = bulletPrefab;
        anomalyToEnable.gameObject.SetActive(true);
        activeAnomalies.Add(anomalyToEnable);
        anomalyToEnable.StartAnomaly();
        anomalies.Remove(anomalyToEnable);
    }

    public void DisableAnomaly(Anomaly anomalyToDisable)
    {
        activeAnomalies.Remove(anomalyToDisable);
    }

    private void CheckAnomalyCount()
    {
        if (anomalies.Count >= anomalyLoseCount)
        {
            GameManager.Instance.EndGame();
        }
    }
}
