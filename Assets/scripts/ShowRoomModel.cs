using Meta.XR.MRUtilityKit;
using UnityEngine;

public class ShowRoomModel : MonoBehaviour
{
    [SerializeField] private GameObject effectMeshParent; // Parent GameObject containing all effect meshes
    [SerializeField] private float transitionDuration = 1f; // Duration of the smooth transition

    private EffectMesh[] effectMeshes; // Array to hold all EffectMesh components
    private bool isMeshHidden = true; // Current state of the "Hide Mesh" property

    private void Start()
    {
        // Find all EffectMesh components in the parent GameObject
        if (effectMeshParent != null)
        {
            effectMeshes = effectMeshParent.GetComponentsInChildren<EffectMesh>();
            SetHideMeshState(true); // Initialize with meshes hidden
        }
        else
        {
            Debug.LogError("Effect Mesh Parent is not assigned!");
        }
    }

    public void ToggleRoomModel()
    {
        // Toggle the Hide Mesh state
        isMeshHidden = !isMeshHidden;

        // Smoothly transition the visibility
        StopAllCoroutines(); // Stop any ongoing transitions
        StartCoroutine(SmoothTransition());
    }

    private System.Collections.IEnumerator SmoothTransition()
    {
        float elapsedTime = 0f;
        float startAlpha = isMeshHidden ? 1f : 0f; // Start with the opposite of target state
        float targetAlpha = isMeshHidden ? 0f : 1f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / transitionDuration);

            SetMeshAlpha(alpha); // Update the alpha during the transition
            yield return null;
        }

        // Ensure the final state
        SetMeshAlpha(targetAlpha);
        SetHideMeshState(isMeshHidden); // Update "Hide Mesh" property after transition
    }

    private void SetHideMeshState(bool hide)
    {
        foreach (var effectMesh in effectMeshes)
        {
            if (effectMesh != null)
            {
                effectMesh.HideMesh = hide;
            }
        }
    }

    private void SetMeshAlpha(float alpha)
    {
        foreach (var effectMesh in effectMeshes)
        {
            if (effectMesh != null && effectMesh.TryGetComponent<Renderer>(out var renderer))
            {
                foreach (var material in renderer.materials)
                {
                    if (material.HasProperty("_Color"))
                    {
                        Color color = material.color;
                        color.a = alpha;
                        material.color = color;
                    }
                }
            }
        }
    }
}
