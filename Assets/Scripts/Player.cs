using UnityEngine;

public class Player : LivingObject
{
    private Camera playerCamera;
    private PlayerGun playerGun;

    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        playerGun = GetComponentInChildren<PlayerGun>();
    }

    private void Start()
    {
        OnDie.AddListener(OnDeath);
        Health = maxHealth;

        playerGun.damage = damage;
        playerGun.playerCamera = playerCamera.transform;
    }

    private void OnDeath()
    {
        Debug.Log("womp womp");
    }
}
