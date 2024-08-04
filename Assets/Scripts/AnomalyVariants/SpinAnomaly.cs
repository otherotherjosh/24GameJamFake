using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using DG.Tweening;
using Unity.VisualScripting;

public class SpinAnomaly : Anomaly
{
    [SerializeField] private float rotateSpeed;

    private Vector3 insanityDriver = Vector3.zero;

    public override void AnomalyBehaviour()
    {
        
    }

    private void FixedUpdate()
    {
        if (!isEnabled) return;

        insanityDriver = new Vector3(GetRandomRotation(insanityDriver.x), GetRandomRotation(insanityDriver.x), GetRandomRotation(insanityDriver.x));

        float rotation = (insanityDriver.x + insanityDriver.y + insanityDriver.z);

        Vector3 currentRot = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(currentRot.x, rotation, currentRot.z));
    }

    private float GetRandomRotation(float currentRotation)
    {
        currentRotation += Random.Range(0, 360 * Time.deltaTime) * rotateSpeed;

        return currentRotation % Random.Range(90, 270);
    }

    protected override void OnDeath()
    {
        isEnabled = false;
        gameObject.SetActive(false);
    }
}