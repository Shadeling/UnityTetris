using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, хранящий в себе все необходимые режимы игры и способный вызывать MyGameAction, 
/// вызывающий все необходимые изменения после изменения режима игры у всех Listeners
/// </summary>
public class GameModeController : MonoBehaviour
{
    [SerializeField] GameModeMyGameAction gameModeChanged;
    [SerializeField] List<GameModeSO> gamemodes;

    public void ChangeGameMode(int num)
    {
        gameModeChanged.Trigger(gamemodes[num]);
    }

    private void Start()
    {
        if(gamemodes[0] != null)
        {
            Debug.Log("Trigger");
            ChangeGameMode(0);
        }
    }

}
