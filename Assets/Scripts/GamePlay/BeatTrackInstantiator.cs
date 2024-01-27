using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTrackInstantiator : MonoBehaviour
{

    float delay = 0.5f;
    float timer;
    BeatLevel level;

    public GameObject targetFirst;
    public GameObject targetSecond;
    public GameObject targetThird; 
    public GameObject targetFourth;

    public GameObject beatHitPrefab;

    bool doStart = false;

    void Start()
    {
        EventCoordinator.StartListening(EventName.World.GameStateChange(), OnGameStateChanged);
    }

    void OnGameStateChanged(GameMessage msg)
    {
        if(msg.gameState == GameState.BeatRun)
        {
            level = LevelSelector.Instance.selectedLevel;
            doStart = true;
        }
    }

    private void Update()
    {
        if (!doStart)
        {
            return;
        }
        if(timer <= delay)
        {
            timer += Time.deltaTime;
        }
        else
        {
            foreach(Beatmap map in level.beatmaps)
            {
                if(map.type == ForwardBeatType.EverydayLife)
                {
                    CreateBeatTrack(map, targetFirst);
                }
                if (map.type == ForwardBeatType.SelfDeprecation)
                {
                    CreateBeatTrack(map, targetSecond);
                }
                if (map.type == ForwardBeatType.SocialComementary)
                {
                    CreateBeatTrack(map, targetThird);
                }
                if (map.type == ForwardBeatType.ObservationalHumor)
                {
                    CreateBeatTrack(map, targetFourth);
                }
            }
            GetComponent<EndForwardBeatRunChecker>().enabled = true;
            this.enabled = false;
            timer = 0;
        }
    }

    void CreateBeatTrack(Beatmap map, GameObject target)
    {
        GameObject newBeatTrack = new GameObject();
        BeatTrack bt = newBeatTrack.AddComponent<BeatTrack>();
        BeatHitChecker checker = target.GetComponent<BeatHitChecker>();
        bt.transform.parent = transform;
        bt.transform.position = Vector3.zero;
        bt.beatmap = map;
        bt.target = target;
        bt.beatHitPrefab = beatHitPrefab;
        newBeatTrack.name = "NewBeatTrack:"+map.type;
        targetFirst = target;
        checker.beatType = map.type;
    }
}
