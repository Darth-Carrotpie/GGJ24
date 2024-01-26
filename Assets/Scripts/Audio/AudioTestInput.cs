using System.Collections;
using System.Collections.Generic;
using GenericEventSystem;
using UnityEngine;

public class AudioTestInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown((KeyCode.A)))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHitResult(), GameMessage.Write().WithStringMessage("mini").WithFBeatType(ForwardBeatType.SocialComementary));
        }
        
        if (Input.GetKeyDown((KeyCode.S)))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHitResult(), GameMessage.Write().WithStringMessage("mini").WithFBeatType(ForwardBeatType.ObservationalHumor));
        }
        
        if (Input.GetKeyDown((KeyCode.D)))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHitResult(), GameMessage.Write().WithStringMessage("mini").WithFBeatType(ForwardBeatType.EverydayLife));
        }
        
        if (Input.GetKeyDown((KeyCode.F)))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHitResult(), GameMessage.Write().WithStringMessage("mini").WithFBeatType(ForwardBeatType.SelfDeprecation));
        }
    }
}
