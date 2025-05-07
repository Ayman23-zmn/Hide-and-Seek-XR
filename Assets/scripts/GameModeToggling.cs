using UnityEngine;
using Meta.XR.MRUtilityKit;

public class GameModeToggling : MonoBehaviour
{
    [SerializeField] private GameObject gameModeUI; // Reference to the GameModeUI GameObject
    [SerializeField] private GameObject foxGameInfo; // Reference to the FoxGameInfo UI GameObject
    [SerializeField] private GameObject playerGameInfo; // Reference to PlayerGameInfo UI GameObject
    private void Start()
    {
        // Ensure GameModeUI is active on startup, and other UIs are inactive
        if (gameModeUI != null)
            gameModeUI.SetActive(true);

        if (foxGameInfo != null)
            foxGameInfo.SetActive(false);

        if (playerGameInfo != null)
            playerGameInfo.SetActive(false);        
    }

    //Toggles FoxGameUI
    public void ToggleFoxGameInfo()
    {
        if (gameModeUI != null)
            gameModeUI.SetActive(false);

        if (foxGameInfo != null)
            foxGameInfo.SetActive(true);

        if (playerGameInfo != null)
            playerGameInfo.SetActive(false);

        Debug.Log("Fox Game Mode selected.");
    }

    // Toggles PlayerGameUI
    public void TogglePlayerGameInfo()
    {
        if (gameModeUI != null)
            gameModeUI.SetActive(false);

        if (foxGameInfo != null)
            foxGameInfo.SetActive(false);

        if (playerGameInfo != null)
            playerGameInfo.SetActive(true);

        Debug.Log("Player Game Mode selected.");
    }

    // Toggles GameMode selector UI
    public void ToggleGameModeUI()
    {
        if(gameModeUI != null)
            gameModeUI.SetActive(true);

        if (foxGameInfo != null)
            foxGameInfo.SetActive(false);

        if (playerGameInfo != null)
            playerGameInfo.SetActive(false);

        Debug.Log("Game Mode UI selected.");
    }
}
