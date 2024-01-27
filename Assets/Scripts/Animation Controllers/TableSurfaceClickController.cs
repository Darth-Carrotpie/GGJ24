using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TableSurfaceClickController : MonoBehaviour
{
    Light2D light;
    ForwardBeatType type;
    void Start()
    {
        light = GetComponent<Light2D>();    
        type = GetComponentInParent<BeatHitChecker>().beatType;
        EventCoordinator.StartListening(EventName.Beats.BeatHitInput(), OnBeatHitInput);
    }
    void OnBeatHitInput(GameMessage msg)
    {
        if (type == msg.fBeatType)
        {
            if (msg.pressed)
            {
                light.intensity = 1.0f;
            }
            else
            {
                light.intensity = 0.5f;
            }
        }
    }
}
