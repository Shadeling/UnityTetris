using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// ƒл€ использовани€ стандартного Unity Action необходимо
/// в скрипте получить ссылку на другой скрипт, чтобы подписатьс€
/// на Action, что снижает гибкость.
/// ѕоэтому использую такие Action, позвол€ющие подписыватьс€ на событи€
/// без изменени€ кода, добавл€€ компонент MyGameActionListener
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
