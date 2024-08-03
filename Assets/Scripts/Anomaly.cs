using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anomaly : LivingObject
{
    [SerializeField] private Vector3 playerPos;
    [HideInInspector] public Bullet bulletPrefab;
    private Bullet currentBullet;
    private bool isEnabled;
    private void Start()
    {
        OnDie.AddListener(OnDeath);
    }

    protected virtual void OnDeath()
    {
        isEnabled = false;
        gameObject.SetActive(false);
        StopCoroutine(ShootLoop());
    }

    public void StartAnomaly()
    {
        isEnabled = true;
        Health = maxHealth;

        AnomalyBehaviour();
        StartCoroutine(ShootLoop());
    }

    private IEnumerator ShootLoop()
    {
        while (isEnabled)
        {
            yield return new WaitForSeconds(fireRate);
            ShootPlayer();
        }
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
