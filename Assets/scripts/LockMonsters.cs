using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class LockMonsters : MonoBehaviour
{
    [SerializeField] private GameObject monstersParent;   // The parent GameObject with animated monsters as children
    [SerializeField] private GameObject HiderUI;           // Reference to the "MenuUI" GameObject
    [SerializeField] private GameObject startSeekingUI;   // Reference to the "StartSeekingUI" GameObject
    [SerializeField] private GameObject gameOverUI;       // Reference to the "GameOverUI" GameObject
    [SerializeField] private GameObject timeManager;      // Reference to the "TimeManager" GameObject
    [SerializeField] private GameObject Barrel;           // Reference to the "Barrel" GameObject
    [SerializeField] private float fadeDuration = 3f;     // Duration for monsters to fade and vanish
    [SerializeField] private GameObject PassHeadsetUI;
    private void Start()
    {
        // Disable TimeManager on startup
        if (timeManager != null)
        {
            timeManager.SetActive(false);
        }
        else
        {
            Debug.LogWarning("TimeManager GameObject is not assigned in the Inspector.");
        }

        // Disable StartSeekingUI on startup
        if (startSeekingUI != null)
        {
            startSeekingUI.SetActive(false);
            PassHeadsetUI.SetActive(false);
        }
        else
        {
            Debug.LogWarning("StartSeekingUI GameObject is not assigned in the Inspector.");
        }

        // Disable GameOverUI on startup
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
        else
        {
            Debug.LogWarning("GameOverUI GameObject is not assigned in the Inspector.");
        }

        // Disable Barrel gameobject
        if (Barrel != null)
        {
            Barrel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Barrel GameObject is not assigned in the Inspector.");
        }
    }

    public void LockAndFadeMonsters()
    {        

        if (monstersParent == null)
        {
            Debug.LogWarning("Monsters Parent GameObject is not assigned in the Inspector.");
            return;
        }

        // Iterate through all child objects of the parent
        foreach (Transform monster in monstersParent.transform)
        {
            Animator animator = monster.GetComponent<Animator>();
            if (animator != null)
            {
                // Disable the Animator component to lock their position
                animator.enabled = false;
            }

            // Start the fade and vanish process
            StartCoroutine(FadeAndVanish(monster.gameObject));
        }

        // Show the transition UI before seeker mode starts
        if (PassHeadsetUI != null)
        {
            PassHeadsetUI.SetActive(true);
            if (HiderUI != null)
            {
                HiderUI.SetActive(false);
            }
            else
            {
                Debug.LogWarning("HiderUI GameObject is not assigned in the Inspector.");
            }
            Invoke(nameof(EnableSeekerMode), 4f); // Wait for 4 seconds before switching UI
        }
        else
        {
            Debug.LogWarning("Transition UI is not assigned in the Inspector.");
            EnableSeekerMode(); // If UI is missing, proceed immediately
        }
    }

    private void EnableSeekerMode()
    {
        if (PassHeadsetUI != null)
        {
            PassHeadsetUI.SetActive(false); // Hide the transition UI
        }

        // Activate StartSeeking UI
        if (startSeekingUI != null)
        {
            startSeekingUI.SetActive(true);
        }
        else
        {
            Debug.LogWarning("StartSeekingUI GameObject is not assigned in the Inspector.");
        }

        Debug.Log("Seeker mode is now active.");
    }

    private IEnumerator FadeAndVanish(GameObject monster)
    {
        Renderer[] renderers = monster.GetComponentsInChildren<Renderer>();
        float elapsedTime = 0f;

        // Cache original colors of all renderers
        Dictionary<Renderer, Color[]> originalColors = new Dictionary<Renderer, Color[]>();
        foreach (Renderer renderer in renderers)
        {
            if (renderer.material.HasProperty("_Color"))
            {
                originalColors[renderer] = new Color[renderer.materials.Length];
                for (int i = 0; i < renderer.materials.Length; i++)
                {
                    originalColors[renderer][i] = renderer.materials[i].color;
                }
            }
        }

        // Gradually reduce the alpha value to fade the monsters
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            foreach (var renderer in originalColors.Keys)
            {
                for (int i = 0; i < renderer.materials.Length; i++)
                {
                    Color color = originalColors[renderer][i];
                    color.a = alpha;
                    renderer.materials[i].color = color;
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set alpha to 0 and deactivate the monster
        foreach (var renderer in originalColors.Keys)
        {
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                Color color = originalColors[renderer][i];
                color.a = 0f;
                renderer.materials[i].color = color;
            }
        }

        monster.SetActive(false); // Deactivate the monster after fading
        Debug.Log($"{monster.name} has vanished.");
    }

}