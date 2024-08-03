using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

/// <summary>
/// PlayerInteraction detects what the player is looking at and displays it to the player via the UI.
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDistance;

    private void Start()
    {
        // SetInteractionText("");
        InputSystem.actions.FindAction("Interact").performed += ctx=> Interact();
    }

    void Interact()
    {
        RaycastHit hit;
        // Sends a raycast and stores it in the hit variable
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, interactionDistance))
            return;

        InteractableObject interactableObject = hit.transform.GetComponent<InteractableObject>();
        // Checks if the hit object has the InteractableObject script on it to update the text
        if (interactableObject == null) return;

        interactableObject.Interact();
    }
}
