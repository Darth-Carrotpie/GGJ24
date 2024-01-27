using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : Singleton<InputController>
{

    public List<KeyCode> keysForReverse = new List<KeyCode>();
    private Dictionary<KeyCode, ReverseBeatType> randomItemKeys = new Dictionary<KeyCode, ReverseBeatType>();

    void Start()
    {
        SelectRandomKeysForeverse();
    }

    public Dictionary<KeyCode, ReverseBeatType> GetRandomItemKeys()
    {
        return randomItemKeys;
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
        foreach (var keyItem in randomItemKeys)
        {
            if (Input.GetKeyDown(keyItem.Key))
            {
                EventCoordinator.TriggerEvent(EventName.Item.DodgeInput(), GameMessage.Write().WithRBeatType(keyItem.Value).WithPressed(true));
            }

            if (Input.GetKeyUp(keyItem.Key))
            {
                EventCoordinator.TriggerEvent(EventName.Item.DodgeInput(), GameMessage.Write().WithRBeatType(keyItem.Value).WithPressed(false));
            }
        }
    }
    void RunInputsPostGame()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }
    void SelectRandomKeysForeverse()
    {
        List<KeyCode> tmpKeys = new List<KeyCode>(keysForReverse);

        // remove keys used for laugh hits
        tmpKeys.Remove(KeyCode.A);
        tmpKeys.Remove(KeyCode.S);
        tmpKeys.Remove(KeyCode.D);
        tmpKeys.Remove(KeyCode.F);

        int randomKeyIndex = Random.Range(0, tmpKeys.Count);
        randomItemKeys.Add(tmpKeys[randomKeyIndex], ReverseBeatType.Bottle);
        tmpKeys.RemoveAt(randomKeyIndex);

        randomKeyIndex = Random.Range(0, tmpKeys.Count);
        randomItemKeys.Add(tmpKeys[randomKeyIndex], ReverseBeatType.Cat);
        tmpKeys.RemoveAt(randomKeyIndex);

        randomKeyIndex = Random.Range(0, tmpKeys.Count);
        randomItemKeys.Add(tmpKeys[randomKeyIndex], ReverseBeatType.Chair);
        tmpKeys.RemoveAt(randomKeyIndex);

        randomKeyIndex = Random.Range(0, tmpKeys.Count);
        randomItemKeys.Add(tmpKeys[randomKeyIndex], ReverseBeatType.Tomato);
        tmpKeys.RemoveAt(randomKeyIndex);
    }
}
