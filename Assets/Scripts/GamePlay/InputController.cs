using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    public List<KeyCode> keysForReverse = new List<KeyCode>();

    KeyCode firstRandKey = KeyCode.None;
    KeyCode secondRandKey = KeyCode.None;
    KeyCode thirdRandKey = KeyCode.None;
    KeyCode fourthRandKey = KeyCode.None;

    void Start()
    {
        SelectRandomKeysForeverse();
    }

    void Update()
    {
        if (GameStateCoordinator.GetState() == GameState.CharacterSelection)
        {
            RunInputsCharacterSelection();
        }
        if (GameStateCoordinator.GetState() == GameState.ForwardBeatRun)
        {
            RunInputsGameplayRunForward();
        }
        if (GameStateCoordinator.GetState() == GameState.ReverseBeatRun)
        {
            RunInputsGameplayRunReverse();
        }
    }

    void RunInputsCharacterSelection()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
        if (Input.GetKeyDown(KeyCode.W))
        {

        }
        if (Input.GetKeyDown(KeyCode.S))
        {

        }
    }

    void RunInputsGameplayRunForward()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {

        }
        if (Input.GetKeyDown(KeyCode.S))
        {

        }
        if (Input.GetKeyDown(KeyCode.A))
        {

        }
        if (Input.GetKeyDown(KeyCode.D))
        {

        }
    }

    void RunInputsGameplayRunReverse()
    {
        if (Input.GetKeyDown(firstRandKey))
        {

        }
        if (Input.GetKeyDown(secondRandKey))
        {

        }
        if (Input.GetKeyDown(thirdRandKey))
        {

        }
        if (Input.GetKeyDown(fourthRandKey))
        {

        }
    }

    void SelectRandomKeysForeverse() {
        List<KeyCode> tmpKeys = new List<KeyCode>(keysForReverse);
        int firstRandInt = Random.Range(0, tmpKeys.Count);
        firstRandKey = tmpKeys[firstRandInt];
        tmpKeys.RemoveAt(firstRandInt);

        int scndRandInt = Random.Range(0, tmpKeys.Count);
        secondRandKey = tmpKeys[scndRandInt];
        tmpKeys.RemoveAt(scndRandInt);

        int thrdRandInt = Random.Range(0, tmpKeys.Count);
        thirdRandKey = tmpKeys[thrdRandInt];
        tmpKeys.RemoveAt(thrdRandInt);

        int fourthRandInt = Random.Range(0, tmpKeys.Count);
        fourthRandKey = tmpKeys[fourthRandInt];
        tmpKeys.RemoveAt(fourthRandInt);
    }
}
