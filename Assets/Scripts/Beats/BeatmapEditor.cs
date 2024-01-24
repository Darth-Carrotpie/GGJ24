using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatmapEditor : MonoBehaviour
{
    public GameObject beatmapPrefab;

    public GameObject currentBeatmap;

    [EditorCools.Button]
    private void CreateNewBeatmap()
    {
        GameObject newMap = Instantiate(beatmapPrefab);
        newMap.transform.parent = transform;
        currentBeatmap = newMap;
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
