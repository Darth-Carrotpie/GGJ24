using System.Collections;
using System.Collections.Generic;
using GenericEventSystem;
using TMPro;
using UnityEngine;

public class AFSelectVisbility : MonoBehaviour
{

    public TextMeshPro afText;
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
        afText.enabled = msg.gameState == GameState.CharacterSelection;
    }

}
