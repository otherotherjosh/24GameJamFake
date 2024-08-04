using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using DG.Tweening;

public class FollowAnomaly : Anomaly
{
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] private float followSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private LayerMask rayIgnoreMask;

    public override void AnomalyBehaviour()
    {
        StartCoroutine(FollowPlayer());
        transform.position = spawnTransform.position;
    }

    private IEnumerator FollowPlayer()
    {
        while (isEnabled)
        {
            yield return null;

            if (!Physics.Linecast(transform.position + new Vector3(0, 1, 0), player.transform.position + new Vector3(0, 1, 0), ~rayIgnoreMask))
            {
                float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(player.transform.position.x, player.transform.position.z));

                transform.LookAt(player.transform.position);

                Vector3 eulerAngles = transform.eulerAngles;
                transform.rotation = Quaternion.Euler(new Vector3(0, eulerAngles.y, 0));

                if (distance > distanceFromPlayer)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, 0, player.transform.position.z), followSpeed * Time.deltaTime);
                }
            }
        }
    }

    protected override void OnDeath()
    {
        isEnabled = false;
        AnomalyManager.Instance.DisableAnomaly(GetComponent<Anomaly>());
        transform.LookAt(player.transform.position);
        transform.DOLocalRotate(new Vector3(-90, transform.localEulerAngles.y), 1)
            .SetEase(Ease.OutExpo)
            .onComplete = Disable;
    }

    void Disable()
    {
        gameObject.SetActive(false);
        transform.position = spawnTransform.position;
    }
}