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
    bool dead;

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
            if(flyProgress > 1f + overFlyTime)
            {
                if (!dead)
                {
                    target.gameObject.GetComponent<BeatHitChecker>().RemoveBeatHit(this);
                    dead = true;
                    animator.enabled = true;
                    animator.Play("DestroyBeatHit");
                    Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
                //Destroy(gameObject);
                //StartCoroutine(DestroyTimer());
                }
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

    //IEnumerator DestroyTimer()
    //{
    //    animator.Play("DestroyBeatHit");
    //    yield return new WaitForSeconds(0.3f);
    //    Destroy(gameObject);
    //}
}
