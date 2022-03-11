using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] int heigth;
    [SerializeField] int wigth;
    [SerializeField] GameObject boardBlock;

    private List<List<GameObject>> tileMap;

    private void Start()
    {
        transform.position = Vector3.zero;
        tileMap = new List<List<GameObject>>();

        ResetTiles();
        SpawnBoard();
        
    }

    public int getHeigth() { return heigth; }
    public int getWigth() { return wigth; }

    /// <summary>
    /// ¬озвращает true если по координатам доски X, Y есть блок
    /// </summary>
    public bool checkTile(int x, int y)
    {
        try
        {
            return tileMap[x][y] != null;
        }
        catch(ArgumentOutOfRangeException e)
        {
            return false;
        }
    }

    public void setTile(int x, int y, GameObject block)
    {
        //Debug.Log(x + " " + y);
        block.transform.parent = transform;
        tileMap[x][y] = block;
    }

    private void SpawnBoard()
    {
        for(int i =0; i<=heigth; i++)
        {
            for( int j=0; j<wigth; j++)
            {
                GameObject cube = Instantiate(boardBlock, transform);
                cube.transform.position = new Vector3(j, i, 0);
            }
        }
    }

    public void ResetTiles()
    {
        tileMap.Clear();
        tileMap = new List<List<GameObject>>();
        for (int i = 0; i < wigth; i++)
        {
            tileMap.Add(new List<GameObject>());
            for (int j = 0; j < heigth; j++)
            {
                tileMap[i].Add(null);
            }
        }
    }
}
