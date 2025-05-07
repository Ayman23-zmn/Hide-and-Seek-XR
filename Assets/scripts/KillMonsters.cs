/*using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KillMonsters : MonoBehaviour
{
    // List to hold all monster GameObjects in the scene
    public List<GameObject> monsters = new List<GameObject>();
    private int monsterCount = 0; // Counter to track the number of monsters destroyed

    [SerializeField] TMP_Text KillCountText;

    // Audio clip for the monster dying sound
    [SerializeField] AudioClip monsterDyingSound;

    private AudioSource audioSource;

    private void Start()
    {
        // Populate the list with all GameObjects that have the "Monster" tag
        GameObject[] foundMonsters = GameObject.FindGameObjectsWithTag("Monster");
        monsters.AddRange(foundMonsters);

        // Get or add an AudioSource component to the GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is tagged as "Monster" and is in the monsters list
        if (other.CompareTag("Monster") && monsters.Contains(other.gameObject))
        {
            // Play the dying sound effect
            if (monsterDyingSound != null)
            {
                audioSource.PlayOneShot(monsterDyingSound);
            }

            // Destroy the monster GameObject
            Destroy(other.gameObject);

            // Increment the monster count
            monsterCount++;

            // Remove the monster from the list
            monsters.Remove(other.gameObject);

            // Update UI text after killing monsters
            KillCountText.text = "Monsters killed: " + monsterCount;
        }
    }
}
*/

using UnityEngine;
using TMPro;

public class KillMonsters : MonoBehaviour
{
    private int monsterCount = 0; // Counter to track the number of monsters destroyed

    [SerializeField] TMP_Text KillCountText;
    [SerializeField] AudioClip monsterDyingSound;

    private AudioSource audioSource;

    private void Start()
    {
        // Get or add an AudioSource component to the GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is tagged as "Monster"
        if (other.CompareTag("Monster"))
        {
            // Play the dying sound effect
            if (monsterDyingSound != null)
            {
                audioSource.PlayOneShot(monsterDyingSound);
            }

            // Destroy the monster GameObject
            Destroy(other.gameObject);

            // Increment the monster count
            monsterCount++;

            // Update UI text after killing a monster
            if (KillCountText != null)
            {
                KillCountText.text = "Monsters killed: " + monsterCount;
            }

            Debug.Log($"Monster destroyed. Total kills: {monsterCount}");
        }
    }
}

