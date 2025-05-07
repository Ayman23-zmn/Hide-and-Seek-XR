using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class Seeker : MonoBehaviour
{
    [SerializeField] private GameObject barrel;           // Reference to the "Barrel" GameObject
    [SerializeField] private GameObject monstersParent;   // Reference to the parent GameObject of the monsters
    [SerializeField] private GameObject timeManager;      // Reference to the "TimeManager" GameObject
    [SerializeField] private GameObject StartSeekingUI;   // Reference to "SeekingUI" GameObject
    public Animator animator;

    public void ActivateHiddenMonsters()
    {
        if (monstersParent == null)
        {
            Debug.LogWarning("Monsters Parent GameObject is not assigned in the Inspector.");
            return;
        }

        // Enable all monsters under the monstersParent GameObject
        foreach (Transform monster in monstersParent.transform)
        {
            monster.gameObject.SetActive(true);
            Animator animator = monster.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = true; // Enable animation
            }
            Debug.Log($"{monster.name} is now active and visible.");
        }

        // Activate the "TimeManager" GameObject
        if (timeManager != null)
        {
            timeManager.SetActive(true);
            Debug.Log("TimeManager has been activated.");
        }
        else
        {
            Debug.LogWarning("TimeManager GameObject is not assigned in the Inspector.");
        }

        //Activate Barrel 
        if (barrel != null)
        {
            barrel.SetActive(true);
            Debug.Log("Barrel is now in the scene.");
        }
        else
        {
            Debug.LogWarning("Barrel GameObject is not assigned in the Inspector.");
        }

        // Disable the "StartSeekingUI" GameObject on start
        if (StartSeekingUI != null)
        {
            StartSeekingUI.SetActive(false);
            Debug.Log("StartSeekingUI disabled");
        }
        else
        {
            Debug.LogWarning("TimeManager GameObject is not assigned in the Inspector.");
        }
    }
}