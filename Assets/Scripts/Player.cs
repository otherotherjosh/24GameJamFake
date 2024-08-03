using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : LivingObject
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Camera playerCamera;

    private void Start()
    {
        OnDie.AddListener(OnDeath);
        InputSystem.actions.FindAction("Gun").performed += ctx => Gun();
    }

    private void OnDeath()
    {
        Debug.Log("womp womp");
    }

    private void Gun()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(playerCamera.transform.forward) * hit.distance, Color.yellow);
            Instantiate(bulletPrefab, playerCamera.transform.position + playerCamera.transform.forward, Quaternion.identity).SetStartParameters(hit.point, damage, bulletSpeed);
        }
    }
}
