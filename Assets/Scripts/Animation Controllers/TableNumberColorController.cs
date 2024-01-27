using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TableNumberColorController : MonoBehaviour
{
    SpriteRenderer rend;
    ForwardBeatType type;
    public Color PerfectColor;
    public Color AlmostColor;
    public Color DadColor;
    public Color Bomb;

    Color wasSet;
    float counter;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        type = GetComponentInParent<BeatHitChecker>().beatType;
        EventCoordinator.StartListening(EventName.Score.ScoreIncreased(), OnScoreIncreased);
    }
    void OnScoreIncreased(GameMessage msg)
    {
        if (type == msg.fBeatType)
        {
            switch (msg.scoreItem.scoreItemType) {
                case ScoreItemType.Perfect: wasSet = PerfectColor; break;
                case ScoreItemType.Almost: wasSet = AlmostColor; break;
                case ScoreItemType.DadJoke: wasSet = DadColor; break;
                case ScoreItemType.Botch: wasSet = Bomb; break;
                default: wasSet = Color.white; break;
            }
                counter = 0f;
        }
    }
    private void Update()
    {
        counter += Time.deltaTime;
        if(counter < 1f)
        {
            rend.color = Color.Lerp(wasSet, Color.white, counter);
        }
    }
}
