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
        HandleKeyInput(KeyCode.Q, BeatLengthType.mini, ForwardBeatType.SocialComementary);
        HandleKeyInput(KeyCode.W, BeatLengthType.mini, ForwardBeatType.ObservationalHumor);
        HandleKeyInput(KeyCode.E, BeatLengthType.mini, ForwardBeatType.EverydayLife);
        HandleKeyInput(KeyCode.R, BeatLengthType.mini, ForwardBeatType.SelfDeprecation);

        HandleKeyInput(KeyCode.A, BeatLengthType.medium, ForwardBeatType.SocialComementary);
        HandleKeyInput(KeyCode.S, BeatLengthType.medium, ForwardBeatType.ObservationalHumor);
        HandleKeyInput(KeyCode.D, BeatLengthType.medium, ForwardBeatType.EverydayLife);
        HandleKeyInput(KeyCode.F, BeatLengthType.medium, ForwardBeatType.SelfDeprecation);

        HandleKeyInput(KeyCode.Z, BeatLengthType.big, ForwardBeatType.SocialComementary);
        HandleKeyInput(KeyCode.X, BeatLengthType.big, ForwardBeatType.ObservationalHumor);
        HandleKeyInput(KeyCode.C, BeatLengthType.big, ForwardBeatType.EverydayLife);
        HandleKeyInput(KeyCode.V, BeatLengthType.big, ForwardBeatType.SelfDeprecation);
    }
    
    
    private static void HandleKeyInput(KeyCode keyCode, BeatLengthType beatLength, ForwardBeatType beatType)
    {
        if (Input.GetKeyDown(keyCode))
        {
            TriggerBeatEvent(beatLength, beatType);
        }
    }

    private static void TriggerBeatEvent(BeatLengthType beatLength, ForwardBeatType beatType)
    {
        var beat = new ForwardBeat
        {
            beatLengthType = beatLength
        };
        EventCoordinator.TriggerEvent(EventName.Beats.BeatCreated(),
            GameMessage.Write().WithFBeat(beat).WithFBeatType(beatType));
    }
}
