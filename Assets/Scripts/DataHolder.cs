using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Transform cellsParentTransform;

    public Material MAT_CELL_DEAD;
    public Material MAT_CELL_ALIVE;

    public CellsHolder cellsHolder;

    public UnityEngine.UI.Button startGameBtn;

    [Header("n x n")]
    public GameObject cellnXn;
    public Transform cellsHoldernXnTransform;
    public int n;

}
