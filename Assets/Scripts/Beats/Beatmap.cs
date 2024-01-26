using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beatmap : MonoBehaviour
{
    public ForwardBeatType type = ForwardBeatType.None;
    public List<ForwardBeat> beats = new List<ForwardBeat>();

    void AddNewBeat()
    {
        //Add a new index position to the end of our list
        beats.Add(new ForwardBeat());
    }

    void RemoveBeat(int index)
    {
        //Remove an index position from our list at a point in our list array
        beats.RemoveAt(index);
    }

    private void OnValidate()
    {
        if (BeatmapEditor.Instance == null)
            return;
        for (int i = 0; i < beats.Count; i++)
        {
            beats[i].UpdateValues();
            if (beats[i].isDirty)
            {
                beats[i].isDirty = false;
                BeatmapEditor.OnBeatEdit(this, beats[i], i);
            }
        }
    }

}
