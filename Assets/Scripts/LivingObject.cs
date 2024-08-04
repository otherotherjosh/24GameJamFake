using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class LivingObject : MonoBehaviour
{
    [HideInInspector] public UnityEvent<LivingObject> OnDie = new UnityEvent<LivingObject>();
    [HideInInspector] public UnityEvent<LivingObject> OnHeal = new UnityEvent<LivingObject>();
    [HideInInspector] public UnityEvent<LivingObject> OnHurt = new UnityEvent<LivingObject>();

    private int health;

    public int Health
    {
        get => health;
        set
        {
            int oldHealth = health;

            health = Mathf.Clamp(value, 0, maxHealth);
            if (health == 0) Die();
            if (oldHealth > health) Hurt();
            if (oldHealth < health) Heal();
        }
    }

    protected virtual void Die()
    {
        OnDie?.Invoke(this);
        Debug.Log($"{name} is dead.");
    }

    protected virtual void Heal()
    => OnHeal?.Invoke(this);

    protected virtual void Hurt()
    => OnHurt?.Invoke(this);

    public AudioClip deathSound;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int damage;
    [SerializeField] protected int timeBetweenShots = 1;
    [SerializeField] protected float bulletSpeed;
}
