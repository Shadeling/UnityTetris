using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureSpawner : MonoBehaviour
{
    [SerializeField] GameModeSO gm;
    [SerializeField] GameObject figurePrefab;

    private BoardController boardController;

    void Start()
    {
        boardController = GetComponent<BoardController>();
        spawnFigure();
    }

    private void spawnFigure()
    {
        int rand = Random.Range(0, 100);
        int sum = 0;

        for (int i=0; i<gm.set.probability.Count; i++)
        {
            
            if (rand < gm.set.probability[i] + sum)
            {
                GameObject figure = Instantiate(figurePrefab, transform);
                figure.transform.Translate(new Vector3((int)boardController.getWigth()/2, boardController.getHeigth() - 2, -1));
                figure.GetComponent<FigureController>().Init(gm.set.FugureSet[i], boardController, gm.hasWalls, gm.framesToDrop);
                figure.GetComponent<FigureController>().onFigureFixed+=spawnFigure;
                break;
            }
            sum += gm.set.probability[i];
        }
    }

    public void ChangeGameMode(GameModeSO gm)
    {
        this.gm = gm;
    }
}
