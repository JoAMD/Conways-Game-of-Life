using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsHolder
{
    public CellBehaviour[][] cells;
    private Vector2Int[] DIRS =
    {
        new Vector2Int(0, 1),
        new Vector2Int(1, 1),
        new Vector2Int(1, 0),
        new Vector2Int(1, -1),
        new Vector2Int(0, -1),
        new Vector2Int(-1, -1),
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1)
    };

    /// <summary>
    /// Each cell calls this function if they are alive
    /// it goes through all the 8 neighbours and adds 1 to the alive neighbours count (neighbourAliveCtr++)
    /// </summary>
    /// <param name="i"> x index of the original alive cell in the array </param>
    /// <param name="j"> y index of the original alive cell in the array </param>
    public void GiveAliveCountToNeighbours(int i, int j)
    {
        int x = i, y = j;
        Vector2Int dir = new Vector2Int(x, y);
        Vector2Int origDir = dir;
        for (int k = 0; k < DIRS.Length; k++)
        {
            dir += DIRS[k];
            if(IsInBounds(dir.x, dir.y))
            {
                cells[dir.x][dir.y].neighbourAliveCtr++;
            }
            dir = origDir;
        }
    }

    /// <summary>
    /// Checks if the cell is inside the array or not
    /// </summary>
    /// <param name="i"> x index of the original alive cell in the array </param>
    /// <param name="j"> y index of the original alive cell in the array </param>
    /// <returns> returns if the cell is in the array or not </returns>
    private bool IsInBounds(int i, int j)
    {
        return
            i >= 0 &&
            i < cells.Length &&
            j >= 0 &&
            j < cells[i].Length;
    }

}

/// <summary>
/// This class holds the important details of the game
/// It also takes care of initialising the game
/// </summary>
public class DataHolder : MonoBehaviour
{
    public static DataHolder instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private List<CellBehaviour> cellsList;

    public Material MAT_CELL_DEAD;
    public Material MAT_CELL_ALIVE;

    public CellsHolder cellsHolder;

    [SerializeField] private UnityEngine.UI.Button startGameBtn;

    [Header("n x n")]
    public GameObject cellnXn;
    public Transform cellsHoldernXnTransform;
    public int n;

    private void Start()
    {
        //disabling start button interactable so that game initialising can be done (coroutine may be needed)_
        startGameBtn.interactable = false;

        if(n > 0)
        {
            InitialiseGame(n);
        }
        else
        {
            InitialiseGame();
        }

        //start game button made interactable again after initialising the game (coroutine may be needed)
        startGameBtn.interactable = true;

        //for (int i = 0; i < cells.Length; i++)
        //{
        //    for (int j = 0; j < cells[i].Length; j++)
        //    {
        //        Debug.Log(cells[i][j].transform.position);
        //    }
        //}

    }

    /// <summary>
    /// Initialising the game, spawns n x n grid for the game of life to run (WIP)
    /// </summary>
    /// <param name="n"></param>
    private void InitialiseGame(int n)
    {
        Camera.main.orthographicSize = (n / 2) * 1.3f;

        float initPosY = (n - 1) / 2;
        float initPosX = -initPosY;
        Vector2 pos = new Vector2(initPosX, initPosY);

        cellsHolder = new CellsHolder();
        cellsHolder.cells = new CellBehaviour[n][];
        GameObject cell;

        Debug.Log("initPos = " + pos);

        for (int i = 0; i < n; i++)
        {
            cellsHolder.cells[i] = new CellBehaviour[n];
            for (int j = 0; j < n; j++)
            {
                cell = Instantiate(cellnXn, pos, Quaternion.identity, cellsHoldernXnTransform);
                Debug.Log("pos = " + pos + " i = " + i + " j = " + j);
                pos.x += 1;
                cellsHolder.cells[i][j] = cell.GetComponent<CellBehaviour>();
            }
            pos.x = initPosX;
            pos.y--;
        }

    }

    /// <summary>
    /// Intiliases the game, takes the cellBehaviours in the cellsList and arranges them in a 2d array for easier referencing when running the game of life.
    /// </summary>
    private void InitialiseGame()
    {
        float sqrtf = Mathf.Sqrt(cellsList.Count);
        int sqrt = (int)sqrtf;
        if (sqrtf != sqrt)
        {
            Debug.LogError("cell number isnt correct, has to be a perfect square");
            return;
        }

        cellsHolder = new CellsHolder();
        cellsHolder.cells = new CellBehaviour[sqrt][];
        int idx = 0;
        int idx2 = 0;
        cellsHolder.cells[0] = new CellBehaviour[sqrt];
        for (int i = 0; i < cellsList.Count; i++, idx++)
        {
            if (idx == sqrt)
            {
                idx = 0;
                idx2++;
                cellsHolder.cells[idx2] = new CellBehaviour[sqrt];
            }
            cellsList[i].i = idx2;
            cellsList[i].j = idx;
            cellsHolder.cells[idx2][idx] = cellsList[i];
        }
    }

}
