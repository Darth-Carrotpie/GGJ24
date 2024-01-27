using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimationController : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        EventCoordinator.StartListening(EventName.World.GameStateChange(), OnStateChange);
    }

    void OnStateChange(GameMessage msg)
    {
        if (msg.gameState == GameState.CharacterSelection)
        {
            animator.SetTrigger("ToSelection");
        }
        if (msg.gameState == GameState.BeatRun) {
            animator.SetTrigger("ToGamePlay");
        }
        if (msg.gameState == GameState.PostLevelLose)
        {
            animator.SetTrigger("ToEndScreenLose");
        }
        if (msg.gameState == GameState.PostLevelWin)
        {
            animator.SetTrigger("ToEndScreenWin");
        }
    }
}
