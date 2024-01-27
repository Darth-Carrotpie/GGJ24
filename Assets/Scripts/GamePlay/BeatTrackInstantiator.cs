using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTrackInstantiator : MonoBehaviour
{

    public float delay = 3f;
    float timer;
    BeatLevel level;

    public GameObject targetFirst;
    public GameObject targetSecond;
    public GameObject targetThird; 
    public GameObject targetFourth;

    public GameObject beatHitPrefab;

    void Start()
    {
        EventCoordinator.StartListening(EventName.World.GameStateChange(), OnGameStateChanged);
    }

    void OnGameStateChanged(GameMessage msg)
    {
        if(msg.gameState == GameState.ForwardBeatRun)
        {
            level = LevelSelector.Instance.selectedLevel;
        }
    }

    private void Update()
    {
        if(timer <= delay)
        {
            timer += Time.deltaTime;
        }
        else
        {
            CreateBeatTrack(level.beatmaps[0], targetFirst);
            CreateBeatTrack(level.beatmaps[1], targetSecond);
            CreateBeatTrack(level.beatmaps[2], targetThird);
            CreateBeatTrack(level.beatmaps[3], targetFourth);
            this.enabled = false;
            timer = 0;
        }
    }

    void CreateBeatTrack(Beatmap map, GameObject target)
    {
        GameObject newBeatTrack = new GameObject();
        BeatTrack bt = newBeatTrack.AddComponent<BeatTrack>();
        bt.transform.parent = transform;
        bt.transform.position = Vector3.zero;
        bt.beatmap = map;
        bt.target = target;
        bt.beatHitPrefab = beatHitPrefab;
        newBeatTrack.name = "NewBeatTrack:"+map.type;
        targetFirst = target;
    }
}