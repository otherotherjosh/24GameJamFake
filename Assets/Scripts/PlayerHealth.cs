using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingObject
{
    private void Start()
    {
        OnDie.AddListener(OnDeath);
    }

    private void OnDeath()
    {

    }
}
