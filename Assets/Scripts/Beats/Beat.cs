using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardBeat
{
    float beatTime;
    ForwardBeatType type;
    float length;
}

public enum ForwardBeatType
{
    EverydayLife,
    SocialComementary,
    SelfDeprecation,
    ObservationalHumor
}

public class ReverseBeat
{
    float beatTime;
    ReverseBeatType type;
    float length;
}
public enum ReverseBeatType {
    Tomato,
    Bottle,
    Chair,
    Cat
}
