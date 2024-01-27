using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class GameStateCoordinator : Singleton<GameStateCoordinator>
{
    [SerializeField]
    GameState state;

    public static GameState GetState()
    {
        return Instance.state;
    }
    public static GameState SetNextState()
    {
        if (Instance.state != GameState.PostLevelWin && Instance.state != GameState.PostLevelLose)
            Instance.state++;
        else Instance.state = GameState.IntroScene;

        return Instance.state;
    }
    public static bool HasRunStarted()
    {
        if (Instance.state == GameState.BeatRun)
            return true;
        return false;
    }

    void Start()
    {
        EventCoordinator.StartListening(EventName.World.GameStateChange(), OnGameStateChanged);
    }

    void OnGameStateChanged(GameMessage msg)
    {
        state = msg.gameState;
    }
}
