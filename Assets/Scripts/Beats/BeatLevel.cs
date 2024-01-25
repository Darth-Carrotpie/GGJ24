using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatLevel : MonoBehaviour
{
    public float bpm = 128f;
    public List<Beatmap> beatmaps = new List<Beatmap>();

    Beatmap GetBeatmap(ForwardBeatType beatType)
    {
        foreach(var beat in beatmaps)
        {
            if(beat.type == beatType) return beat;
        }
        return null;
    }
}
