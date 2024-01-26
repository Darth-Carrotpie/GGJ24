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
    public static bool HasRunStarted()
    {
        if(Instance.state == GameState.ForwardBeatRun || Instance.state == GameState.ReverseBeatRun)
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
