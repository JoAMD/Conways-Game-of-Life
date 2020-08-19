using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellState
{
    Dead,
    Alive
}

public class CellBehaviour : MonoBehaviour
{

    public CellState cellState = CellState.Dead;
    public Renderer _renderer;
    public int neighbourAliveCtr = 0;
    public int i, j;

    private void OnEnable()
    {
        MainGameControl.GameTick += Game;
        MainGameControl.GameTick2 += Game2;
    }

    private void OnDisable()
    {
        MainGameControl.GameTick -= Game;
        MainGameControl.GameTick2 -= Game2;
    }

    private void Game()
    {
        if (cellState == CellState.Alive)
        {
            DataHolder.instance.cellsHolder.GiveAliveCountToNeighbours(i, j);
        }
    }

    private void Game2()
    {
        //decide alive or death, RULES
        if (neighbourAliveCtr < 2) //underpopulation
        {
            if (cellState == CellState.Alive)
            {
                CellDie();
            }
        }
        else if (neighbourAliveCtr == 2)
        {
            if (cellState == CellState.Alive)
            {
                //continues to live on
            }
        }
        else if (neighbourAliveCtr == 3) //reproduction
        {
            if (cellState == CellState.Dead)
            {
                CellLive();
            }
        }
        else if (neighbourAliveCtr > 3) //overpopulation
        {
            if (cellState == CellState.Alive)
            {
                CellDie();
            }
        }

        neighbourAliveCtr = 0;
    }

    private void OnMouseDown()
    {
        SwitchCellState();
    }

    private void SwitchCellState()
    {
        if (cellState == CellState.Dead)
        {
            CellLive();
        }
        else
        {
            CellDie();
        }
    }

    private void CellLive()
    {
        cellState = CellState.Alive;
        _renderer.material = DataHolder.instance.MAT_CELL_ALIVE;
    }

    private void CellDie()
    {
        cellState = CellState.Dead;
        _renderer.material = DataHolder.instance.MAT_CELL_DEAD;
    }

}
