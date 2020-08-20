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
