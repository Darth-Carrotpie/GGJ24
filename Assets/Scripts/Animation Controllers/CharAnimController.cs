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

    public GameObject currentTalk;

    bool delay;
    float timer;

    void Start()
    {
        EventCoordinator.StartListening(EventName.Item.Hit(), OnHit);
        EventCoordinator.StartListening(EventName.Item.Dodge(), OnDodge);
        EventCoordinator.StartListening(EventName.Beats.BeatCreated(), OnBeatHit);
    }
    private void Update()
    {
        if (delay)
        {
            timer += Time.deltaTime;
        }
        if (timer > 1.0f)
        {
            delay = false;
            ToIdle();
        }
    }
    void OnBeatHit(GameMessage msg)
    {
        ToTalk();
    }
    void OnHit(GameMessage msg)
    {
        ToState(hit);
    }
    void OnDodge(GameMessage msg)
    {

        switch (msg.rBeatType)
        {
            case ReverseBeatType.Cat: ToState(duck); break;
            case ReverseBeatType.Tomato: ToState(tomatoe); break;
            case ReverseBeatType.Chair: ToState(sit); break;
            case ReverseBeatType.Bottle: ToState(drinkIt); break;
            default: Debug.LogError("Dodged invalid: " + msg); break;
        }
    }

    void ToIdle()
    {
        drinkIt.SetActive(false);
        duck.SetActive(false);
        hit.SetActive(false);
        idle.SetActive(true);
        sit.SetActive(false);
        talk.SetActive(false);
        tomatoe.SetActive(false);
    }
    void ToTalk()
    {
        drinkIt.SetActive(false);
        duck.SetActive(false);
        hit.SetActive(false);
        idle.SetActive(false);
        sit.SetActive(false);
        talk.SetActive(true);
        tomatoe.SetActive(false);
    }
    void ToState(GameObject objAct)
    {
        if (currentTalk == objAct)
            return;

        currentTalk = objAct;

        drinkIt.SetActive(false);
        duck.SetActive(false);
        hit.SetActive(false);
        idle.SetActive(false);
        sit.SetActive(false);
        talk.SetActive(false);
        tomatoe.SetActive(false);

        objAct.SetActive(true);
        timer = 0;
        delay = true;
    }
}
