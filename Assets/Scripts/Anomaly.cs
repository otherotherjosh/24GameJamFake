using System.Collections;
using UnityEngine;

public class Anomaly : LivingObject
{
    [SerializeField] protected GameObject player;
    [SerializeField] private float shootHeightOffset;
    [SerializeField] protected float bulletOffsetSpawnDistance = 0.1f;
    [HideInInspector] public Bullet bulletPrefab;
    protected AudioSource audioSource;
    private Bullet currentBullet;
    public bool isEnabled;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log($"{name} (ANOMALY) is dying!");
        AnomalyManager.Instance.DisableAnomaly(this);
        StopCoroutine(ShootLoop());
        gameObject.SetActive(false);
        isEnabled = false;
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
            yield return new WaitForSeconds(timeBetweenShots);
            ShootPlayer();
        }
    }

    protected void ShootPlayer()
    {
        Vector3 shootPosition = new Vector3(transform.position.x, transform.position.y + shootHeightOffset, transform.position.z);

        currentBullet = Instantiate(bulletPrefab, shootPosition, Quaternion.identity);
        currentBullet.SetStartParameters(gameObject, player.transform.position + (player.transform.position - shootPosition) * 20, damage, bulletSpeed, bulletOffsetSpawnDistance);
    }

    /// <summary>
    /// This is to be overriden by each anomoly type to add its custom behaviour
    /// </summary>
    public virtual void AnomalyBehaviour() { }
}
