using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimController : MonoBehaviour
{
    public GameObject drinkIt;
    public GameObject duck;
    public GameObject hit;
    public GameObject idle;
    public GameObject sit;
    public GameObject talk;
    public GameObject tomatoe;

    float timer;
    
    void Start()
    {
        EventCoordinator.StartListening(EventName.Item.CheckHit(), OnCheckHit);
        EventCoordinator.StartListening(EventName.Beats.BeatCreated(), OnBeatHit);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1.0f)
        {
            ToState(idle);
        }
    }
    void OnBeatHit(GameMessage msg)
    {
        ToState(talk);
        timer = 0;
    }
    void OnCheckHit(GameMessage msg)
    {

        switch (msg.rBeatType)
        {
            case ReverseBeatType.Cat: ToState(duck); break;
            case ReverseBeatType.Tomato: ToState(tomatoe); break;
            case ReverseBeatType.Chair: ToState(sit); break;
            case ReverseBeatType.Bottle: ToState(drinkIt); break;
        }
        timer = 0;
    }

    void ToState(GameObject objAct)
    {
        drinkIt.SetActive(false);
        duck.SetActive(false);
        hit.SetActive(false);
        idle.SetActive(false);
        sit.SetActive(false);
        talk.SetActive(false);
        tomatoe.SetActive(false);
        objAct.SetActive(true);
}
}
