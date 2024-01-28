using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

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
#if UNITY_EDITOR
    public GameObject EditCube(int indexAt, ForwardBeat beat)
    {
        GameObject cube = cubes[indexAt];
        currOffset = cubes[indexAt].transform.position.y;
        UnityEditor.EditorApplication.delayCall += () =>
        {
            DestroyImmediate(cube);
        };
        GameObject newCube = CreateBeatCube(indexAt, beat);
        AdjustCubesPos(indexAt);
        Selection.activeTransform = newCube.transform;
        return newCube;
    }
#endif
    public GameObject GetBeatCubeAt(int indexAt)
    {
        return cubes[indexAt];
    }
    public void RemoveCube(int indexAt)
    {
        //float cubeOffset = cubes[indexAt].GetComponent<BeatCube>().myOffset;
        currOffset = cubes[indexAt].transform.position.y;
        DestroyImmediate(cubes[indexAt]);
        cubes.RemoveAt(indexAt);
        AdjustCubesPos(indexAt);
    }
    public void GenerateCubes(Beatmap beatmap)
    {
        for (int i = 0; i < beatmap.beats.Count; i++)
        {
            CreateBeatCube(beatmap.beats[i]);
        }
    }
    public GameObject InsertCube(int indexAt, ForwardBeat beat)
    {
        GameObject newCube = CreateBeatBase(beat);
        cubes.Insert(indexAt, newCube);
        return newCube;
    }
    public GameObject CreateBeatCube(int indexAt, ForwardBeat beat)
    {
        GameObject newCube = CreateBeatBase(beat);
        cubes[indexAt]=newCube;
        return newCube;
    }
    public GameObject CreateBeatCube(ForwardBeat beat)
    {
        GameObject newCube = CreateBeatBase(beat);
        cubes.Add(newCube);
        return newCube;
    }
    public GameObject CreateBeatBase(ForwardBeat beat)
    {
        GameObject newCube = Instantiate(BeatmapEditor.GetBeatCube(beat.beatLengthType));
            newCube.transform.parent = transform;
            Vector3 newOffset = new Vector3(0, currOffset, 0);
            newCube.transform.localPosition = newOffset;
        BeatCube bcube = newCube.GetComponent<BeatCube>();
        bcube.myOffset = currOffset;
        bcube.beat = beat;
            newCube.GetComponentInChildren<Renderer>().material = GetBeatLineMat(beatType);
        if (beat.beatLengthType == BeatLengthType.pause)
        {
            bcube.Hide();
        }
        float inc = System.Math.Max(beat.length, beat.beatTime) + BeatmapEditor.Instance.padding;
        currOffset += inc;
        return newCube;
    }
    public void AdjustCubesPos(int indexAt)
    {
        for (int i = indexAt+1; i < cubes.Count; i++)
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
