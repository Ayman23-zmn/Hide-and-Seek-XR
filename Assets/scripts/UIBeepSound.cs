using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UIBeepSound : MonoBehaviour
{
    [SerializeField] private AudioClip uiBeepSound; // The sound effect to play

    private AudioSource audioSource;

    private void Awake()
    {
        // Ensure an AudioSource component is attached
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set default settings for the AudioSource
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    private void OnEnable()
    {
        PlayBeepSound();
    }

    private void PlayBeepSound()
    {
        if (uiBeepSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(uiBeepSound);
        }
        else
        {
            Debug.LogWarning("UI beep sound or AudioSource is not assigned.");
        }
    }
}
