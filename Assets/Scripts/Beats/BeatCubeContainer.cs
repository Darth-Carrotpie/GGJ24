using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCubeContainer : MonoBehaviour
{
    public Color color = Color.blue;
    public ForwardBeatType beatType;
    public List<GameObject> cubes = new List<GameObject>();

    public float currOffset;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void EditCube(int indexAt)
    {

    }
    public void RemoveCube(int indexAt)
    {
        //float cubeOffset = cubes[indexAt].GetComponent<BeatCube>().myOffset;
        Destroy(gameObject);
        cubes.RemoveAt(indexAt);
        currOffset = cubes[indexAt].transform.position.y;
        AdjustCubesPos(indexAt);
    }
    public void GenerateCubes(Beatmap beatmap)
    {
        for (int i = 0; i < beatmap.beats.Count; i++)
        {
            CreateBeatCube(beatmap.beats[i]);
        }
    }
    public void CreateBeatCube(ForwardBeat beat)
    {
        if (beat.beatLengthType != BeatLengthType.pause)
        {
            GameObject newCube = Instantiate(BeatmapEditor.GetBeatCube(beat.beatLengthType));
            newCube.transform.parent = transform;
            Vector3 newOffset = new Vector3(0, currOffset, 0);
            newCube.transform.localPosition = newOffset;
            newCube.GetComponent<BeatCube>().myOffset = currOffset;
            newCube.GetComponent<BeatCube>().beat = beat;
            newCube.GetComponentInChildren<Renderer>().material = GetBeatLineMat(beatType);
            cubes.Add(newCube);
        }
        currOffset += System.Math.Max(beat.length, beat.beatTime) + BeatmapEditor.Instance.padding;
    }
    public void AdjustCubesPos(int indexAt)
    {
        for (int i = indexAt; i < cubes.Count; i++)
        {
            Vector3 newOffset = new Vector3(0, currOffset, 0);
            cubes[i].transform.localPosition = newOffset;
            BeatCube bCube = cubes[i].GetComponent<BeatCube>();
            bCube.myOffset = currOffset;
            currOffset += System.Math.Max(bCube.beat.length, bCube.beat.beatTime) + BeatmapEditor.Instance.padding;
        }
    }
    public void SetBeatLineColor(ForwardBeatType type)
    {
        color = GetBeatLineColor(type);
    }
    public void Clear()
    {
        currOffset = 0;
        foreach (Transform child in this.transform)
        {
            DestroyImmediate(child.gameObject);
        }
        cubes.Clear();
    }
    Color GetBeatLineColor(ForwardBeatType type)
    {
        switch (type)
        {
            case ForwardBeatType.None: return Color.black;
            case ForwardBeatType.EverydayLife: return BeatmapEditor.Instance.colEverydayLife;
            case ForwardBeatType.SocialComementary: return BeatmapEditor.Instance.colSocialCommentary;
            case ForwardBeatType.SelfDeprecation: return BeatmapEditor.Instance.colSelfDeprecation;
            case ForwardBeatType.ObservationalHumor: return BeatmapEditor.Instance.colObservationalHumor;
            default: return Color.black;
        }
    }
    Material GetBeatLineMat(ForwardBeatType type)
    {
        switch (type)
        {
            case ForwardBeatType.None: return BeatmapEditor.Instance.matEverydayLife;
            case ForwardBeatType.EverydayLife: return BeatmapEditor.Instance.matEverydayLife;
            case ForwardBeatType.SocialComementary: return BeatmapEditor.Instance.matSocialCommentary;
            case ForwardBeatType.SelfDeprecation: return BeatmapEditor.Instance.matSelfDeprecation;
            case ForwardBeatType.ObservationalHumor: return BeatmapEditor.Instance.matObservationalHumor;
            default: return BeatmapEditor.Instance.matEverydayLife;
        }
    }
}
