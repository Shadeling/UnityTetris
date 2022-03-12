using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FugureSet", menuName = "ScriptableObjects/FugureSetScriptObj", order = 2)]
public class FigureSetScriptObj : ScriptableObject
{
    //По хорошему сделать словарь <Фигура, вероятность>, но словари по умолчанию не сериализуемые в Unity, поэтому два списка для хранения набора фигур для игры и их вероятностей выпадения
    [SerializeField] public List<FigureScriptableObj> FugureSet;
    [SerializeField] public List<int> probability;
}