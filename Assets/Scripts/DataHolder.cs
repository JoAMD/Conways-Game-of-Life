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

    private bool IsInBounds(int i, int j)
    {
        return
            i >= 0 &&
            i < cells.Length &&
            j >= 0 &&
            j < cells[i].Length;
    }

}

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

    private void Start()
    {
        startGameBtn.interactable = false;

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
            if(idx == sqrt)
            {
                idx = 0;
                idx2++;
                cellsHolder.cells[idx2] = new CellBehaviour[sqrt];
            }
            cellsList[i].i = idx2;
            cellsList[i].j = idx;
            cellsHolder.cells[idx2][idx] = cellsList[i];
        }

        startGameBtn.interactable = true;

        //for (int i = 0; i < cells.Length; i++)
        //{
        //    for (int j = 0; j < cells[i].Length; j++)
        //    {
        //        Debug.Log(cells[i][j].transform.position);
        //    }
        //}

    }

}
