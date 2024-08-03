using UnityEngine;
using DG.Tweening;

public class Door : InteractableObject
{
    [SerializeField] private Transform openTransform;
    [SerializeField] private float timeToOpen;
    [SerializeField] private Ease ease;
    [SerializeField] private float openDegrees;
    private bool doorIsOpen;
    [HideInInspector] public int rotationDirection;
    private Transform playerTransform;

    void Awake()
    {
        doorIsOpen = false;
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    public override void Interact()
    {
        Debug.Log($"door open? {doorIsOpen}, rotation direction? {rotationDirection}");
        float targetRotation = doorIsOpen ? 0 : openDegrees * rotationDirection;
        transform.DOLocalRotate(new Vector3(
            0, targetRotation, 0
        ), 
        timeToOpen).SetEase(ease)
            .onComplete = () => EnableCollider(targetRotation);
        doorIsOpen = !doorIsOpen;
    }

    void EnableCollider(float targetRotation)
    {
        if (targetRotation < 0) targetRotation += 360;
        Debug.Log(string.Format("target rotation: {0}, rotation: {1}",
            targetRotation, transform.localRotation.eulerAngles.y
        ));
        if (Mathf.Round(transform.localRotation.eulerAngles.y) == targetRotation)
        {
            Debug.Log("target rotation and rotation are the same!!");
        }
    }
}
