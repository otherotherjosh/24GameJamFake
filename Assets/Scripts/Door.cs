using UnityEngine;
using DG.Tweening;

public class Door : InteractableObject
{
    [SerializeField] private Transform openTransform;
    [SerializeField] private float timeToOpen;
    [SerializeField] private Ease ease;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool doorIsOpen;
    private BoxCollider boxCollider;

    void Awake()
    {
        doorIsOpen = false;
        openRotation = openTransform.rotation;
        closedRotation = transform.rotation;
        boxCollider = GetComponent<BoxCollider>();
    }

    public override void Interact()
    {
        boxCollider.enabled = false;
        if (doorIsOpen)
        {
            Debug.Log("door closing");
            transform.DORotateQuaternion(closedRotation, timeToOpen).SetEase(ease)
                .onComplete = () => EnableCollider(closedRotation);
        }
        else
        {
            Debug.Log("door is opening!!");
            transform.DORotateQuaternion(openRotation, timeToOpen).SetEase(ease)
                .onComplete = () => EnableCollider(openRotation);
        }          
        doorIsOpen = !doorIsOpen;
    }

    void EnableCollider(Quaternion targetRotation)
    {
        if (transform.rotation == targetRotation)
            boxCollider.enabled = true;
    }
}
