using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FigureController : MonoBehaviour
{
    [SerializeField] GameObject block;
    [SerializeField] int framesToDrop = 150;

    public UnityAction onFigureFixed;

    private FigureScriptableObj fig;
    private bool hasWalls = true;
    private List<GameObject> FigureBlocks;
    private BoardController boardController;
    private int[] rotMatrix = { 0, 1, -1, 0 };

    void Update()
    {
        LeftRightInput();
        RotationInput();

        if(Time.frameCount % framesToDrop == 0)
        {
            if (!checkFloor())
            {
                Move(0, -1);
            }
            else
            {
                FixOnBoard();
            }
        }
    }


    /// <summary>
    /// Инициализация фигуры нужной формы
    /// </summary>
    /// <param name="figure">геометрия фигуры</param>
    /// <param name="board">ссылка на контроллер доски</param>
    public void Init(FigureScriptableObj figure, BoardController board)
    {
        fig = figure;
        boardController = board;
        FigureBlocks = new List<GameObject>();
        spawnFigure();
    }

    /// <summary>
    /// Первоначальное создание каждого блока фигуры из FigureScriptableObj и его сдвиг
    /// </summary>
    private void spawnFigure()
    {
        foreach(Vector2Int bl in fig.blockCoords)
        {
            GameObject newBlock = Instantiate(block, transform);
            newBlock.transform.Translate(bl.x, bl.y, 0);
            FigureBlocks.Add(newBlock);
        }
    }


    private void LeftRightInput()
    {
        if (Input.GetButtonDown("Right"))
        {
            if (!checkRightWalls())
            {
                Move(1, 0);
            }
        }
        else if (Input.GetButtonDown("Left"))
        {
            if (!checkLeftWalls())
            {
                Move(-1, 0);
            }
            //Debug.Log("1" + FigureBlocks[0].transform.position);
            //Debug.Log("\n");
        }
    }

    /*private void RotationInput()
    {
        int[] matrix = { 0, 1, -1, 0 };

        Vector3 point = new Vector3(fig.center.x+transform.position.x, fig.center.y+transform.position.y, 0);

        if (Input.GetButtonDown("RotateClock"))
        {
            transform.RotateAround(point, Vector3.forward, -90);
            if (checkRightWalls() || checkFloor() || checkValidState() || checkLeftWalls())
            {
                transform.RotateAround(point, Vector3.forward, 90);
            }
        }
        else if (Input.GetButtonDown("RotateAntiClock"))
        {
            transform.RotateAround(point, Vector3.forward, 90);
            if (checkRightWalls() || checkFloor() || checkValidState() || checkLeftWalls())
            {
                transform.RotateAround(point, Vector3.forward, -90);
            }
        }
    }*/


    private void RotationInput()
    {

        if (Input.GetButtonDown("RotateClock"))
        {
            foreach (GameObject bl in FigureBlocks)
            {
                Rotate(bl, 1);
            }

            if (checkRightWalls() || checkFloor() || checkValidState() || checkLeftWalls())
            {
                foreach (GameObject bl in FigureBlocks)
                {
                    Rotate(bl, -1);
                }
            }
        }

        else if (Input.GetButtonDown("RotateAntiClock"))
        {
            foreach (GameObject bl in FigureBlocks)
            {
                Rotate(bl, -1);
            }

            if (checkRightWalls() || checkFloor() || checkValidState() || checkLeftWalls())
            {
                foreach (GameObject bl in FigureBlocks)
                {
                    Rotate(bl, 1);
                }
            }
        }
    }

    private void Rotate(GameObject bl, int dir)
    {
        int x, y;

        x = Mathf.CeilToInt(((bl.transform.localPosition.x + fig.center.x) * rotMatrix[0] * dir) + ((bl.transform.localPosition.y + fig.center.y) * rotMatrix[1] * dir));
        y = Mathf.CeilToInt(((bl.transform.localPosition.x + fig.center.x) * rotMatrix[2] * dir) + ((bl.transform.localPosition.y + fig.center.y) * rotMatrix[3] * dir));
        bl.transform.localPosition = new Vector3(x, y, bl.transform.localPosition.z);
    }

    /// <summary>
    /// Движение фигуры по полю на X, Y
    /// </summary>
    private void Move(int x, int y)
    {
        transform.position+= new Vector3(x, y, 0);
    }

    /// <summary>
    /// Проверка на наличие препятсятвий слева, true если есть препятствие
    /// </summary>
    private bool checkLeftWalls()
    {
        foreach (GameObject bl in FigureBlocks)
        {
            if ( ((int)bl.transform.position.x <= 0 && hasWalls) || boardController.checkTile((int)bl.transform.position.x, (int)bl.transform.position.y))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Проверка на наличие препятсятвий справа, true если есть препятствие
    /// </summary>
    private bool checkRightWalls()
    {
        foreach (GameObject bl in FigureBlocks)
        {
            if ( ((int)bl.transform.position.x >= boardController.getWigth()-1 && hasWalls) || boardController.checkTile((int)bl.transform.position.x + 1 , (int)bl.transform.position.y))
            {
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// Проверка на наличие препятствия под каждым блоком, true если есть препятствие
    /// </summary>
    private bool checkFloor()
    {
        foreach (GameObject bl in FigureBlocks)
        {
            if( (int)bl.transform.position.y <= 0 || boardController.checkTile((int)bl.transform.position.x, (int)bl.transform.position.y - 1 )){
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Проверка является ли текущее состояние стабильным, true если есть наложения блоков
    /// </summary>
    private bool checkValidState()
    {
        foreach (GameObject bl in FigureBlocks)
        {
            if (boardController.checkTile((int)bl.transform.position.x, (int)bl.transform.position.y))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Функция для удаления фигуры и передачи составляющих ее блоков в матрицу BoardController, после чего генерация следующей фигуры
    /// </summary>
    private void FixOnBoard()
    {
        foreach(GameObject block in FigureBlocks)
        {
            boardController.setTile( (int)block.transform.position.x, (int)block.transform.position.y, block);
        }

        onFigureFixed?.Invoke();
        Destroy(this.gameObject);
    }
}
