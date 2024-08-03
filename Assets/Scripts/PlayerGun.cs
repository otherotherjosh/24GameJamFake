using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private GameObject recoilPrefab;
    [SerializeField] private GameObject bulletHolePrefab;
    [SerializeField] private int maxBulletHoles;
    [Header("Sounds")]
    [SerializeField] private AudioClip gunshot;
    [SerializeField][Range(0, 0.1f)] private float gunshotVariation;

    [HideInInspector] public Transform playerCamera;
    [HideInInspector] public int damage;

    private GameObject[] bulletHoles;
    private int bulletHolesIndex;
    private AudioSource audioSource;
    private Transform previousRecoilLayer;

    void Awake()
    {
        audioSource = GetComponentInParent<AudioSource>();
        InstantiateBulletHoles();
    }

    private void Start()
    {
        InputSystem.actions.FindAction("Gun").performed += ctx => Gun();
    }

    private void Gun()
    {
        audioSource.pitch = UnityEngine.Random.Range(1 - gunshotVariation / 2, 1 + gunshotVariation / 2);
        audioSource.PlayOneShot(gunshot);

        AddRecoil();

        RaycastHit hit;
        if (!Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, Mathf.Infinity))
            return;

        Anomaly anomaly = hit.transform.GetComponent<Anomaly>();
        if (anomaly == null)
        {
            PlantBulletHole(hit);
            return;
        }

        if (!anomaly.isEnabled)
        {
            PlantBulletHole(hit);
            return;
        }

        anomaly.Health -= damage;
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

    void AddRecoil()
    {
        Debug.Log(previousRecoilLayer == null);
        Transform recoilLayer = Instantiate(recoilPrefab, transform.parent.transform).transform;
        recoilLayer.name = "Recoil Layer";
        if (previousRecoilLayer != null)
            previousRecoilLayer.parent = recoilLayer;
        else
            playerCamera.parent = recoilLayer;
        previousRecoilLayer = recoilLayer;
    }
}
