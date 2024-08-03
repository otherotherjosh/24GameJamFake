using UnityEngine;
using DG.Tweening;
using System.Collections;

public class TransformAnomaly : Anomaly
{
    [SerializeField] private GameObject originalObject;
    [SerializeField] private Transform anomalousTransform;
    [SerializeField] private float transformTime;
    [SerializeField] Ease ease;
    private bool isTransforming;

    private GameObject dupeObject;

    public override void AnomalyBehaviour()
    {
        dupeObject = Instantiate(originalObject, originalObject.transform.position, originalObject.transform.rotation, transform);
        originalObject.SetActive(false);
        TransformOverTime(
            anomalousTransform.position,
            anomalousTransform.rotation,
            anomalousTransform.localScale,
            transformTime
        );
    }

    void TransformOverTime(Vector3 position, Quaternion rotation, Vector3 scale, float time)
    {
        dupeObject.transform.DOMove(position, time).SetEase(ease);
        dupeObject.transform.DORotate(rotation.eulerAngles, time).SetEase(ease);
        dupeObject.transform.DOScale(scale, time).SetEase(ease)
        .onComplete = () => { if (isTransforming) isTransforming = false; };
    }

    protected override void OnDeath()
    => StartCoroutine(Die());

    IEnumerator Die()
    {
        isTransforming = true;
        TransformOverTime(
            originalObject.transform.position,
            originalObject.transform.rotation,
            originalObject.transform.localScale,
            transformTime / 2
        );
        yield return null;
        yield return new WaitUntil(() => !isTransforming);

        originalObject.SetActive(true);
        base.OnDeath();
    }
}
