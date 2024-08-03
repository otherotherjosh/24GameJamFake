using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 targetPosition;
    private float damage;
    private float bulletSpeed;

    private void Update()
    {
        StepToPosition();
    }

    public void SetStartParameters(Vector3 targetPosition, float damage, float bulletSpeed)
    {
        this.targetPosition = targetPosition;
        this.damage = damage;
        this.bulletSpeed = bulletSpeed;
    }

    private void StepToPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        LivingObject livingObject = collision.gameObject.GetComponent<LivingObject>();

        if (livingObject != null)
        {
            livingObject.Health -= Mathf.RoundToInt(damage);
            Debug.Log("hit something with health");
        }
        else
        {
            Debug.Log("miss");
            Destroy(gameObject);
        }
    }
}
