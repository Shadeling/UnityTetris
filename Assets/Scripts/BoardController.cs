using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] int heigth;
    [SerializeField] int wigth;
    [SerializeField] GameObject boardBlock;

    private bool[][] tileMap;

    private void Start()
    {
        transform.position = Vector3.zero;
        SpawnBoard();
    }

    private void SpawnBoard()
    {
        for(int i =0; i<=heigth; i++)
        {
            for( int j=-wigth/2; j<wigth/2; j++)
            {
                GameObject cube = Instantiate(boardBlock, transform);
                cube.transform.position = new Vector3(j, i, 0);
            }
        }
    }
}
