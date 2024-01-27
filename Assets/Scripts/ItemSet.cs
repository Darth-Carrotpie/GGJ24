using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSet", menuName = "ScriptableObjects/ItemSet", order = 1)]
public class ItemSet : ScriptableObject
{
    [System.Serializable]
    public class TypeItemEntry
    {
        public ReverseBeatType type;
        public Transform prefab;
    }

    public TypeItemEntry[] throwItemPrefabs;
}
