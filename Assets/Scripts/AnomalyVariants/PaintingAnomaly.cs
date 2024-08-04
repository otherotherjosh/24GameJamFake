using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//[RequireComponent(typeof(AudioSource))]
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
        if (!isEnabled) return;

        base.OnDeath();
        gameObject.SetActive(true);
        imageMesh.material = normalPaintingImage;
    }
}