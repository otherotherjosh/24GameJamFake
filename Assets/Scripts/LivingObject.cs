using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class LivingObject : MonoBehaviour
{
    protected UnityEvent OnDie = new UnityEvent();

    private int health;
    public int Health {
        get => health;
        set {
            health = Mathf.Clamp(value, 0, MaxHealth);
            if (health == 0)
            {
                OnDie?.Invoke();
            }
        }
    }
    private int maxHealth;
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
}

