using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatmapEditor : MonoBehaviour
{
    public GameObject beatmapPrefab;

    public Beatmap currentBeatmap;

    [EditorCools.Button]
    private void CreateNewBeatmap()
    {
        GameObject newMap = Instantiate(beatmapPrefab);
        newMap.transform.parent = transform;
        currentBeatmap = newMap.GetComponent<Beatmap>();
        newMap.name = "New Beatmap";
        Debug.Log("Beatmap Created!");
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
