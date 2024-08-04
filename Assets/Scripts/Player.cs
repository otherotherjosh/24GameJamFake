using UnityEngine;

public class Player : LivingObject
{
    [SerializeField] private AudioClip hurtSound;
    private Camera playerCamera;
    private PlayerGun playerGun;
    private PlayerMovement playerMovement;
    private PlayerRotation playerRotation;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerCamera = GetComponentInChildren<Camera>();
        playerGun = GetComponentInChildren<PlayerGun>();
        playerMovement = GetComponentInChildren<PlayerMovement>();
        playerRotation = GetComponentInChildren<PlayerRotation>();
    }

    private void Start()
    {
        Health = maxHealth;

        playerGun.damage = damage;
        playerGun.playerCamera = playerCamera.transform;
    }

    protected override void Hurt()
    {
        base.Hurt();
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(hurtSound, 0.35f);
        Debug.Log("\"Yeowch!\" ~ Player");
    }

    protected override void Die()
    {
        playerGun.CanGun = false;
        playerMovement.CanMove = false;
        playerRotation.CanMove = false;
        GameManager.Instance.EndGame();
        base.Die();
    }

    protected override void Heal()
    {
        base.Heal();
        Debug.Log("Player is healing!!");
    }
}
