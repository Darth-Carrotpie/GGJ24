using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GenericEventSystem;
using TMPro;
using UnityEngine;

public class HitWarning : MonoBehaviour
{
    TextMeshPro text;
    ReverseBeatType activeType = ReverseBeatType.None;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshPro>();
        text.gameObject.SetActive(false);


        EventCoordinator.StartListening(EventName.Item.Throw(), OnThrow);
        EventCoordinator.StartListening(EventName.Item.CheckHit(), OnCheckHit);
    }

    void OnThrow(GameMessage msg)
    {
        Debug.Assert(activeType == ReverseBeatType.None);
        activeType = msg.rBeatType;

        text.gameObject.SetActive(true);
        var keyType = InputController.Instance.GetRandomItemKeys().Single(kt => kt.Value == activeType);
        text.text = keyType.Key.ToString();
    }

    void OnCheckHit(GameMessage msg)
    {
        Debug.Assert(activeType == msg.rBeatType);
        Debug.Assert(activeType != ReverseBeatType.None);
        activeType = ReverseBeatType.None;

        text.gameObject.SetActive(false);
    }
}
