using UnityEngine;
using TMPro;

public class CaptureMonster : MonoBehaviour
{
    [SerializeField] private TMP_Text monsterCounterText; // Reference to the TextMeshPro counter
    [SerializeField] private GameObject gameOverUI;       // Reference to the GameOver UI
    [SerializeField] private TMP_Text seekerSummary;      // Reference to the SeekerSummary UI
    [SerializeField] private GameObject monsters;         // Parent object of all monsters
    [SerializeField] private GameObject MonsterRadar;     // Monster Radar object
    [SerializeField] private GameObject Barrel;           // Barrel object
    [SerializeField] private GameObject TimeManager;      // Time Manager object
    [SerializeField] private AudioClip captureSound;      // Sound to play when a monster is captured

    private AudioSource audioSource;                     // Reference to the AudioSource
    private int capturedMonsters = 0;                    // Counter for captured monsters
    private int totalMonsters;                           // Total number of monsters in the scene
    private float startTime;                             // Time when capturing started

    private void Start()
    {
        // Dynamically calculate total monsters from the parent object
        if (monsters != null)
        {
            totalMonsters = monsters.transform.childCount;
        }
        else
        {
            Debug.LogError("Monsters parent object is not assigned.");
            return;
        }

        // Initialize the monster counter text
        UpdateMonsterCounterText();

        // Ensure GameOver UI and SeekerSummary are inactive at the start
        if (gameOverUI != null) gameOverUI.SetActive(false);
        if (seekerSummary != null) seekerSummary.gameObject.SetActive(false);

        // Add an AudioSource component if not already present
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Start time for capturing monsters
        startTime = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the collider has the "Monster" tag
        if (other.CompareTag("Monster"))
        {
            Debug.Log($"Monster {other.gameObject.name} captured!");

            // Play the capture sound
            PlayCaptureSound();

            // Destroy the monster GameObject
            Destroy(other.gameObject);

            // Increment the captured monster count
            capturedMonsters++;

            // Update the monster counter text
            UpdateMonsterCounterText();

            // Check if all monsters are captured
            if (capturedMonsters >= totalMonsters)
            {
                Debug.Log("All monsters captured! Game over!");
                ActivateGameOverUI();
            }
        }
    }

    private void PlayCaptureSound()
    {
        if (captureSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(captureSound);
        }
        else
        {
            Debug.LogWarning("Capture sound or AudioSource is not assigned.");
        }
    }

    private void ActivateGameOverUI()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        if (seekerSummary != null)
        {
            float totalElapsedTime = Time.time - startTime; // Calculate elapsed time
            seekerSummary.text = "Seeker won!\n" +
                                 //$"Captured all monsters in {totalElapsedTime:F1} seconds.";
                                 "Captured all hidden monsters";
            seekerSummary.gameObject.SetActive(true);
        }

        // Disable Barrel
        if (Barrel != null)
        {
            Barrel.SetActive(false);
            Debug.Log("Barrel out of scene.");
        }

        // Disable TimeManager
        if (TimeManager != null)
        {
            TimeManager.SetActive(false);
            Debug.Log("TimeManager deactivated.");
        }

        //Disable Monster Radar
        if (MonsterRadar != null)
        {
            MonsterRadar.SetActive(false);
        }
    }

    private void UpdateMonsterCounterText()
    {
        if (monsterCounterText != null)
        {
            monsterCounterText.text = $"Monsters caught: {capturedMonsters}/{totalMonsters}";
        }
    }
}
