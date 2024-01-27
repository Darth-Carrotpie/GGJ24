using System.Collections;
using System.Collections.Generic;
using GenericEventSystem;
using UnityEngine;

public class AudioTestInput : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        HandleKeyInput(KeyCode.Q, "mini", ForwardBeatType.SocialComementary);
        HandleKeyInput(KeyCode.W, "mini", ForwardBeatType.ObservationalHumor);
        HandleKeyInput(KeyCode.E, "mini", ForwardBeatType.EverydayLife);
        HandleKeyInput(KeyCode.R, "mini", ForwardBeatType.SelfDeprecation);

        HandleKeyInput(KeyCode.A, "medium", ForwardBeatType.SocialComementary);
        HandleKeyInput(KeyCode.S, "medium", ForwardBeatType.ObservationalHumor);
        HandleKeyInput(KeyCode.D, "medium", ForwardBeatType.EverydayLife);
        HandleKeyInput(KeyCode.F, "medium", ForwardBeatType.SelfDeprecation);

        HandleKeyInput(KeyCode.Z, "long", ForwardBeatType.SocialComementary);
        HandleKeyInput(KeyCode.X, "long", ForwardBeatType.ObservationalHumor);
        HandleKeyInput(KeyCode.C, "long", ForwardBeatType.EverydayLife);
        HandleKeyInput(KeyCode.V, "long", ForwardBeatType.SelfDeprecation);
    }
    
    
    private static void HandleKeyInput(KeyCode keyCode, string length, ForwardBeatType beatType)
    {
        if (Input.GetKeyDown(keyCode))
        {
            TriggerBeatEvent(length, beatType);
        }
    }

    private static void TriggerBeatEvent(string length, ForwardBeatType beatType)
    {
        EventCoordinator.TriggerEvent(EventName.Beats.BeatHitResult(),
            GameMessage.Write().WithStringMessage(length).WithFBeatType(beatType));
    }
}
