using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class LivingObject : MonoBehaviour
{
    public UnityEvent OnDie = new UnityEvent();
    public UnityEvent OnHeal = new UnityEvent();
    public UnityEvent OnHurt = new UnityEvent();

    private int health;

    public int Health {
        get => health;
        set {
            int oldHealth = health;   

            health = Mathf.Clamp(value, 0, maxHealth);
            if (health == 0) OnDie?.Invoke();
            if (oldHealth > health) OnHurt?.Invoke();
            if (oldHealth < health) OnHeal?.Invoke();
        }
    }

    [SerializeField] protected int maxHealth;
    [SerializeField] protected int damage;
    [SerializeField] protected int fireRate;
    [SerializeField] protected float bulletSpeed;
}
