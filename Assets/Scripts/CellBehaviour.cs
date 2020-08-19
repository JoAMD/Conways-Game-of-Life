using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum to show the cell state, dead or alive
/// </summary>
public enum CellState
{
    Dead,
    Alive
}

/// <summary>
/// Class to take care of each cell behaviour during each game tick
/// Includes rules of the game of life
/// </summary>
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

    /// <summary>
    /// Game tick one in which if this cell is alive, it reports to all its 8 neighbour cells saying they have one more alive neighbour
    /// </summary>
    private void Game()
    {
        if (cellState == CellState.Alive)
        {
            DataHolder.instance.cellsHolder.GiveAliveCountToNeighbours(i, j);
        }
    }

    /// <summary>
    /// Game tick two in which the alive neighbour count (neighbourAliveCtr) is checked and rules are applied
    /// so as to find the next alive and dead cells for the next iteration
    /// </summary>
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

    /// <summary>
    /// Switches cell state from alive to dead or from dead to alive
    /// </summary>
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

    /// <summary>
    /// Makes the cell alive
    /// CellState enum and renderer material colour
    /// </summary>
    private void CellLive()
    {
        cellState = CellState.Alive;
        _renderer.material = DataHolder.instance.MAT_CELL_ALIVE;
    }

    /// <summary>
    /// Makes the cell dead
    /// CellState enum and renderer material colour
    /// </summary>
    private void CellDie()
    {
        cellState = CellState.Dead;
        _renderer.material = DataHolder.instance.MAT_CELL_DEAD;
    }

}
