using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameModeMyGameActionListener : MonoBehaviour, IMyGameActionListener<GameModeSO>
{
    public GameModeMyGameAction action;
    public MyGameModeEvent unityEvent;

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

    public void OnEventRaised(GameModeSO gamemode)
    {
        Debug.Log("EventRaised");
        unityEvent.Invoke(gamemode);
    }
}

[System.Serializable]
public class MyGameModeEvent : UnityEvent<GameModeSO>
{

}
