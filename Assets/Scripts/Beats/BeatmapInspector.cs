using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[InitializeOnLoad]
[CustomEditor(typeof(Beatmap))]
public class BeatmapInspector : Editor
{
    enum displayFieldType { DisplayAsAutomaticFields, DisplayAsCustomizableGUIFields }
    displayFieldType DisplayFieldType;

    Beatmap t;
    SerializedObject GetTarget;
    SerializedProperty ThisBeatList;
    int ListSize;

    //SerializedProperty beats;

    void OnEnable()
    {
        t = (Beatmap)target;
        GetTarget = new SerializedObject(t);
        ThisBeatList = GetTarget.FindProperty("beats"); // Find the List in our script and create a refrence of it
                                                        //beats = GetTarget.FindProperty("beats");
        Delegate del = Delegate.Combine(SceneView.onSceneGUIDelegate, new SceneView.OnSceneFunc(CustomOnSceneGUI));

        if (SceneView.onSceneGUIDelegate != (SceneView.OnSceneFunc)del)
        {
            SceneView.onSceneGUIDelegate += (SceneView.OnSceneFunc)del;
            Debug.Log("sub");
        }
    }
    void OnDisable() { 
        SceneView.onSceneGUIDelegate += (SceneView.OnSceneFunc)Delegate.Combine(SceneView.onSceneGUIDelegate, new SceneView.OnSceneFunc(CustomOnSceneGUI));
        Debug.Log("unsub");

    }
    public override void OnInspectorGUI()
    {
        GetTarget.Update();

        if (GUILayout.Button("Add New"))
        {
            t.beats.Add(new ForwardBeat());
        }
        if (GUILayout.Button("Update Scene"))
        {
        SceneView.lastActiveSceneView.Repaint();
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //Display our list to the inspector window
        string[] selStrings = Enum.GetNames(typeof(ForwardBeatType));
        for (int i = 0; i < ThisBeatList.arraySize; i++)
        {
            SerializedProperty MyListRef = ThisBeatList.GetArrayElementAtIndex(i);
            SerializedProperty type = MyListRef.FindPropertyRelative("type");
            SerializedProperty length = MyListRef.FindPropertyRelative("length");
            SerializedProperty beatTime = MyListRef.FindPropertyRelative("beatTime");

            int selGridInt = type.intValue;
            GUILayout.BeginVertical("Box");
            selGridInt = GUILayout.SelectionGrid(selGridInt, selStrings, 5);
            GUILayout.EndVertical();

            type.intValue = selGridInt;
            //type.intValue = EditorGUILayout.IntField("type", type.intValue, GUILayout.ExpandWidth(false));
            length.floatValue = EditorGUILayout.FloatField("length", length.floatValue, GUILayout.ExpandWidth(false));
            beatTime.floatValue = EditorGUILayout.FloatField("beatTime", beatTime.floatValue, GUILayout.ExpandWidth(false));


            EditorGUILayout.Space();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Remove This Index (" + i.ToString() + ")"))
            {
                ThisBeatList.DeleteArrayElementAtIndex(i);
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            if (GUILayout.Button("Insert New"))
            {
                t.beats.Insert(i+1, new ForwardBeat());
            }
            GUILayout.EndHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        //Apply the changes to our list
        GetTarget.ApplyModifiedProperties();
    }

    public void CustomOnSceneGUI(SceneView sceneView)
    {


        var t = (target as Beatmap);
        GetTarget = new SerializedObject(t);
        ThisBeatList = GetTarget.FindProperty("beats"); // Find the List in our script and create a refrence of it

        ListSize = ThisBeatList.arraySize;

        var tr = t.transform;
        var startPos= tr.position;


        /*Vector3 position = t.transform.position + Vector3.up * 2f;
        float size = 0.5f;
        float pickSize = size * 0.5f;

        if (Handles.Button(position, Quaternion.identity, size, pickSize, Handles.CapFunction.))
            Debug.Log("The button was pressed!");*/

        //EditorGUI.BeginChangeCheck();
        //Vector3 pos = Handles.PositionHandle(t.lookAtPoint, Quaternion.identity);


        /*if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Move point");
            t.lookAtPoint = pos;
            t.Update();
        }*/
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
    }
}
