using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
[System.Serializable]
public class ForwardBeat
{
    public float beatTime = 0f;
    public float length = 0f;
    public bool isDirty = false;
    public BeatLengthType _beatLengthType;
    public BeatLengthType beatLengthType
    {
        get { return _beatLengthType; }
        set {
            _beatLengthType = value;
            length = GetBeatLengthVal(value);
        }
    }
    public BeatTimingType _beatTimeType;
    public BeatTimingType beatTimeType
    {
        get { return _beatTimeType; }
        set
        {
            _beatTimeType = value;
            beatTime = GetBeatTimeVal(value);
        }
    }
    public void UpdateValues()
    {
        float newLength = GetBeatLengthVal(_beatLengthType);
        float newTime = GetBeatTimeVal(_beatTimeType);
        if(newLength != length || newTime != beatTime)
        {
            isDirty = true;
        }
        length = GetBeatLengthVal(_beatLengthType);
        beatTime = GetBeatTimeVal(_beatTimeType);
    }
    public float GetBeatLengthVal(BeatLengthType beatLengthType)
    {
        float newLength = 0f;
        switch (_beatLengthType)
        {
            case BeatLengthType.mini: newLength = 0.25f; break;
            case BeatLengthType.medium: newLength = 1.0f; break;
            case BeatLengthType.big: newLength = 4f; break;
            default: newLength = 0f; break;
        }
        return newLength;
    }
    public float GetBeatTimeVal(BeatTimingType type)
    {
        switch (type)
        {
            case BeatTimingType.B1_8th: return 0.125f;
            case BeatTimingType.B1_6th: return 0.16666f;
            case BeatTimingType.B1_4th: return 0.25f;
            case BeatTimingType.B1_3rd: return 0.3333f;
            case BeatTimingType.B1_2nd: return 0.5f;
            case BeatTimingType.B_single: return 1f;
            case BeatTimingType.B_double: return 2f;
            case BeatTimingType.B_full: return 4f;
            default: return 0f;
        }
    }
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
public enum BeatLengthType
{
    pause,
    mini,
    medium,
    big
}
public enum BeatTimingType
{
    B1_8th,
    B1_6th,
    B1_4th,
    B1_3rd,
    B1_2nd,
    B_single,
    B_double,
    B_full,
}