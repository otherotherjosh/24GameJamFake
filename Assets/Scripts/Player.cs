using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : LivingObject
{
    [SerializeField] private Camera playerCamera;
    [Header("Sounds")]
    [SerializeField] private AudioClip gunshot;
    [SerializeField][Range(0,0.1f)] private float gunshotVariation;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        OnDie.AddListener(OnDeath);
        InputSystem.actions.FindAction("Gun").performed += ctx => Gun();
        Health = maxHealth;
    }

    private void OnDeath()
    {
        Debug.Log("womp womp");
    }

    private void Gun()
    {
        audioSource.pitch = Random.Range(1 - gunshotVariation / 2, 1 + gunshotVariation / 2);
        audioSource.PlayOneShot(gunshot);

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(playerCamera.transform.forward) * hit.distance, Color.yellow);
            //Instantiate(bulletPrefab, playerCamera.transform.position + playerCamera.transform.forward, Quaternion.identity).SetStartParameters(hit.point, damage, bulletSpeed);

            LivingObject livingObject = hit.transform.GetComponentInParent<LivingObject>();
            if (livingObject != null)
            {
                livingObject.Health -= damage;
            }
        }
    }
}
