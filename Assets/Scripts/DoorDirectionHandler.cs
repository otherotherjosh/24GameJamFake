using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDirectionHandler : InteractableObject
{
    enum Direction
    {
        Counter = -1,
        Clockwise = 1
    }
    [SerializeField] private Direction direction;
    private Door door;

    void Awake()
    {
        door = GetComponentInParent<Door>();
        Debug.Log(door.transform.position);
    }

    public override void Interact()
    {
        Debug.Log("touched door!");
        door.rotationDirection = (int)direction;
        door.Interact();
    }
}
