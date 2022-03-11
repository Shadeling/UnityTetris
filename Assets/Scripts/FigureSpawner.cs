using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureSpawner : MonoBehaviour
{
    [SerializeField] FigureSetScriptObj set;
    [SerializeField] GameObject figurePrefab;

    private BoardController boardController;

    void Start()
    {
        boardController = GetComponent<BoardController>();
        spawnFigure();
    }

    void Update()
    {
        
    }

    private void spawnFigure()
    {
        int rand = Random.Range(0, 100);
        int sum = 0;

        for(int i=0; i<set.probability.Count; i++)
        {
            
            if (rand < set.probability[i] + sum)
            {
                GameObject figure = Instantiate(figurePrefab, transform);
                figure.transform.Translate(new Vector3((int)boardController.getWigth()/2, boardController.getHeigth() - 2, -1));
                figure.GetComponent<FigureController>().Init(set.FugureSet[i], boardController);
                figure.GetComponent<FigureController>().onFigureFixed+=spawnFigure;
                break;
            }
            sum += set.probability[i];
        }
    }
}
