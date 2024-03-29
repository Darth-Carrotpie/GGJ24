﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace GenericEventSystem {
#if UNITY_EDITOR
  public static class PropertyDrawersHelper
    {

        public static string[] AllSceneNames() {
      var temp = new List<string>();
      foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes) {
        if (S.enabled) {
          string name = S.path.Substring(S.path.LastIndexOf('/') + 1);
          name = name.Substring(0, name.Length - 6);
          temp.Add(name);
        }
      }
      return temp.ToArray();
    }

    //this is an example of how to list all names
    public static string[] AllEventNames() {
      return EventName.Get().ToArray();
    }
  }
#endif
}
