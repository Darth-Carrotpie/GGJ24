using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BeatHit : MonoBehaviour
{
    public ForwardBeat fBeat;
    public float flyTime;
    public float distanceToTarget;
    public GameObject target;
    public float overFlyTime = 0.5f;

    public float flyProgress;
    Vector3 startPos;
    float flySpeed;

    void Start()
    {
        distanceToTarget = (target.transform.position - transform.position).magnitude;
        flySpeed = distanceToTarget / flyTime;
        Debug.Log("beat hit created: " + fBeat);
        Debug.Log("flyTime: " + flyTime);
        Debug.Log("flySpeed: " + flyTime);
    }

    void Update()
    {
        flyProgress += Time.deltaTime / flyTime;
        transform.position = Vector3.LerpUnclamped(startPos, target.transform.position, flyProgress);
        if (flyProgress > 1f+0.1f)
        {
            target.gameObject.GetComponent<BeatHitChecker>().RemoveBeatHit(this);
            SpawnDestructionFx();
            Destroy(gameObject);
        }
    }

    public void SpawnDestructionFx()
    {

    }
}
