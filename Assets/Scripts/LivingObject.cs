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
            health = Mathf.Clamp(value, 0, maxHealth);
            if (health == 0)
            {
                OnDie?.Invoke();
            }
        }
    }

    [SerializeField] protected int maxHealth;
    [SerializeField] protected int damage;
    [SerializeField] protected int fireRate;
    [SerializeField] protected float bulletSpeed;
}
