using UnityEngine;

/// <summary>
/// Put this script onto an object to make it have display text or the ability to interact with it. 
/// Override this class for an item that would run code when interacted with.
/// </summary>
public class InteractableObject : MonoBehaviour
{
    // This is the default but can be changed in Inspector, using X here for an example we can change this later when we pick a default
    [SerializeField] private string interactableText = "Press (E) To Interact"; 
    public string InteractableText { get { return interactableText; } set { interactableText = value; } }

    // This method is made to be overridden if interactable is toggled true
    // This is for anything the player will push a button on to do something with
    public virtual void Interact()
    {

    }
}
