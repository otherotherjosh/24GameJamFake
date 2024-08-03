using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : LivingObject
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject bulletHolePrefab;
    [SerializeField] private int maxBulletHoles;

    private GameObject[] bulletHoles;
    private int bulletHolesIndex;

    [Header("Sounds")]
    [SerializeField] private AudioClip gunshot;
    [SerializeField][Range(0, 0.1f)] private float gunshotVariation;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        InstantiateBulletHoles();
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
                try
                {
                    Anomaly bob = ((Anomaly)livingObject);

                    if (bob.isEnabled)
                    {
                        livingObject.Health -= damage;
                    }
                    else
                    {
                        PlantBulletHole(hit);
                    }
                }
                catch
                {
                    livingObject.Health -= damage;
                }
            }
            else
            {
                PlantBulletHole(hit);
            }
        }
    }

    void PlantBulletHole(RaycastHit hit)
    {
        bulletHolesIndex %= maxBulletHoles;
        bulletHoles[bulletHolesIndex].transform.position = hit.point;
        bulletHoles[bulletHolesIndex].transform.up = hit.normal;
        bulletHoles[bulletHolesIndex].SetActive(true);
        bulletHoles[bulletHolesIndex].transform.parent = hit.transform;
        bulletHolesIndex++;
    }

    void InstantiateBulletHoles()
    {
        bulletHoles = new GameObject[maxBulletHoles];
        for (int i = 0; i < maxBulletHoles; i++)
        {
            bulletHoles[i] = Instantiate(bulletHolePrefab, transform);
            bulletHoles[i].SetActive(false);
        }
    }
}
