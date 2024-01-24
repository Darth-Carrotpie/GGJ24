using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ForwardBeat
{
    public float beatTime = 0f;
    public ForwardBeatType type = ForwardBeatType.None;
    public float length = 0f;
}

public enum ForwardBeatType
{
    None,
    EverydayLife,
    SocialComementary,
    SelfDeprecation,
    ObservationalHumor,
}

public class ReverseBeat
{
    float beatTime = 0f;
    ReverseBeatType type = 0;
    float length = 0f;
}
public enum ReverseBeatType {
    None,
    Tomato,
    Bottle,
    Chair,
    Cat
}
