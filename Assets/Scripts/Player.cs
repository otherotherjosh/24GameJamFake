using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : LivingObject
{
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
        Debug.Log("GUN");
    }
}
