using UnityEngine;

/// <summary>
/// Initialises the game, by taking care of all the cell details by using the references from DataHolder as well
/// </summary>
public class InitialiseGameControl : MonoBehaviour
{

    CellsHolder cellsHolder;

    private UnityEngine.UI.Button startGameBtn;

    private GameObject cellnXn;
    private Transform cellsHoldernXnTransform;
    private int n;

    private void Start()
    {
        //get values from data holder
        startGameBtn = DataHolder.instance.startGameBtn;
        //nXn
        cellnXn = DataHolder.instance.cellnXn;
        cellsHoldernXnTransform = DataHolder.instance.cellsHoldernXnTransform;
        n = DataHolder.instance.n;

        //disabling start button interactable so that game initialising can be done (coroutine may be needed)_
        startGameBtn.interactable = false;

        if (n > 0)
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
        float sqrtf = Mathf.Sqrt(DataHolder.instance.cellsParentTransform.childCount);
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
        for (int i = 0; i < DataHolder.instance.cellsParentTransform.childCount; i++, idx++)
        {
            if (idx == sqrt)
            {
                idx = 0;
                idx2++;
                cellsHolder.cells[idx2] = new CellBehaviour[sqrt];
            }
            DataHolder.instance.cellsParentTransform.GetChild(i).GetComponent<CellBehaviour>().i = idx2;
            DataHolder.instance.cellsParentTransform.GetChild(i).GetComponent<CellBehaviour>().j = idx;
            cellsHolder.cells[idx2][idx] = DataHolder.instance.cellsParentTransform.GetChild(i).GetComponent<CellBehaviour>();
        }
        DataHolder.instance.cellsHolder = cellsHolder;
    }
}
