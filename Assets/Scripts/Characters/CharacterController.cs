using GenericEventSystem;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private ReverseBeatType activeType = ReverseBeatType.None;

    void Start()
    {
        FindObjectOfType<ThrowManager>().SetThrowTarget(GetComponentInChildren<ThrowTarget>());

        EventCoordinator.StartListening(EventName.Item.DodgeInput(), OnDodgeInput);
        EventCoordinator.StartListening(EventName.Item.CheckHit(), OnCheckHit);
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

    void OnCheckHit(GameMessage msg)
    {
        if (activeType == msg.rBeatType)
        {
            EventCoordinator.TriggerEvent(EventName.Item.Dodge(), msg);
        }
        else
        {
            // TODO: this event needs to subtract hearts
            EventCoordinator.TriggerEvent(EventName.Item.Hit(), msg);
        }
    }
}
