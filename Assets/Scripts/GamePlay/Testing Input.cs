using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            EventCoordinator.TriggerEvent(EventName.World.GameStateChange(), GameMessage.Write().WithNewGameState(GameState.IntroScene));
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            EventCoordinator.TriggerEvent(EventName.World.GameStateChange(), GameMessage.Write().WithNewGameState(GameState.CharacterSelection));
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            EventCoordinator.TriggerEvent(EventName.World.GameStateChange(), GameMessage.Write().WithNewGameState(GameState.ForwardBeatRun));
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            EventCoordinator.TriggerEvent(EventName.World.GameStateChange(), GameMessage.Write().WithNewGameState(GameState.ReverseBeatRun));
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            EventCoordinator.TriggerEvent(EventName.World.GameStateChange(), GameMessage.Write().WithNewGameState(GameState.PostLevelWin));
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            EventCoordinator.TriggerEvent(EventName.World.GameStateChange(), GameMessage.Write().WithNewGameState(GameState.PostLevelLose));
        }
    }
}
