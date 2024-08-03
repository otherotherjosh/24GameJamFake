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
    [SerializeField] private AudioClip deathSound;
    private AudioSource audioSource;

    public override void AnomalyBehaviour()
    {
        audioSource = GetComponent<AudioSource>();

        gameObject.SetActive(true);
        imageMesh.material = anomalyPaintingImage;
    }

    protected override void OnDeath()
    {
        if (!isEnabled) return;

        base.OnDeath();
        gameObject.SetActive(true);
        imageMesh.material = normalPaintingImage;
        audioSource.PlayOneShot(deathSound);
    }
}