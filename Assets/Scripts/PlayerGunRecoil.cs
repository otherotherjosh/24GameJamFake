using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PlayerGunRecoil : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Vector3 position;
    [SerializeField] private float duration;
    [SerializeField] private float recoverDuration;
    [SerializeField] private Ease rotationEase;
    [SerializeField] private Ease positionEase;
    [SerializeField] private Ease recoverEase;

    void Start()
    {
        transform.DOLocalMove(position, duration/2)
            .SetEase(positionEase)
            .OnComplete(() => 
            transform.DOLocalMove(Vector3.zero, recoverDuration/2)
                .SetEase(recoverEase)
        );
        transform.DOLocalRotate(rotation, duration)
            .SetEase(rotationEase)
            .OnComplete(() => 
            transform.DOLocalRotate(Vector3.zero, recoverDuration)
                .SetEase(recoverEase)
                .OnComplete(EndRecoil)
        );
    }

    void EndRecoil()
    {
        Transform cam = GetComponentInChildren<Camera>().transform;
        cam.parent = transform.parent;
        Destroy(gameObject);
    }
}
