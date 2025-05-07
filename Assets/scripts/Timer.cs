using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float timerDuration = 60f; // Timer duration in seconds
    [SerializeField] private GameObject timerTextObject; // Reference to the TimerText GameObject
    [SerializeField] private TMP_Text timerText; // Reference to the TextMeshPro component inside TimerText
    [SerializeField] private GameObject gameOverCanvas; // Reference to the Game Over canvas
    [SerializeField] private GameObject monsters; // Reference to the Monsters parent object
    [SerializeField] private GameObject monsterCounter; // Reference to the Monster Counter UI
    [SerializeField] private GameObject timeManager; // Reference to the TimeManager object
    [SerializeField] private GameObject MenuUI; // Reference to the MenuUI object
    [SerializeField] private TMP_Text summaryHider; // Summary for Hider win
    [SerializeField] private GameObject Barrel;           // Barrel object
    [SerializeField] private GameObject MonsterRadar;     // Monster Radar object


    private float timer;
    private bool gameEnded = false;

    private void Start()
    {
        timer = timerDuration;
        gameOverCanvas.SetActive(false); // Ensure the Game Over canvas is inactive at the start

        // Make HiderSummary initially inactive
        if (summaryHider != null) summaryHider.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameEnded) return;

        // Decrease the timer
        timer -= Time.deltaTime;

        // Update the timer display
        if (timerText != null)
        {
            timerText.text = $"Time Left: {Mathf.Ceil(timer)}s";
        }

        // Check if the timer has reached zero
        if (timer <= 0)
        {
            Debug.Log("Hider win condition: Timer has run out.");
            EndGame();
        }
    }

    private void EndGame()
    {
        gameEnded = true;

        Debug.Log("Game Ended: Showing Game Over UI");

        // Reveal remaining monsters by switching their material
        RevealRemainingMonsters();

        // Deactivate specific game objects
        //if (monsters != null) monsters.SetActive(false);
        if (monsterCounter != null) monsterCounter.SetActive(false);
        if (timeManager != null) timeManager.SetActive(false);
        if (MenuUI != null) MenuUI.SetActive(false);
        //Disable Monster Radar
        if (MonsterRadar != null)
        {
            MonsterRadar.SetActive(false);
        }

        // Destroy the TimerText GameObject
        if (timerTextObject != null) Destroy(timerTextObject);

        // Activate the Game Over canvas
        gameOverCanvas.SetActive(true);

        // Display the Hider Summary
        DisplayHiderSummary();
    }

    private void DisplayHiderSummary()
    {
        if (summaryHider != null)
        {
            Barrel.SetActive(false);
            int remainingMonsters = CountRemainingMonsters();

            summaryHider.text = $"Hider won\n" +
                                $"{remainingMonsters} monsters left\n" +
                                "Time ran out.";

            summaryHider.gameObject.SetActive(true);
            Debug.Log("Hider Summary Displayed");
        }
    }

    private int CountRemainingMonsters()
    {
        int remainingMonsters = 0;

        if (monsters == null)
        {
            Debug.LogWarning("Monsters parent object is not assigned.");
            return remainingMonsters;
        }

        // Count active monsters
        foreach (Transform monster in monsters.transform)
        {
            if (monster.gameObject.activeSelf)
            {
                remainingMonsters++;
            }
        }

        return remainingMonsters;

    }

    private void RevealRemainingMonsters()
    {
        if (monsters == null)
        {
            Debug.LogWarning("Monsters parent object is not assigned.");
            return;
        }

        foreach (Transform monster in monsters.transform)
        {
            // Find the Skinned Mesh Renderer inside each monster
            SkinnedMeshRenderer skinnedMeshRenderer = monster.GetComponentInChildren<SkinnedMeshRenderer>();

            if (skinnedMeshRenderer != null)
            {
                foreach (Material material in skinnedMeshRenderer.materials)
                {
                    if (material != null)
                    {
                        // Change shader to Standard to make the monster visible
                        material.shader = Shader.Find("Universal Render Pipeline/Lit");
                    }
                }
                Debug.Log($"Monster {monster.name} shader changed to Standard.");
            }
            else
            {
                Debug.LogWarning($"SkinnedMeshRenderer missing on {monster.name}");
            }
        }
    }


}