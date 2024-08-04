using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class Player : LivingObject
{
    private Camera playerCamera;
    private PlayerGun playerGun;
    private PlayerMovement playerMovement;
    private PlayerRotation playerRotation;
    private Light flashlight;
    private bool flashlightIsOn;
    public bool FlashlightIsOn { get => flashlightIsOn; }

    void Awake()
    {
        flashlight = GetComponentInChildren<Light>();
        playerCamera = GetComponentInChildren<Camera>();
        playerGun = GetComponentInChildren<PlayerGun>();
        playerMovement = GetComponentInChildren<PlayerMovement>();
        playerRotation = GetComponentInChildren<PlayerRotation>();
    }

    private void Start()
    {
        InputSystem.actions.FindAction("Flashlight").performed += ctx => ToggleFlashlight();

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

    void ToggleFlashlight()
    {
        flashlight.enabled = flashlightIsOn;
        flashlightIsOn = !flashlightIsOn;
    }
}
