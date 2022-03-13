using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntMyGameAction", menuName = "ScriptableObjects/IntMyGameAction", order = 5)]
public class IntMyGameAction : MyGameAction<int>
{
    public void Trigger(int var)
    {
        InvokeAction(var);
    }
}
