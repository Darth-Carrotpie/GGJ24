using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatmapEditor : Singleton<BeatmapEditor>
{
    public GameObject beatmapPrefab;

    public BeatLevel currentBeatLevel;

    public GameObject B1_8th_beatCube;
    public GameObject B1_6th_beatCube;
    public GameObject B1_4th_beatCube;
    public GameObject B1_3rd_beatCube;
    public GameObject B1_2nd_beatCube;
    public GameObject B_full_beatCube;
    public GameObject B_double_beatCube;

    public GameObject singleBeatCube;
    public GameObject mediumBeatCube;
    public GameObject longBeatCube;

    public List<BeatCubeContainer> beatCubeContList = new List<BeatCubeContainer>();

    public Color colEverydayLife = Color.blue;
    public Material matEverydayLife;
    public Color colSocialCommentary = Color.green;
    public Material matSocialCommentary;
    public Color colSelfDeprecation = Color.red;
    public Material matSelfDeprecation;
    public Color colObservationalHumor = Color.yellow;
    public Material matObservationalHumor;

    public float padding = 0.05f;

    public static GameObject OnBeatAdded(Beatmap beatmap, ForwardBeat beat)
    {
        ForwardBeatType thisType = beatmap.type;
        BeatCubeContainer cont = Instance.GetContainer(thisType);

        return cont.CreateBeatCube(beat);
    }
    public static GameObject OnBeatAdded(Beatmap beatmap, ForwardBeat beat, int removeAt)
    {
        ForwardBeatType thisType = beatmap.type;
        BeatCubeContainer cont = Instance.GetContainer(thisType);

        return cont.CreateBeatCube(removeAt, beat);
    }
    public static void OnBeatRemoved(Beatmap beatmap, int removeAt)
    {
        ForwardBeatType thisType = beatmap.type;
        BeatCubeContainer cont = Instance.GetContainer(thisType);

        cont.RemoveCube(removeAt);
    }
    public static GameObject OnBeatEdit(Beatmap beatmap, ForwardBeat beat, int indexAt)
    {
        ForwardBeatType thisType = beatmap.type;
        BeatCubeContainer cont = Instance.GetContainer(thisType);

        return cont.EditCube(indexAt, beat);
    }
    public static GameObject GetBeatAt(Beatmap beatmap, int index)
    {
        ForwardBeatType thisType = beatmap.type;
        BeatCubeContainer cont = Instance.GetContainer(thisType);

        return cont.GetBeatCubeAt(index);
    }
    void CeateAllBeatmaps()
    {
        beatCubeContList.Clear();
        for (int i = 1; i < System.Enum.GetValues(typeof(ForwardBeatType)).Length; i++)
        {
            CreateNewBeatmap((ForwardBeatType)i);
            CreateCubeListContainer((ForwardBeatType)i);
        }
    }
    void CreateNewBeatmap(ForwardBeatType beatType)
    {
        GameObject newMap = Instantiate(beatmapPrefab);
        newMap.transform.parent = currentBeatLevel.transform;
        currentBeatLevel.beatmaps.Add(newMap.GetComponent<Beatmap>());
        newMap.name = "New BeatMap: "+ beatType;
        Debug.Log("New BeatLevel created and Empty Beatmap Added!: "+ beatType);
        newMap.GetComponent<Beatmap>().type = beatType;
    }
    [EditorCools.Button]
    void CeateNewBeatLevel()
    {
        GameObject newLevelObj = new GameObject();
        currentBeatLevel = newLevelObj.AddComponent<BeatLevel>();
        newLevelObj.transform.parent = transform;
        newLevelObj.name = "New Beatmap Level";
        CeateAllBeatmaps();
        Debug.Log("Each Beatmap is it's own track and timing. There should be at least a few beatmaps. To edit multiple beatmaps, just open multiple inspector windows side by side and lock each on different beatmap object.");
    }
    void CreateCubeListContainer(ForwardBeatType beatType)
    {
        GameObject newContObj = new GameObject();
        BeatCubeContainer newCont = newContObj.AddComponent<BeatCubeContainer>();
        beatCubeContList.Add(newCont);
        newContObj.transform.parent = transform;
        newContObj.transform.localPosition = Vector3.zero;
        newContObj.transform.localPosition += Vector3.right * beatCubeContList.Count;
        newContObj.name = "Cubes Container: "+ beatType;
        newCont.beatType = beatType;
        newCont.SetBeatLineColor(beatType);
    }
    [EditorCools.Button]
    void RegenerateAllBeatMaps()
    {
        for (int i = 0; i<currentBeatLevel.beatmaps.Count; i++)
        {
            RegenerateBeatMap(currentBeatLevel.beatmaps[i]);
        }
    }

    void RegenerateBeatMap(Beatmap beatmap)
    {
        ForwardBeatType thisType = beatmap.type;
        BeatCubeContainer cont = GetContainer(thisType);

        cont.Clear();
        cont.GenerateCubes(beatmap);
    }

    BeatCubeContainer GetContainer(ForwardBeatType beatType)
    {
        foreach (var cont in beatCubeContList)
        {
            if (cont.beatType == beatType) return cont;
        }
        return null;
    }

    public void OnBeatAdded()
    {

    }
    public void OnBeatRemoved()
    {

    }

    public static GameObject GetBeatCube(BeatLengthType lengthType)
    {
        switch (lengthType)
        {
            case BeatLengthType.mini: return Instance.singleBeatCube;
            case BeatLengthType.medium: return Instance.mediumBeatCube;
            case BeatLengthType.big: return Instance.longBeatCube;
            default: return Instance.singleBeatCube;
        }
    }
}
