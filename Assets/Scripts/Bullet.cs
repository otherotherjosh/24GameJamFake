using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 targetPosition;
    private float damage;
    private float bulletSpeed;
    private GameObject shotBy;

    private void Update()
    {
        StepToPosition();
    }

    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    public void SetStartParameters(GameObject shotBy, Vector3 targetPosition, float damage, float bulletSpeed)
    {
        this.shotBy = shotBy;
        this.targetPosition = targetPosition;
        this.damage = damage;
        this.bulletSpeed = bulletSpeed;

        // This just gets it out of hitting walls or whatever for painting things
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 1);
    }

    private void StepToPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == shotBy) return;

        LivingObject livingObject = collision.gameObject.GetComponentInParent<LivingObject>();

        if (livingObject != null)
        {
            livingObject.Health -= Mathf.RoundToInt(damage);
            Debug.Log($"hit something with health health is now {livingObject.Health}");
        }

        StopCoroutine(DestroyBullet());
        Die();
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(60);
        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
        StopCoroutine(DestroyBullet());
    }
}
