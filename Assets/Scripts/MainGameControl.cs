using System.Collections;
using UnityEngine;

/// <summary>
/// Main game class with the game loop and tick interval
/// And also the static C# actions which are subscribed to by all the cells in the scene
/// </summary>
public class MainGameControl : MonoBehaviour
{
    public static System.Action GameTick;
    public static System.Action GameTick2;

    [Header("Dev")]
    public bool isDevMode = false;

    public float gameTick = 0.25f;

    public void StartGame()
    {
        StartCoroutine(GameLoop());
    }

    /// <summary>
    /// Main game loop
    /// Contains two game ticks:
    /// First one, causes all alive cells to report to neighbour cells saying they are alive
    /// Second one counts the number of alive neighbours for each cell (got from previous tick) and applies the rules to determine if each cell is alive or dead
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameLoop()
    {
        WaitForSeconds waitGameTick = new WaitForSeconds(gameTick / 2);
        WaitUntil waitUntilInput = new WaitUntil(() => Input.GetKeyDown(KeyCode.P));
        while (true)
        {
            Debug.Log("tick 1");
            GameTick?.Invoke();
            yield return waitGameTick;
            if (isDevMode)
            {
                yield return waitUntilInput;
            }
            Debug.Log("tick 2");
            GameTick2?.Invoke();
            yield return waitGameTick;
        }
    }

    //private void PopulateWithSprites()
    //{

    //}

}
