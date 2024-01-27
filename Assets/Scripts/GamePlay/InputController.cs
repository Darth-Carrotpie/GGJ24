using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : Singleton<InputController>
{

    public List<KeyCode> keysForReverse = new List<KeyCode>();

    public KeyCode firstRandKey = KeyCode.None;
    public KeyCode secondRandKey = KeyCode.None;
    public KeyCode thirdRandKey = KeyCode.None;
    public KeyCode fourthRandKey = KeyCode.None;

    void Start()
    {
        SelectRandomKeysForeverse();
    }
    //sawp order
    void Update()
    {
        if (GameStateCoordinator.GetState() == GameState.PostLevelWin || GameStateCoordinator.GetState() == GameState.PostLevelLose)
        {
            RunInputsPostGame();
        }
        if (GameStateCoordinator.GetState() == GameState.BeatRun)
        {
            RunInputsGameplayRunForward();
            RunInputsGameplayRunReverse();
        }
        if (GameStateCoordinator.GetState() == GameState.CharacterSelection)
        {
            RunInputsCharacterSelection();
        }
        if (GameStateCoordinator.GetState() == GameState.IntroScene)
        {
            RunInputsIntroScene();
        }
    }
    void RunInputsIntroScene()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventCoordinator.TriggerEvent(EventName.World.GameStateChange(), GameMessage.Write().WithNewGameState(GameState.CharacterSelection).WithPressed(true));
        }

    }
    void RunInputsCharacterSelection()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventCoordinator.TriggerEvent(EventName.World.GameStateChange(), GameMessage.Write().WithNewGameState(GameState.BeatRun).WithPressed(true));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            EventCoordinator.TriggerEvent(EventName.Input.Menus.SelectCharacterNext(), GameMessage.Write());
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            EventCoordinator.TriggerEvent(EventName.Input.Menus.SelectCharacterPrevious(), GameMessage.Write());
        }
    }

    void RunInputsGameplayRunForward()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHitInput(), GameMessage.Write().WithFBeatType(ForwardBeatType.EverydayLife).WithPressed(true));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHitInput(), GameMessage.Write().WithFBeatType(ForwardBeatType.SelfDeprecation).WithPressed(true));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHitInput(), GameMessage.Write().WithFBeatType(ForwardBeatType.SocialComementary).WithPressed(true));
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHitInput(), GameMessage.Write().WithFBeatType(ForwardBeatType.ObservationalHumor).WithPressed(true));
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHitInput(), GameMessage.Write().WithFBeatType(ForwardBeatType.EverydayLife).WithPressed(false));
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHitInput(), GameMessage.Write().WithFBeatType(ForwardBeatType.SelfDeprecation).WithPressed(false));
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHitInput(), GameMessage.Write().WithFBeatType(ForwardBeatType.SocialComementary).WithPressed(false));
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHitInput(), GameMessage.Write().WithFBeatType(ForwardBeatType.ObservationalHumor).WithPressed(false));
        }
    }

    void RunInputsGameplayRunReverse()
    {
        if (Input.GetKeyDown(firstRandKey))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHitInput(), GameMessage.Write().WithRBeatType(ReverseBeatType.Cat).WithPressed(true));
        }
        if (Input.GetKeyDown(secondRandKey))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHitInput(), GameMessage.Write().WithRBeatType(ReverseBeatType.Chair).WithPressed(true));
        }
        if (Input.GetKeyDown(thirdRandKey))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHitInput(), GameMessage.Write().WithRBeatType(ReverseBeatType.Tomato).WithPressed(true));
        }
        if (Input.GetKeyDown(fourthRandKey))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHitInput(), GameMessage.Write().WithRBeatType(ReverseBeatType.Bottle).WithPressed(true));
        }
        if (Input.GetKeyUp(firstRandKey))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHitInput(), GameMessage.Write().WithRBeatType(ReverseBeatType.Cat).WithPressed(false));
        }
        if (Input.GetKeyUp(secondRandKey))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHitInput(), GameMessage.Write().WithRBeatType(ReverseBeatType.Chair).WithPressed(false));
        }
        if (Input.GetKeyUp(thirdRandKey))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHitInput(), GameMessage.Write().WithRBeatType(ReverseBeatType.Tomato).WithPressed(false));
        }
        if (Input.GetKeyUp(fourthRandKey))
        {
            EventCoordinator.TriggerEvent(EventName.Beats.BeatHitInput(), GameMessage.Write().WithRBeatType(ReverseBeatType.Bottle).WithPressed(false));
        }
    }
    void RunInputsPostGame()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
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
