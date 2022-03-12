using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� ���������, ���������� �� ����� ����
/// </summary>
[CreateAssetMenu(fileName = "GameMode", menuName = "ScriptableObjects/GameModeSO", order = 3)]
public class GameModeSO : ScriptableObject
{
    [SerializeField] public FigureSetScriptObj set;
    [SerializeField] public int boardHeight;
    [SerializeField] public int boardWight;
    [SerializeField] public bool hasWalls;
    [SerializeField] public int linesToDestroy;
    [SerializeField] public int framesToDrop;
}
