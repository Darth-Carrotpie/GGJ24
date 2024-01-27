using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCanvasController : MonoBehaviour
{
    public GameObject panelToEnableWin;
    public GameObject panelToEnableDefeat;
    public GameObject panelToEnableAlways;
    public GameObject panelToDisableAlways;
    bool isPostGameView;
    void Start()
    {
        EventCoordinator.StartListening(EventName.World.GameStateChange(), OnStateChange);
    }

    void OnStateChange(GameMessage msg)
    {
        if (msg.gameState == GameState.PostLevelLose)
        {
            panelToEnableDefeat.SetActive(true);
            isPostGameView=true;
            panelToEnableAlways.SetActive(true);
            panelToDisableAlways.SetActive(false);
        }
        if (msg.gameState == GameState.PostLevelWin)
        {
            panelToEnableWin.SetActive(true);
            isPostGameView = true;
            panelToEnableAlways.SetActive(true);
            panelToDisableAlways.SetActive(false);
        }
    }
}