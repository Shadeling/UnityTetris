using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FugureSet", menuName = "ScriptableObjects/FugureSetScriptObj", order = 2)]
public class FigureSetScriptObj : ScriptableObject
{
    //�� �������� ������� ������� <������, �����������>, �� ������� �� ��������� �� ������������� � Unity, ������� ��� ������ ��� �������� ������ ����� ��� ���� � �� ������������ ���������
    [SerializeField] public List<FigureScriptableObj> FugureSet;
    [SerializeField] public List<int> probability;
}