using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboUI : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;

    void Start()
    {
        EventCoordinator.StartListening(EventName.Score.ComboIncreased(), OnComboIncreased);
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    void OnComboIncreased(GameMessage msg)
    {
        textMeshProUGUI.text = "Combo X" + ComboTracker.GetCombo();
        //could spawn number here to add score
        //msg.scoreItem.score.ToString();
    }
}
