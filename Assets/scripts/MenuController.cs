using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Method to load the Main Scene
    public void LoadMainScene()
    {
        // Replace "Main Scene" with the exact name of your target scene
        SceneManager.LoadScene("Game Scene");
    }

    // Method to load the Menu Scene
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu Scene");
    }

    // Method to load the Setup Scene
    public void LoadSetupScene()
    {
        SceneManager.LoadScene("Setup Scene");
    }

    // Method to restart the current scene
    public void RestartGame()
    {
        // Reload the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadPlayerGameScene()
    {
        SceneManager.LoadScene("Player Scene");
    }

    public void LoadStartupScene()
    {
        SceneManager.LoadScene("Startup Scene");
    }
}


