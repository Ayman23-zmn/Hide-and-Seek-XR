using UnityEngine;

public class RecenterOnLongPress : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Start()
    {
        // Store the initial position and rotation of the player root object (OVRCameraRig)
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        // Check if the Oculus Button (long press) is triggered
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            // Recenter head and controller pose
            OVRManager.display.RecenterPose();

            // Reset the OVRCameraRig position and rotation to avoid unwanted teleporting
            transform.position = initialPosition;
            transform.rotation = initialRotation;

            Debug.Log("Recenter triggered for OVRCameraRig!");
        }
    }
}
