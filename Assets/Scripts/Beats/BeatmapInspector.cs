using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
//Made this using info from this post: https://forum.unity.com/threads/display-a-list-class-with-a-custom-editor-script.227847/
#if UNITY_EDITOR
[InitializeOnLoad]
[CustomEditor(typeof(Beatmap))]
public class BeatmapInspector : Editor
{
    enum displayFieldType { DisplayAsAutomaticFields, DisplayAsCustomizableGUIFields }
    displayFieldType DisplayFieldType;

    Beatmap t;
    SerializedObject GetTarget;
    SerializedProperty ThisBeatList;
    SerializedProperty BeatmapType;
    int ListSize;

    //SerializedProperty beats;

    void OnEnable()
    {
        t = (Beatmap)target;
        GetTarget = new SerializedObject(t);
        ThisBeatList = GetTarget.FindProperty("beats"); // Find the List in our script and create a refrence of it
                                                        //beats = GetTarget.FindProperty("beats");
        BeatmapType = GetTarget.FindProperty("type");
        /*Delegate del = Delegate.Combine(SceneView.onSceneGUIDelegate, new SceneView.OnSceneFunc(CustomOnSceneGUI));

        if (SceneView.onSceneGUIDelegate != (SceneView.OnSceneFunc)del)
        {
            SceneView.onSceneGUIDelegate += (SceneView.OnSceneFunc)del;
            Debug.Log("sub");
        }*/
    }
    void OnDisable() { 
        //SceneView.onSceneGUIDelegate += (SceneView.OnSceneFunc)Delegate.Combine(SceneView.onSceneGUIDelegate, new SceneView.OnSceneFunc(CustomOnSceneGUI));
       // Debug.Log("unsub");
    }
    public override void OnInspectorGUI()
    {
        GetTarget.Update();

        /*if (GUILayout.Button("Update Scene"))
        {
            SceneView.lastActiveSceneView.Repaint();
        }*/
        //BeatmapType.intValue = EditorGUILayout.IntField("BeatmapType", BeatmapType.intValue, GUILayout.ExpandWidth(false));
        EditorGUILayout.PropertyField(BeatmapType);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //Display our list to the inspector window
        string[] timeStrings = Enum.GetNames(typeof(BeatTimingType));
        string[] lenStrings = Enum.GetNames(typeof(BeatLengthType));
        for (int i = 0; i < ThisBeatList.arraySize; i++)
        {
            SerializedProperty MyListRef = ThisBeatList.GetArrayElementAtIndex(i);
            SerializedProperty beatTimeType = MyListRef.FindPropertyRelative("_beatTimeType");
            SerializedProperty beatLengthType = MyListRef.FindPropertyRelative("_beatLengthType");
            SerializedProperty length = MyListRef.FindPropertyRelative("length");
            SerializedProperty beatTime = MyListRef.FindPropertyRelative("beatTime");

            GUILayout.BeginVertical("Box");
            int timeGridInt = beatTimeType.intValue;
            timeGridInt = GUILayout.SelectionGrid(timeGridInt, timeStrings, 4);
            beatTimeType.intValue = timeGridInt;
            GUILayout.EndVertical();

            GUILayout.BeginVertical("Box");
            int lenGridInt = beatLengthType.intValue;
            lenGridInt = GUILayout.SelectionGrid(lenGridInt, lenStrings, 3);
            beatLengthType.intValue = lenGridInt;
            GUILayout.EndVertical();
            //t.beats[i].UpdateValues();
            //type.intValue = EditorGUILayout.IntField("type", type.intValue, GUILayout.ExpandWidth(false));
            //length.floatValue = EditorGUILayout.FloatField("length", length.floatValue, GUILayout.ExpandWidth(false));
            //beatTime.floatValue = EditorGUILayout.FloatField("beatTime", beatTime.floatValue, GUILayout.ExpandWidth(false));

            GUILayout.BeginHorizontal("Box");
            GUILayout.Label("Size: "+ length.floatValue);
            GUILayout.Label("Time (play offset): "+ beatTime.floatValue);
            GUILayout.EndHorizontal();

            EditorGUILayout.Space();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Remove This Index (" + i.ToString() + ")"))
            {
                BeatmapEditor.OnBeatRemoved(t, i);
                ThisBeatList.DeleteArrayElementAtIndex(i);
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            if (GUILayout.Button("Insert New"))
            {
                t.beats.Insert(i+1, new ForwardBeat());
                BeatmapEditor.OnBeatAdded(t, t.beats[i+1], i + 1);

            }
            if (GUILayout.Button("Focus This"))
            {
                GameObject beat = BeatmapEditor.GetBeatAt(t, i);
                SceneView.lastActiveSceneView.LookAt(beat.transform.position);
                Selection.activeTransform = beat.transform;
            }

            GUILayout.EndHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorHelper.DrawUILine(Color.white);
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

            GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Pause"))
        {
            ForwardBeat newBeat = new ForwardBeat();
            t.beats.Add(newBeat);
            BeatmapEditor.OnBeatAdded(t, newBeat);
        }
        if (GUILayout.Button("Add Mini"))
        {
            ForwardBeat newBeat = new ForwardBeat();
            newBeat.beatLengthType = BeatLengthType.mini;
            t.beats.Add(newBeat);
            GameObject newBeatCube = BeatmapEditor.OnBeatAdded(t, newBeat);
            SceneView.lastActiveSceneView.LookAt(newBeatCube.transform.position);
        }
        if (GUILayout.Button("Add Medium"))
        {
            ForwardBeat newBeat = new ForwardBeat();
            newBeat.beatLengthType = BeatLengthType.medium;
            t.beats.Add(newBeat);
            GameObject newBeatCube = BeatmapEditor.OnBeatAdded(t, newBeat);
            SceneView.lastActiveSceneView.LookAt(newBeatCube.transform.position);
        }
        if (GUILayout.Button("Add Big"))
        {
            ForwardBeat newBeat = new ForwardBeat();
            newBeat.beatLengthType = BeatLengthType.big;
            t.beats.Add(newBeat);
            GameObject newBeatCube = BeatmapEditor.OnBeatAdded(t, newBeat);
            SceneView.lastActiveSceneView.LookAt(newBeatCube.transform.position);
        }
        GUILayout.EndHorizontal();
        //Apply the changes to our list
        GetTarget.ApplyModifiedProperties();
    }

    /*public void CustomOnSceneGUI(SceneView sceneView)
    {


        var t = (target as Beatmap);
        GetTarget = new SerializedObject(t);
        ThisBeatList = GetTarget.FindProperty("beats"); // Find the List in our script and create a refrence of it

        ListSize = ThisBeatList.arraySize;

        var tr = t.transform;
        var startPos= tr.position;


        //EditorGUI.BeginChangeCheck();
        //Vector3 pos = Handles.PositionHandle(t.lookAtPoint, Quaternion.identity);

        float currPos = 0f;
        float lineOffset = 0f;


        for (int i = 0; i < ListSize; i++)
        {
            SerializedProperty MyListRef = ThisBeatList.GetArrayElementAtIndex(i);
            SerializedProperty type = MyListRef.FindPropertyRelative("type");
            SerializedProperty length = MyListRef.FindPropertyRelative("length");
            SerializedProperty beatTime = MyListRef.FindPropertyRelative("beatTime");

            switch ((ForwardBeatType)type.intValue)
            {
                case ForwardBeatType.EverydayLife: lineOffset=1f; break;
                case ForwardBeatType.SocialComementary: lineOffset=2f; break;
                case ForwardBeatType.SelfDeprecation: lineOffset=3f; break;
                case ForwardBeatType.ObservationalHumor: lineOffset=4f; break;
                default: break;
            }

            Handles.color = GetBeatLineColor((ForwardBeatType)type.intValue);
            float thickness = 0f;
            if ((ForwardBeatType)type.intValue != ForwardBeatType.None)
                thickness = 20f;


            var sideOffset = Vector3.right * lineOffset;
            var lineStart = Vector3.up * currPos + sideOffset;
            currPos += length.floatValue;

            var lineEnd = Vector3.up * currPos + sideOffset + Vector3.up*0.1f;
            Handles.DrawLine(lineStart, lineEnd, thickness);
        }
        //Handles.EndGUI();
    }

    Color GetBeatLineColor(ForwardBeatType type)
    {
        switch(type)
        {
            case ForwardBeatType.None:return Color.black;
            case ForwardBeatType.EverydayLife: return Color.blue;
            case ForwardBeatType.SocialComementary: return Color.green;
            case ForwardBeatType.SelfDeprecation: return Color.red;
            case ForwardBeatType.ObservationalHumor: return Color.white;
            default: return Color.black;
        }
    }*/

}
#endif
