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
            //CreateBeatTrack();
        }
    }

    void CreateBeatTrack(Beatmap map, GameObject target)
    {
        GameObject newBeatTrack = new GameObject();
        newBeatTrack.name = "NewBeatTrack";
        targetFirst = target;
    }
}
