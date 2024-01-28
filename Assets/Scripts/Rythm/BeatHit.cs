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
    public float overFlyTime = 0.25f;

    public float flyProgress;
    Vector3 startPos;
    float flySpeed;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;

        distanceToTarget = (target.transform.position - transform.position).magnitude;
        flySpeed = distanceToTarget / flyTime;
    }

    void Update()
    {
        flyProgress += Time.deltaTime / flyTime;
        transform.position = Vector3.LerpUnclamped(startPos, target.transform.position, flyProgress);
        if (flyProgress > 1f)
        {
            flyProgress += Time.deltaTime / flyTime;
            if(flyProgress > 1f + +overFlyTime)
            {
                target.gameObject.GetComponent<BeatHitChecker>().RemoveBeatHit(this);
                animator.enabled = true;
                animator.Play("DestroyBeatHit");
                Destroy(gameObject, 0.3f);
            }
        }
    }
    private void OnDestroy()
    {
        SpawnDestructionFx();
    }
    public void SpawnDestructionFx()
    {
        
    }
}
