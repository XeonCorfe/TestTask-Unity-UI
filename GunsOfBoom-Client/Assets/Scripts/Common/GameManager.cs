using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        ScreenManager.LoadScreen(ScreenName.Loading);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}