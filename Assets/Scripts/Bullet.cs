using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 targetPosition;
    private float damage;
    private float bulletSpeed;
    private GameObject shotBy;
    private float shootDistance;

    private void Update()
    {
        StepToPosition();
    }

    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    public void SetStartParameters(GameObject shotBy, Vector3 targetPosition, float damage, float bulletSpeed, float shootDistance = 0.1f)
    {
        this.shotBy = shotBy;
        this.targetPosition = targetPosition;
        this.damage = damage;
        this.bulletSpeed = bulletSpeed;
        this.shootDistance = shootDistance;

        // This just gets it out of hitting walls or whatever for painting things
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, shootDistance);
    }

    private void StepToPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    { 
        // if shooting anomaly ignore and continue
        if (collision.gameObject.GetComponentInParent<Anomaly>() != null) return;

        Player playerShot = collision.gameObject.GetComponentInParent<Player>();

        if (playerShot != null)
        {
            playerShot.Health -= Mathf.RoundToInt(damage);
            //Debug.Log($"hit something with health health is now {livingObject.Health}");
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
