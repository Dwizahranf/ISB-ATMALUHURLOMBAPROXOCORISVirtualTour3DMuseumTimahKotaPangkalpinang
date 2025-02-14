using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance; // Singleton instance

    [SerializeField] GameObject pauseMenu;
    private bool isMenuEnabled = true; // Track whether the pause menu is enabled

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Set the singleton instance
            DontDestroyOnLoad(gameObject); // Keep the PauseMenu object persistent across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public void Pause()
    {
        if (isMenuEnabled)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Home()
    {
        if (isMenuEnabled)
        {
            SceneManager.LoadScene("Main Menu");
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        if (isMenuEnabled)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    // Method to enable or disable the pause menu
    public void SetMenuEnabled(bool enabled)
    {
        isMenuEnabled = enabled;
    }
}
