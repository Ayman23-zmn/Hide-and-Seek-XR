using UnityEngine;

public class SmoothUIFollow : MonoBehaviour
{
    [SerializeField] private Transform centerEyeAnchor; // Reference to the center eye anchor (player's headset/camera)
    [SerializeField] private Vector3 offset = new Vector3(0, -0.2f, 1f); // Offset to position the UI relative to the player
    [SerializeField] private float followSpeed = 5f; // Speed of the UI's movement

    private void LateUpdate()
    {
        if (centerEyeAnchor == null)
        {
            Debug.LogWarning("CenterEyeAnchor is not assigned. Please assign it in the Inspector.");
            return;
        }

        // Calculate the target position for the UI
        Vector3 targetPosition = centerEyeAnchor.position + centerEyeAnchor.forward * offset.z +
                                 centerEyeAnchor.up * offset.y +
                                 centerEyeAnchor.right * offset.x;

        // Smoothly move the UI to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Make the UI always face the player
        Quaternion targetRotation = Quaternion.LookRotation(centerEyeAnchor.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, followSpeed * Time.deltaTime);
    }
}
