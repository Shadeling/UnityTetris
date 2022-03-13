using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����, �������� � ���� ��� ����������� ������ ���� � ��������� �������� MyGameAction, 
/// ���������� ��� ����������� ��������� ����� ��������� ������ ���� � ���� Listeners
/// </summary>
public class GameModeController : MonoBehaviour
{
    [SerializeField] GameModeMyGameAction gameModeAction;
    [SerializeField] List<GameModeSO> gamemodes;

    public void ChangeGameMode(int num)
    {
        if(gamemodes.Count > num)
        {
            gameModeAction.Trigger(gamemodes[num]);
        }
    }

    private void Start()
    {
        if(gamemodes[0] != null)
        {
            ChangeGameMode(0);
        }
    }

}
