using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrackerTest : MonoBehaviour
{
    public float scoreSize;
    [EditorCools.Button]
    void AddScore()
    {
        ScoreTracker.AddScore(scoreSize);
    }
    [EditorCools.Button]
    void GetScore()
    {
        Debug.Log(ScoreTracker.GetLastScore().ToString());
    }
    [EditorCools.Button]
    void GetTotalScore()
    {
        Debug.Log(ScoreTracker.GetTotalScore().ToString());
    }

    private void Start()
    {
        EventCoordinator.StartListening(EventName.World.ScoreIncreased(), OnScoreIncreased);
    }

    void OnScoreIncreased(GameMessage msg) { Debug.Log(msg.intMessage.ToString()); }

}
