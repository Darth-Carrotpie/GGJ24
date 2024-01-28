using System.Collections;
using System.Collections.Generic;
using GenericEventSystem;
using TMPro;
using UnityEngine;

public class ContinueTextVisibility : MonoBehaviour
{
    public TextMeshProUGUI continueText;

    // Start is called before the first frame update
    void Start()
    {
        EventCoordinator.StartListening(EventName.World.GameStateChange(), OnGameStateChange);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnGameStateChange(GameMessage msg)
    {
        switch (msg.gameState)
        {
            case GameState.IntroScene:
                continueText.text = "Press Space to continue";
                continueText.enabled = true;
                break;
            case GameState.CharacterSelection:
                continueText.text = "Press Space to confirm character";
                continueText.enabled = true;
                break;
            default:
                continueText.enabled = false;
                break;
        }
    }
}