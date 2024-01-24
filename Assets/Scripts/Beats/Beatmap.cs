using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beatmap : MonoBehaviour
{
    public float bpm = 128f;
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

}
