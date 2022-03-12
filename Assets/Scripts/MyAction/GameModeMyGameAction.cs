using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameModeAction", menuName = "ScriptableObjects/GameModeAction", order = 5)]
public class GameModeMyGameAction : MyGameAction<GameModeSO>
{
    public void Trigger(GameModeSO gamemode)
    {
        InvokeAction(gamemode);
    }
}
