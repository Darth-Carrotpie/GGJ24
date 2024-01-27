using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatHitChecker : MonoBehaviour
{
    public List<BeatHit> hits = new List<BeatHit>();

    void Start()
    {
        EventCoordinator.StartListening(EventName.Beats.BeatHitInput(), OnBeatHitIniput);

    }

    void OnBeatHitIniput(GameMessage msg)
    {
        //GameMessage.Write().WithFBeatType(ForwardBeatType.EverydayLife).WithPressed(true));
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
