using System.Collections;
using System.Collections.Generic;
using GenericEventSystem;
using UnityEngine;

public class CrowdReactionController : MonoBehaviour
{
    public float performance;
    public float currBeatWeigth = .2f;
    public float comboIncrease = 0.05f;
    public int avgCount = 5;
    public float[] performaceForReactionLevels = {40f, 30f, 20f, 10f, 0f}; //kolkas random, change later.
    int previousLvl = -1;

    // Start is called before the first frame update
    void Start()
    {
        performance = (performaceForReactionLevels[1]+performaceForReactionLevels[2])/2;
        EventCoordinator.StartListening(EventName.Score.ScoreIncreased(), OnBeatHit);
    }
    void OnBeatHit(GameMessage msg){
        //Debug.Log(msg.score);
        int currBeat = (int)msg.scoreItem.scoreItemType;
        int combo = ComboTracker.GetCombo();
        int previousAvg = ScoreTracker.GetLastItemsAvarage(avgCount);


        performance += ((3-currBeat)+combo*comboIncrease)*currBeatWeigth + ((3-currBeat)-(3-previousAvg))*(1-currBeatWeigth);
        //Debug.Log(3-currBeat + " " + ((3-currBeat)+combo*comboIncrease)*currBeatWeigth + " " + ((3-currBeat)-(3-previousAvg))*(1-currBeatWeigth));

        int LaughBooLevel = performaceForReactionLevels.Length-1;
        for (int i = performaceForReactionLevels.Length-1; i >= 0; i--)
        {
            if(performance >= performaceForReactionLevels[i]){
                LaughBooLevel = i;
            }
        }
        if(previousLvl != LaughBooLevel){
            EventCoordinator.TriggerEvent(EventName.World.CrowdStateChange(), GameMessage.Write().WithIntMessage(LaughBooLevel));
        }
        previousLvl = LaughBooLevel;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
