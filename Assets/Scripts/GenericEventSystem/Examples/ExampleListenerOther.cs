using System.Collections;
using System.Collections.Generic;
using GenericEventSystem;
using UnityEngine;

public class ExampleListenerOther : MonoBehaviour {
    void Start() {
        //EventCoordinator.StartListening(EventName.Input.Menus.ShowSettings(), OnShowSettings);
        //Can listen to the same event with any number of Listeners independently, they will all receive an identical Message
        //EventCoordinator.StartListening(EventName.UI.ScoreScreenShown(), OnScoreScreenShown);
    }

    void OnShowSettings(GameMessage msg) {
        foreach (CustomObject ob in msg.targetCustomObjects) {
            Debug.Log(ob.value);
        }
    }

    void OnScoreScreenShown(GameMessage msg) {
        //Event was attached so it will be always executed after the main one.
        Debug.Log(msg.strMessage);
    }
}