using System.Collections;
using System.Collections.Generic;
using GenericEventSystem;
using UnityEngine;

public class CrowdReactionController : MonoBehaviour
{
    public float performance;
    public float currBeatWeigth = .2f;
    public float comboIncrease = 0.05f;
    public float missPunish = 1.5f;
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
        float currBeat = (float)msg.scoreItem.scoreItemType;
        float combo = (float)ComboTracker.GetCombo();
        float previousAvg = ScoreTracker.GetLastItemsAvarage(avgCount);

        if(currBeat >= 2){
            currBeat *= missPunish;
        }
        performance += ((1.5f-currBeat)+combo*comboIncrease)*currBeatWeigth + ((1.5f-currBeat)-(1.5f-previousAvg))*(1-currBeatWeigth);
        //Debug.Log(3-currBeat + " " + ((3-currBeat)+combo*comboIncrease)*currBeatWeigth + " " + ((3-currBeat)-(3-previousAvg))*(1-currBeatWeigth));
        if(performance > performaceForReactionLevels[0]+10){
            performance = performaceForReactionLevels[0]+10;
        }

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
