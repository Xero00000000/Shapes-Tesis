using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public static bool GameIsPaused = false;
    public static bool GameIsOver = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (!GameIsOver)
        {
            if (!pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
        }


    }

    public void Exit()
    {
        Application.Quit();
    }
}
