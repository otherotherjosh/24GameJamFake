using UnityEngine;

public class TransformAnomaly : Anomaly
{
    [SerializeField] private GameObject originalObject;
    [SerializeField] private Transform anomalousTransform;
    private GameObject dupeObject;

    public override void AnomalyBehaviour()
    {
        dupeObject = Instantiate(originalObject, originalObject.transform.position, originalObject.transform.rotation, transform);
        originalObject.SetActive(false);
        dupeObject.transform.position = anomalousTransform.position;
        dupeObject.transform.rotation = anomalousTransform.rotation;
        dupeObject.transform.localScale = anomalousTransform.localScale;
    }

    protected override void OnDeath()
    {
        originalObject.SetActive(true);
        base.OnDeath();
    }
}
