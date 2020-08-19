using System.Collections;
using UnityEngine;

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
