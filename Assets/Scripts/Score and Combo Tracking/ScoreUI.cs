using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;

    void Start()
    {
        EventCoordinator.StartListening(EventName.Score.ScoreIncreased(), OnScoreIncreased);
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    void OnScoreIncreased(GameMessage msg)
    {
        textMeshProUGUI.text = "Score: "+ScoreTracker.GetTotalScore();
        //could spawn number here to add score
        //msg.scoreItem.score.ToString();
    }
}
