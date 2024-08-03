using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anomaly : LivingObject
{
    private void Start()
    {
        OnDie.AddListener(OnDeath);
    }

    private void OnDeath()
    {

    }

    /// <summary>
    /// This is to be overriden by each anomoly type to add its custom behaviour
    /// </summary>
    public virtual void AnomalyBehaviour() { }
}
