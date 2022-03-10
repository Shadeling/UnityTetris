using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fugure", menuName = "ScriptableObjects/FugureScriptableObj", order = 1)]
public class FugureScriptableObj : ScriptableObject
{
    [SerializeField] Vector2 center;

    [SerializeField] List<Vector2Int> blockCoords;
}
