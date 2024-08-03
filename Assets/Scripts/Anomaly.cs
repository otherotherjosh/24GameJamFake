using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anomaly : LivingObject
{
    [SerializeField] private Vector3 playerPos;
    [SerializeField] private Bullet bulletPrefab;
    private Bullet currentBullet;
    private void Start()
    {
        OnDie.AddListener(OnDeath);
    }

    private void OnDeath()
    {

    }

    protected void ShootPlayer()
    {
        currentBullet = Instantiate(bulletPrefab, transform);
        currentBullet.SetStartParameters(playerPos, damage, bulletSpeed);
    }

    /// <summary>
    /// This is to be overriden by each anomoly type to add its custom behaviour
    /// </summary>
    public virtual void AnomalyBehaviour() { }
}
