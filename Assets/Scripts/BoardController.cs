using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] GameObject boardBlock;
    [SerializeField] IntMyGameAction scoreChange;
    [SerializeField] IntMyGameAction gameOverAction;

    private List<GameObject> board;
    private List<List<GameObject>> tileMap;
    private int linesToDestroy = 1;
    private int heigth = 20;
    private int wigth = 10;

    private void Awake()
    {
        transform.position = Vector3.zero;
        tileMap = new List<List<GameObject>>();
        board = new List<GameObject> ();

        ResetTiles();
        SpawnBoard();
    }

    public int getHeigth() { return heigth; }
    public int getWigth() { return wigth; }

    /// <summary>
    /// ���������� true ���� �� ����������� ����� X, Y ���� ����
    /// </summary>
    public bool checkTile(int x, int y)
    {
        try
        {
            return tileMap[y][x] != null;
        }
        catch(ArgumentOutOfRangeException e)
        {
            return false;
        }
    }

    /// <summary>
    /// ��������� ����� � ������� Board �� ����������� X, Y. ���������� False ���� ����� �� �������
    /// </summary>
    public void setTile(int x, int y, GameObject block)
    {
            block.transform.parent = transform;
            tileMap[y][x] = block;
    }


    /// <summary>
    /// ��������� ���� ������� ����� �� �����
    /// </summary>
    private void SpawnBoard()
    {
        for(int i=0; i<board.Count; i++)
        {
            Destroy(board[i].gameObject);
        }

        board.Clear();
        for(int i =0; i<=heigth; i++)
        {
            for( int j=0; j<wigth; j++)
            {
                GameObject cube = Instantiate(boardBlock, transform);
                cube.transform.position = new Vector3(j, i, 0);
                board.Add(cube);
            }
        }
    }


    /// <summary>
    /// ������� � ����������������� ������� ����� ������� �������
    /// </summary>
    public void ResetTiles()
    {
        for (int i = 0; i < tileMap.Count; i++)
        {
            for (int j = 0; j < tileMap[i].Count; j++)
            {
                Destroy(tileMap[i][j]);
            }
        }

        tileMap.Clear();
        tileMap = new List<List<GameObject>>();
        for (int i = 0; i < heigth; i++)
        {
            tileMap.Add(new List<GameObject>());
            for (int j = 0; j < wigth; j++)
            {
                tileMap[i].Add(null);
            }
        }
    }

    /// <summary>
    /// ���� �� ���� ���� ��������� ����������� ������ � ���������� linesToDestroy, �� ��� ������������
    /// </summary>
    public void CheckDeleteLines()
    {
        int emptyNum = 0, emptyLine = 0;
        for (int i = 0; i < heigth; i++)
        {
            emptyNum = 0;

            for (int j = 0; j < wigth; j++)
            {
                if (tileMap[i][j] == null) { 
                    emptyNum++; 
                }
            }

            if (emptyNum == 0) {
                emptyLine++;
            }
            else
            {
                emptyLine = 0;
            }

            //���� ������ ������ ���������� ������ �����
            if(emptyLine == linesToDestroy)
            {
                emptyLine = 0;
                //������� ��������� linesToDestroy �����
                for (int k = i; k>i-linesToDestroy; k--)
                {
                    for (int j = 0; j < wigth; j++)
                    {
                        Destroy(tileMap[k][j].gameObject);
                        tileMap[k][j] = null;
                    }
                }
                MoveBoardDown(linesToDestroy, i+1);
                i -= linesToDestroy;

                scoreChange.Trigger(linesToDestroy * wigth);
            }

        }
    }


    /// <summary>
    /// ����� ����� ����
    /// </summary>
    /// <param name="y">�� ������� ����� ��������</param>
    /// <param name="startWith">� ����� ����� ��������</param>
    private void MoveBoardDown(int y, int startWith)
    {
        for (int i = startWith; i < heigth; i++)
        {
            for (int j = 0; j < wigth; j++)
            {
                if(tileMap[i][j] != null)
                {
                    tileMap[i - y][j] = tileMap[i][j];
                    tileMap[i][j] = null;
                    tileMap[i - y][j].transform.Translate(new Vector3(0, -y));
                }
            }
        }
    }

    /// <summary>
    /// ��� ��������� �����, ����������� ��� ����� ������ ����
    /// </summary>
    public void ChangeGameMode(GameModeSO gm)
    {
        this.linesToDestroy = gm.linesToDestroy;
        this.heigth = gm.boardHeight;
        this.wigth = gm.boardWight;

        ResetTiles();
        SpawnBoard();
    }
}
