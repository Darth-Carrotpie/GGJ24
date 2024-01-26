using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    public List<KeyCode> keysForReverse = new List<KeyCode>();

    KeyCode firstRandKey = KeyCode.None;
    KeyCode secondRandKey = KeyCode.None;
    KeyCode thirdRandKey = KeyCode.None;
    KeyCode fourthRandKey = KeyCode.None;

    void Start()
    {
        SelectRandomKeysForeverse();
    }

    void Update()
    {
        if (GameStateCoordinator.GetState() == GameState.CharacterSelection)
        {
            RunInputsCharacterSelection();
        }
        if (GameStateCoordinator.GetState() == GameState.ForwardBeatRun)
        {
            RunInputsGameplayRunForward();
        }
        if (GameStateCoordinator.GetState() == GameState.ReverseBeatRun)
        {
            RunInputsGameplayRunReverse();
        }
    }

    void RunInputsCharacterSelection()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventCoordinator.TriggerEvent(EventName.World.GameStateChange(), GameMessage.Write().WithNewGameState(GameState.ForwardBeatRun).WithPressed(true));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            EventCoordinator.TriggerEvent(EventName.Input.Menus.SelectCharacterNext(), GameMessage.Write());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            EventCoordinator.TriggerEvent(EventName.Input.Menus.SelectCharacterPrevious(), GameMessage.Write());
        }
    }

    void RunInputsGameplayRunForward()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHit(), GameMessage.Write().WithFBeatType(ForwardBeatType.EverydayLife).WithPressed(true));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHit(), GameMessage.Write().WithFBeatType(ForwardBeatType.SelfDeprecation).WithPressed(true));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHit(), GameMessage.Write().WithFBeatType(ForwardBeatType.SocialComementary).WithPressed(true));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHit(), GameMessage.Write().WithFBeatType(ForwardBeatType.ObservationalHumor).WithPressed(true));
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHit(), GameMessage.Write().WithFBeatType(ForwardBeatType.EverydayLife).WithPressed(false));
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHit(), GameMessage.Write().WithFBeatType(ForwardBeatType.SelfDeprecation).WithPressed(false));
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHit(), GameMessage.Write().WithFBeatType(ForwardBeatType.SocialComementary).WithPressed(false));
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHit(), GameMessage.Write().WithFBeatType(ForwardBeatType.ObservationalHumor).WithPressed(false));
        }
    }

    void RunInputsGameplayRunReverse()
    {
        if (Input.GetKeyDown(firstRandKey))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHit(), GameMessage.Write().WithRBeatType(ReverseBeatType.Cat).WithPressed(true));
        }
        if (Input.GetKeyDown(secondRandKey))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHit(), GameMessage.Write().WithRBeatType(ReverseBeatType.Chair).WithPressed(true));
        }
        if (Input.GetKeyDown(thirdRandKey))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHit(), GameMessage.Write().WithRBeatType(ReverseBeatType.Tomato).WithPressed(true));
        }
        if (Input.GetKeyDown(fourthRandKey))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHit(), GameMessage.Write().WithRBeatType(ReverseBeatType.Bottle).WithPressed(true));
        }
        if (Input.GetKeyUp(firstRandKey))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHit(), GameMessage.Write().WithRBeatType(ReverseBeatType.Cat).WithPressed(false));
        }
        if (Input.GetKeyUp(secondRandKey))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHit(), GameMessage.Write().WithRBeatType(ReverseBeatType.Chair).WithPressed(false));
        }
        if (Input.GetKeyUp(thirdRandKey))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHit(), GameMessage.Write().WithRBeatType(ReverseBeatType.Tomato).WithPressed(false));
        }
        if (Input.GetKeyUp(fourthRandKey))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHit(), GameMessage.Write().WithRBeatType(ReverseBeatType.Bottle).WithPressed(false));
        }
    }

    void SelectRandomKeysForeverse() {
        List<KeyCode> tmpKeys = new List<KeyCode>(keysForReverse);
        int firstRandInt = Random.Range(0, tmpKeys.Count);
        firstRandKey = tmpKeys[firstRandInt];
        tmpKeys.RemoveAt(firstRandInt);

        int scndRandInt = Random.Range(0, tmpKeys.Count);
        secondRandKey = tmpKeys[scndRandInt];
        tmpKeys.RemoveAt(scndRandInt);

        int thrdRandInt = Random.Range(0, tmpKeys.Count);
        thirdRandKey = tmpKeys[thrdRandInt];
        tmpKeys.RemoveAt(thrdRandInt);

        int fourthRandInt = Random.Range(0, tmpKeys.Count);
        fourthRandKey = tmpKeys[fourthRandInt];
        tmpKeys.RemoveAt(fourthRandInt);
    }
}
