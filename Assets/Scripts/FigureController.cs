using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FigureController : MonoBehaviour
{
    [SerializeField] GameObject block;
    [SerializeField] int framesToDrop = 150;
    [SerializeField] int accelerationThenDownPressed = 3;

    public UnityAction onFigureFixed;

    private FigureScriptableObj fig;
    private List<GameObject> FigureBlocks;
    private BoardController boardController;
    private int[] rotMatrix = { 0, 1, -1, 0 };
    private int blocksOnOtherSide = 0;
    private bool hasWalls = false;

    void Update()
    {
        LeftRightInput();
        
        //���� ������ ��������� �� ��������� ������ �� ������ �������, ����� � ������ ���������� ����
        if(blocksOnOtherSide == 0)
        {
            RotationInput();
        }

        //������ framesToDrop ������� �������� ������ ����, � ����� ������ ������ ���� ������ ��� ���� 
        if (Time.frameCount % framesToDrop == 0 || (Input.GetAxis("Vertical")<0 && Time.frameCount % (int)framesToDrop/accelerationThenDownPressed == 0))
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
    /// ������������� ������ ������ �����
    /// </summary>
    /// <param name="figure">��������� ������</param>
    /// <param name="board">������ �� ���������� �����</param>
    public void Init(FigureScriptableObj figure, BoardController board, bool walls, int framesToDrop)
    {
        fig = figure;
        boardController = board;
        hasWalls = walls;
        this.framesToDrop = framesToDrop;
        FigureBlocks = new List<GameObject>();
        spawnFigure();
    }

    public void ChangeGameMode(GameModeSO gm)
    {
        onFigureFixed?.Invoke();
        Destroy(this.gameObject);
    }

    /// <summary>
    /// �������������� �������� ������� ����� ������ �� FigureScriptableObj � ��� �����
    /// </summary>
    private void spawnFigure()
    {
        foreach(Vector2Int bl in fig.blockCoords)
        {
            GameObject newBlock = Instantiate(block, transform);
            newBlock.transform.Translate(bl.x, bl.y, 0);
            if(fig.FigureMaterial != null)
            {
                newBlock.GetComponent<Renderer>().material = fig.FigureMaterial;
            }
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
        }
    }


    private void RotationInput()
    {

        if (Input.GetButtonDown("RotateClock"))
        {
            foreach (GameObject bl in FigureBlocks)
            {
                Rotate(bl, 1);
            }

            //���� �������� �����-���� �������� ����� �������� �������� ���
            if (checkValidState())
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

            //���� �������� �����-���� �������� ����� �������� �������� ���
            if (checkValidState())
            {
                foreach (GameObject bl in FigureBlocks)
                {
                    Rotate(bl, 1);
                }
            }
        }
    }

    /// <summary>
    /// �������� � ������ ��������� ������ ������
    /// </summary>
    private void Rotate(GameObject bl, int dir)
    {
        int x, y;
        x = Mathf.CeilToInt(((bl.transform.localPosition.x + fig.center.x) * rotMatrix[0] * dir) + ((bl.transform.localPosition.y + fig.center.y) * rotMatrix[1] * dir));
        y = Mathf.CeilToInt(((bl.transform.localPosition.x + fig.center.x) * rotMatrix[2] * dir) + ((bl.transform.localPosition.y + fig.center.y) * rotMatrix[3] * dir));
        bl.transform.localPosition = new Vector3(x, y, bl.transform.localPosition.z);
    }

    /// <summary>
    /// �������� ������ �� ���� �� X, Y
    /// </summary>
    private void Move(int x, int y)
    {
        transform.position+= new Vector3(x, y, 0);
        if (!hasWalls)
        {
            NoWallsFix();
        }
    }

    private void NoWallsFix()
    {
        //���������� ������, ����������� � �������� �������
        blocksOnOtherSide = 0;
        foreach (GameObject bl in FigureBlocks)
        {
            if (bl.transform.position.x == -1)
            {
                bl.transform.Translate(boardController.getWigth(), 0, 0);
            }
            if (bl.transform.position.x == boardController.getWigth())
            {
                bl.transform.Translate(-boardController.getWigth(), 0, 0);
            }
            if (Mathf.Abs(bl.transform.localPosition.x) >= boardController.getWigth() / 2)
            {
                blocksOnOtherSide++;
            }

        }
        Debug.Log(blocksOnOtherSide);

        //���� ��� ������ ������� ����� �������
        if (blocksOnOtherSide == FigureBlocks.Count)
        {
            //����������� ������ ���� ������
            int dir = FigureBlocks[0].transform.localPosition.x>0 ? 1 : -1;
            Debug.Log("�������");
            foreach (GameObject bl in FigureBlocks)
            {
                bl.transform.Translate(new Vector3(-1 * dir * boardController.getWigth(), 0, 0));
            }
            transform.Translate(new Vector3(dir * boardController.getWigth(), 0, 0));
        }
    }

    /// <summary>
    /// �������� �� ������� ������������ �����, true ���� ���� �����������
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
    /// �������� �� ������� ������������ ������, true ���� ���� �����������
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
    /// �������� �� ������� ����������� ��� ������ ������, true ���� ���� �����������
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
    /// �������� �������� �� ������� ��������� ����������, true ���� ���� ��������� ������ ��� ������ �� �������
    /// </summary>
    private bool checkValidState()
    {
        foreach (GameObject bl in FigureBlocks)
        {
            if (boardController.checkTile((int)bl.transform.position.x, (int)bl.transform.position.y) || bl.transform.position.x<0 || bl.transform.position.x>boardController.getWigth() || bl.transform.position.y <0 || bl.transform.position.y > boardController.getHeigth())
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// ������� ��� �������� ������ � �������� ������������ �� ������ � ������� BoardController, ����� ���� ��������� ��������� ������
    /// </summary>
    private void FixOnBoard()
    {
        foreach(GameObject block in FigureBlocks)
        {
            boardController.setTile( (int)block.transform.position.x, (int)block.transform.position.y, block);
        }
        boardController.CheckDeleteLines();

        onFigureFixed?.Invoke();
        Destroy(this.gameObject);
    }
}
