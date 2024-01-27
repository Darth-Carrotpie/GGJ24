using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatHitChecker : MonoBehaviour
{
    public Queue<BeatHit> hits = new Queue<BeatHit>();
    public ForwardBeatType beatType;
    float deviation;

    BeatHit hit;

    void Start()
    {
        EventCoordinator.StartListening(EventName.Beats.BeatHitInput(), OnBeatHitIniput);
    }

    void OnBeatHitIniput(GameMessage msg)
    {
        if(beatType == msg.fBeatType)
        {
            if (msg.pressed)
            {
                if (hits.Count > 0)
                {
                    deviation = GetBeatDistance();
                    EventCoordinator.TriggerEvent(EventName.Beats.BeatHitResult(), GameMessage.Write().WithFBeatType(beatType).WithPressed(true).WithDeltaFloat(deviation));
                } else
                {
                    EventCoordinator.TriggerEvent(EventName.Beats.BeatHitResult(), GameMessage.Write().WithFBeatType(beatType).WithPressed(true).WithDeltaFloat(1f));
                }
            }
            /*else
            {
                deviation += GetBeatEndDistance();
                EventCoordinator.TriggerEvent(EventName.Beats.BeatHitResult(), GameMessage.Write().WithFBeatType(beatType).WithPressed(false).WithDeltaFloat(deviation));
                deviation = 0f;
            }*/
            ScoreTracker.AddScore(deviation);
        }
        //GameMessage.Write().WithFBeatType(ForwardBeatType.EverydayLife).WithPressed(true));
    }

    float GetBeatDistance()
    {
        hit = hits.Dequeue();
        hit.enabled = false;
        Destroy(hit.gameObject);
        return (hit.transform.position - transform.position).magnitude;
    }
    float GetBeatEndDistance()
    {
        return (hit.transform.position - transform.position).magnitude - hit.fBeat.length;
    }
    public void AddBeatHit(BeatHit beat)
    {
        hits.Enqueue(beat);
    }

    public void RemoveBeatHit(BeatHit beat)
    {
        hits.Dequeue();
    }
}
