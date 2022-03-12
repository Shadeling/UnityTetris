using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����, �������� � ���� ��� ����������� ������ ���� � ��������� �������� MyGameAction, 
/// ���������� ��� ����������� ��������� ����� ��������� ������ ���� � ���� Listeners
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
