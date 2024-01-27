using System;
using System.Collections;
using System.Collections.Generic;
using GenericEventSystem;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private ReverseBeatType activeType = ReverseBeatType.None;
    // TMP: code to visualize input
    private Transform spriteTransform;
    private Vector3 initialSpritePosition;
    private Quaternion initialSpriteRotation;

    void Start()
    {
        EventCoordinator.StartListening(EventName.Item.DodgeInput(), OnDodgeInput);
        // TMP: code to visualize input
        spriteTransform = GetComponentInChildren<SpriteRenderer>().transform;
        initialSpritePosition = spriteTransform.localPosition;
        initialSpriteRotation = spriteTransform.localRotation;
    }

    (Vector3, Quaternion) PoseForActiveType()
    {
        switch (activeType)
        {
            case ReverseBeatType.Cat:
                return (initialSpritePosition + Vector3.right, initialSpriteRotation);
            case ReverseBeatType.Chair:
                return (initialSpritePosition - Vector3.right, initialSpriteRotation);
            case ReverseBeatType.Tomato:
                return (initialSpritePosition, initialSpriteRotation * Quaternion.Euler(Vector3.forward * 90));
            case ReverseBeatType.Bottle:
                return (initialSpritePosition - Vector3.up, initialSpriteRotation);
            default:
                return (initialSpritePosition, initialSpriteRotation);
        }
    }

    void Update()
    {
        var (targetPosition, targetRotation) = PoseForActiveType();

        spriteTransform.position = Vector3.Lerp(spriteTransform.position, targetPosition, 0.1f);
        spriteTransform.rotation = Quaternion.Lerp(spriteTransform.rotation, targetRotation, 0.1f);
    }

    public ReverseBeatType ActiveType()
    {
        return activeType;
    }

    void OnDodgeInput(GameMessage msg)
    {
        if (msg.pressed)
        {
            // only last pressed input will be valid
            activeType = msg.rBeatType;
        }
        else
        {
            // until it was released
            if (activeType == msg.rBeatType)
            {
                activeType = ReverseBeatType.None;
            }
        }
    }
}
