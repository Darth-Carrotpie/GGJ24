using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatHitChecker : MonoBehaviour
{
    public List<BeatHit> hits = new List<BeatHit>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBeatHit(BeatHit beat)
    {
        hits.Add(beat);
    }

    public void RemoveBeatHit(BeatHit beat)
    {
        hits.Remove(beat);
    }
}
