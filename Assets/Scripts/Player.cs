using UnityEngine;

public class Player : LivingObject
{
    private Camera playerCamera;
    private PlayerGun playerGun;
    private PlayerMovement playerMovement;
    private PlayerRotation playerRotation;

    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        playerGun = GetComponentInChildren<PlayerGun>();
        playerMovement = GetComponentInChildren<PlayerMovement>();
        playerRotation = GetComponentInChildren<PlayerRotation>();
    }

    private void Start()
    {
        OnDie.AddListener(OnDeath);
        OnHurt.AddListener(Hurt);
        Health = maxHealth;

        playerGun.damage = damage;
        playerGun.playerCamera = playerCamera.transform;
    }

    private void Hurt()
    {
        Debug.Log("Yeowch!");
    }

    public void OnDeath()
    {
        playerGun.CanGun = false;
        playerMovement.CanMove = false;
        playerRotation.CanMove = false;
    }
}
