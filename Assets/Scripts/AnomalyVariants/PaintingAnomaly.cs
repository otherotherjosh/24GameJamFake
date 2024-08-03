using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingAnomaly : Anomaly
{
    [SerializeField] private Material normalPaintingImage;
    [SerializeField] private Material anomalyPaintingImage;
    [SerializeField] private MeshRenderer imageMesh;

    public override void AnomalyBehaviour()
    {
        gameObject.SetActive(true);
        imageMesh.material = anomalyPaintingImage;
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        gameObject.SetActive(true);
        imageMesh.material = normalPaintingImage;
    }
}