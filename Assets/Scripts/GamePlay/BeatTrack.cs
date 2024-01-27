using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class BeatTrack : MonoBehaviour
{

    public GameObject target;
    public Beatmap beatmap;
    public ForwardBeatType beatType;

    public GameObject beatHitPrefab;
    public Queue<ForwardBeat> beatQueue;

    public float flyTime = 4f;


    float counterNext = 0;

    ForwardBeat nextBeat;

    void Start()
    {
        beatType = beatmap.type;
        beatQueue = new Queue<ForwardBeat>(beatmap.beats);
        Debug.Log("beat track created: " + beatType);
    }

    void Update()
    {
        counterNext -= Time.deltaTime;
        if (counterNext < 0)
        {
            if (CheckBeatmapEnd()) return;
            GetNextBeat();
            if (nextBeat.beatLengthType != BeatLengthType.pause)
            {
                SpawnBeatHit(nextBeat);
                EventCoordinator.TriggerEvent(EventName.Beats.BeatCreated(), GameMessage.Write().WithFBeat(nextBeat));
            }
        }
    }

    bool CheckBeatmapEnd()
    {
        if(beatQueue.Count == 0)
        {
            gameObject.SetActive(false);
            return true;
        }
        return false;
    }

    ForwardBeat GetNextBeat()
    {

        nextBeat = beatQueue.Dequeue();
        if (nextBeat.beatLengthType == BeatLengthType.pause)
            counterNext = nextBeat.beatTime;
        else
            counterNext = nextBeat.length;

        return nextBeat;
    }

    void SpawnBeatHit(ForwardBeat fBeat)
    {
        GameObject newBeatHitObj = Instantiate(beatHitPrefab);
        newBeatHitObj.transform.position = transform.position;
        BeatHit beatHit = newBeatHitObj.GetComponent<BeatHit>();
        beatHit.fBeat = fBeat;
        beatHit.flyTime = flyTime;
        beatHit.target = target;
        target.GetComponent<BeatHitChecker>().AddBeatHit(beatHit);
    }
}
