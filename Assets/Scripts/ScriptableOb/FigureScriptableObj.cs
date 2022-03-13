using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fugure", menuName = "ScriptableObjects/FugureScriptableObj", order = 1)]
public class FigureScriptableObj : ScriptableObject
{
    /// <summary>
    /// �����, ������������ ������� ����� ������������� ��������
    /// </summary>
    [SerializeField] public Vector2 center;

    [SerializeField] public List<Vector2Int> blockCoords;

    [SerializeField] public Material FigureMaterial;
}
