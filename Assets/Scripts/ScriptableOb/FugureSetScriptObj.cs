using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fugure", menuName = "ScriptableObjects/FugureSetScriptObj", order = 2)]
public class FugureSetScriptObj : ScriptableObject
{
    //Словарь фигур и вероятностей их выпадения
    [SerializeField] Dictionary<FugureScriptableObj, float> FugureSet;
}
