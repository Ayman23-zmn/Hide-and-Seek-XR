using UnityEngine;
using TMPro;
using System.Collections;

public class ProximityAudioTrigger : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField] private Transform player; // Reference to the player's headset or camera position

    [Header("Audio Settings")]
    [SerializeField] private float triggerDistance = 5f; // Distance to trigger the sound
    [SerializeField] private AudioClip monsterSound; // Sound to play when player is close
    [SerializeField] private float cooldownTime = 2f; // Cooldown time between sounds for each monster
    [SerializeField] private float textDisplayDuration = 1.5f; // Duration to display the proximity text

    [Header("Dependencies")]
    [SerializeField] private GameObject barrel; // Reference to the Barrel object
    [SerializeField] private GameObject hiderUI; // Reference to the HiderUI object
    [SerializeField] private TMP_Text proximityText; // Reference to the TextMeshPro text field for displaying proximity message
    [SerializeField] private GameObject proximityTextUI; // Reference to the UI containing the TextMeshPro component

    private Transform[] monsters; // Array of monster transforms
    private float[] cooldownTimers; // Cooldown timers for each monster
    private Coroutine textDisplayCoroutine;

    private void Start()
    {
        // Get all monster children
        int childCount = transform.childCount;
        monsters = new Transform[childCount];
        cooldownTimers = new float[childCount];

        for (int i = 0; i < childCount; i++)
        {
            monsters[i] = transform.GetChild(i);
            AudioSource audioSource = monsters[i].gameObject.AddComponent<AudioSource>();
            audioSource.clip = monsterSound;
            audioSource.playOnAwake = false; // Prevent sound from playing on start
        }

        // Ensure the TextMeshPro text and UI are initially inactive
        if (proximityText != null)
        {
            proximityText.text = "";
        }
        if (proximityTextUI != null)
        {
            proximityTextUI.SetActive(false);
        }
    }

    private void Update()
    {
        // Check if the barrel is active and HiderUI is inactive
        if (barrel != null && barrel.activeInHierarchy && (hiderUI == null || !hiderUI.activeInHierarchy))
        {
            EnableMonsterAudio();
        }
        else
        {
            DisableMonsterAudio();
            return; // Skip further processing if monster audio is disabled
        }

        if (player == null)
        {
            Debug.LogWarning("Player transform is not assigned.");
            return;
        }

        for (int i = 0; i < monsters.Length; i++)
        {
            Transform monster = monsters[i];

            if (monster != null && cooldownTimers[i] <= 0f)
            {
                float distance = Vector3.Distance(player.position, monster.position);

                if (distance <= triggerDistance)
                {
                    // Play sound and reset cooldown
                    AudioSource audioSource = monster.GetComponent<AudioSource>();
                    if (audioSource != null && !audioSource.isPlaying)
                    {
                        audioSource.Play();
                        cooldownTimers[i] = cooldownTime;

                        // Update the TextMeshPro text and activate the UI
                        if (proximityText != null && proximityTextUI != null)
                        {
                            proximityText.text = $"Monster detected in {distance:F2} meters.\nYou are close!!";

                            if (!proximityTextUI.activeSelf)
                            {
                                proximityTextUI.SetActive(true);
                                if (textDisplayCoroutine != null)
                                {
                                    StopCoroutine(textDisplayCoroutine);
                                }
                                textDisplayCoroutine = StartCoroutine(DisplayTextTemporarily());
                            }
                        }
                    }
                }
            }
            else
            {
                // Reduce cooldown timer
                cooldownTimers[i] -= Time.deltaTime;
            }
        }
    }


    private void EnableMonsterAudio()
    {
        foreach (Transform monster in monsters)
        {
            if (monster != null)
            {
                AudioSource audioSource = monster.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.enabled = true;
                }
            }
        }
    }

    private void DisableMonsterAudio()
    {
        foreach (Transform monster in monsters)
        {
            if (monster != null)
            {
                AudioSource audioSource = monster.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.enabled = false;
                }
            }
        }
    }

    private IEnumerator DisplayTextTemporarily()
    {
        // Activate the UI
        proximityTextUI.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(textDisplayDuration);

        // Deactivate the UI
        proximityTextUI.SetActive(false);
    }
}

