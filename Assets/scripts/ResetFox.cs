using UnityEngine;
using Meta.XR.MRUtilityKit;

public class FoxResetter : MonoBehaviour
{
    public GameObject foxObject;     // Reference to the fox GameObject
    public GameObject feetCollider; // Reference to the feet collider

    // Method to reset the position and rotation of the fox
    public void ResetFox()
    {
        if (foxObject != null)
        {
            // Reset fox's position and rotation
            foxObject.transform.position = Vector3.zero;
            foxObject.transform.rotation = Quaternion.identity;

            // Adjust position to the floor
            PlaceOnFloor();

            Debug.Log("Fox position and rotation have been reset.");
        }
        else
        {
            Debug.LogError("No fox object assigned. Please assign a fox object.");
        }
    }

    // Method to place the fox on the floor
    public void PlaceOnFloor()
    {
        MRUKAnchor[] anchors = FindObjectsOfType<MRUKAnchor>();
        foreach (var anchor in anchors)
        {
            if (anchor.HasAnyLabel(MRUKAnchor.SceneLabels.FLOOR))
            {
                Vector3 floorPosition = anchor.GetAnchorCenter(); // Get the center of the floor anchor

                // Adjust fox position to place its feet collider on the floor
                Vector3 feetOffset = feetCollider.transform.position - foxObject.transform.position;
                foxObject.transform.position = new Vector3(
                    foxObject.transform.position.x,
                    floorPosition.y - feetOffset.y,
                    foxObject.transform.position.z
                );

                Debug.Log($"Fox placed on floor: {anchor.name}");
                return;
            }
        }

        Debug.LogWarning("No FLOOR anchor found. Fox position remains unchanged.");
    }
}
