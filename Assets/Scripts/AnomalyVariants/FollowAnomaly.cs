using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowAnomaly : Anomaly
{
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] private float followSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float XZAngleClamp;

    public override void AnomalyBehaviour()
    {
        StartCoroutine(FollowPlayer());
    }

    private IEnumerator FollowPlayer()
    {
        while (isEnabled) 
        { 
            yield return null;

            if (!Physics.Linecast(transform.position, player.transform.position))
            {
                float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(player.transform.position.x, player.transform.position.z));

                transform.LookAt(player.transform.position);

                Vector3 eulerAngles = transform.eulerAngles;
                transform.rotation = Quaternion.Euler(new Vector3(Mathf.Clamp(eulerAngles.x, -XZAngleClamp, XZAngleClamp), eulerAngles.y, Mathf.Clamp(eulerAngles.z, -XZAngleClamp, XZAngleClamp)));

                if (distance > distanceFromPlayer)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, 0, player.transform.position.z), followSpeed * Time.deltaTime);
                }
            }
        }
    }

    protected override void OnDeath()
    {
        base.OnDeath();

        // GO back to start position
    }
}
