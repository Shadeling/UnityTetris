using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntMyGameActionListener : MonoBehaviour, IMyGameActionListener<int>
{
    public IntMyGameAction action;
    public MyIntEvent unityEvent;

    private void OnEnable()
    {
        if (action)
        {
            action.RegisterListener(this);
        }
    }

    private void OnDisable()
    {
        if (action)
        {
            action.UnRegisterListener(this);
        }
    }

    public void OnEventRaised(int var)
    {
        unityEvent.Invoke(var);
    }
}

[System.Serializable]
public class MyIntEvent : UnityEvent<int>
{

}
