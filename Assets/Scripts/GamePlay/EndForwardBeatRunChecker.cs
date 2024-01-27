using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EndForwardBeatRunChecker : MonoBehaviour
{
    private void Start()
    {
        this.enabled = false;
    }
    void Update()
    {
        if (!CheckIfTracksActive() && GameStateCoordinator.GetState() == GameState.BeatRun)
        {
            EventCoordinator.TriggerEvent(EventName.World.GameStateChange(), GameMessage.Write().WithNewGameState(GameState.PostLevelWin));
            this.enabled = false;
        }
    }

    public bool CheckIfTracksActive()
    {
        List<BeatTrack> tracks = GetComponentsInChildren<BeatTrack>().ToList();
        foreach (var track in tracks)
        {
            if (track.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
}
