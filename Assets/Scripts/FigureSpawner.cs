using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureSpawner : MonoBehaviour
{
    [SerializeField] GameObject figurePrefab;
    [SerializeField] IntMyGameAction gameOverAction;

    private BoardController boardController;
    private GameObject figure;
    private GameModeSO gm;

    void Awake()
    {
        boardController = GetComponent<BoardController>();
    }

    private void spawnFigure()
    {

        int rand = Random.Range(0, 100);
        int sum = 0;

        for (int i=0; i<gm.set.probability.Count; i++)
        {
            
            if (rand < gm.set.probability[i] + sum)
            {
                figure = Instantiate(figurePrefab, transform);
                figure.transform.Translate(new Vector3((int)boardController.getWigth()/2, boardController.getHeigth()-2, -1));
                FigureController figCont = figure.GetComponent<FigureController>();
                figCont.Init(gm.set.FugureSet[i], boardController, gm.hasWalls, gm.framesToDrop);
                figCont.onFigureFixed+=spawnFigure;
                
                //если при спауне возникают коллизии - конец игры
                if (figCont.checkValidState())
                {
                    Destroy(figure);
                    gameOverAction.Trigger(0);
                }

                break;
            }
            sum += gm.set.probability[i];
        }
    }

    public void ChangeGameMode(GameModeSO gm)
    {
        this.gm = gm;
        Destroy(figure);

        spawnFigure();
    }
}
