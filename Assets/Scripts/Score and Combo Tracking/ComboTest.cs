using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboTest : MonoBehaviour
{
    public int currentComboTest;
    public bool maxComboReachedTest;

    [EditorCools.Button]
    void AddCombo()
    {
        ComboTracker.IncreaseCombo();
    }
    [EditorCools.Button]
    void ResetCombo()
    {
        ComboTracker.ResetCombo();
    }
    [EditorCools.Button]
    void GetCombo()
    {
        int val = ComboTracker.GetCombo();
        currentComboTest = val;
        Debug.Log("Current Combo: " + val.ToString());
    }
    [EditorCools.Button]
    void GetMaxComboReached()
    {
        bool val = ComboTracker.IsMaxCombo();
        maxComboReachedTest = val;
        Debug.Log("Is Max Combo: "+ maxComboReachedTest.ToString());
    }

    private void Start()
    {
        EventCoordinator.StartListening(EventName.Score.ComboIncreased(), OnComboIncreased);
        EventCoordinator.StartListening(EventName.Score.MaxComboReached(), OnMaxComboReached);
    }

    void OnComboIncreased(GameMessage msg) { Debug.Log("Got MSG> Combo Increased to: "+msg.intMessage.ToString()); }
    void OnMaxComboReached(GameMessage msg) { Debug.Log("Got MSG> Max Combo Reached: " + msg.intMessage.ToString()); }
}
