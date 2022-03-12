using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// ��� ������������� ������������ Unity Action ����������
/// � ������� �������� ������ �� ������ ������, ����� �����������
/// �� Action, ��� ������� ��������.
/// ������� ��������� ����� Action, ����������� ������������� �� �������
/// ��� ��������� ����, �������� ��������� MyGameActionListener
/// </summary>
[Serializable]
public class MyGameAction<T> : ScriptableObject
{
    private List<IMyGameActionListener<T>> Listeners = new List<IMyGameActionListener<T>>();

    public void InvokeAction(T variable)
    {
        for(int i = Listeners.Count-1; i>=0; i--)
        {
            Listeners[i].OnEventRaised(variable);
        }
    }

    public void RegisterListener(IMyGameActionListener<T> listeners)
    {
        if (!Listeners.Contains(listeners))
        {
            Listeners.Add(listeners);
        }
        else
        {
            Debug.Log("Already contains" + listeners);
        }
    }

    public void UnRegisterListener(IMyGameActionListener<T> listeners)
    {
        if (Listeners.Contains(listeners))
        {
            Listeners.Remove(listeners);
        }
    }

}
