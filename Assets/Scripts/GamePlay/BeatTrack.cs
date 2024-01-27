using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTrack : MonoBehaviour
{

    public GameObject target;
    public Beatmap beatmap;
    public ForwardBeatType beatType;
    public float distanceToTarget;

    public GameObject beatHitPrefab;
    public List<ForwardBeat> beatQueue;


    float counterNext = 0;
    float timeInitTrack = 0;

    void Start()
    {
        beatType = beatmap.type;
        timeInitTrack = Time.time;
        beatQueue = new List<ForwardBeat>(beatmap.beats);
    }

    void Update()
    {
        distanceToTarget = (target.transform.position - transform.position).magnitude;
        ForwardBeat nextBeat = beatQueue[0];
        if(nextBeat.beatLengthType == BeatLengthType.pause)
        {

        } else
        {

        }
    }


    void SpawnBeatHit(ForwardBeat fBeat)
    {
        GameObject newBeatHitObj = Instantiate(beatHitPrefab);
        newBeatHitObj.transform.position = transform.position;
        BeatHit beatHit = newBeatHitObj.GetComponent<BeatHit>();
        beatHit.fBeat = fBeat;
        target.GetComponent<BeatHitChecker>().AddBeatHit(beatHit);
    }
}
