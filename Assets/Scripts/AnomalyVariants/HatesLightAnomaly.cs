using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HatesLightAnomaly : Anomaly
{
    [SerializeField] private GameObject model;

    void Start()
    {
        StartAnomaly();
        ToggleVisibility();
        InputSystem.actions.FindAction("Flashlight").performed += ctx => ToggleVisibility();
    }

    void Update()
    {
        if (!model.activeSelf) return;
        float yRotation = Quaternion.LookRotation(GameManager.Instance.Player.transform.position - transform.position)
            .eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);


    }

    void ToggleVisibility()
    => model.SetActive(!GameManager.Instance.Player.FlashlightIsOn);
}
