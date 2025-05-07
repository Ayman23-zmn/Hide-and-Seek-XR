using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class MonsterHider : MonoBehaviour
{
    [SerializeField] private float hideInterval;    // Interval between hiding
    [SerializeField] private float moveSpeed;       // Speed for smooth movement
    [SerializeField] private Vector3 defaultOffset; // Default offset for hiding

    private List<MRUKAnchor> hidingSpots = new List<MRUKAnchor>(); // Valid hiding spots
    private List<GameObject> monsters = new List<GameObject>();    // List of monster objects
    private bool initialized = false;

    private void Start()
    {
        // Populate the monsters list with all child GameObjects
        foreach (Transform child in transform)
        {
            monsters.Add(child.gameObject);
        }

        // Start the initialization process to wait for effect meshes to be ready
        StartCoroutine(InitializeHidingSpots());
    }

    private IEnumerator InitializeHidingSpots()
    {
        while (!initialized)
        {
            // Attempt to find valid hiding spots
            hidingSpots = FindValidHidingSpots();


            if (hidingSpots.Count > 0)
            {
                initialized = true;
                Debug.Log("Hiding spots initialized successfully.");
                StartCoroutine(HideRoutine());
            }
            else
            {
                Debug.LogWarning("No valid hiding spots found yet. Retrying...");
                yield return new WaitForSeconds(1f); // Wait before retrying
            }
        }
    }

    private List<MRUKAnchor> FindValidHidingSpots()
    {
        List<MRUKAnchor> validSpots = new List<MRUKAnchor>();

        // Find all MRUKAnchor objects in the scene
        MRUKAnchor[] allAnchors = FindObjectsOfType<MRUKAnchor>();

        foreach (var anchor in allAnchors)
        {
            // Check if the anchor has desired labels (e.g., TABLE, SCREEN, etc.)
            if (anchor.HasAnyLabel(MRUKAnchor.SceneLabels.TABLE |
                                   MRUKAnchor.SceneLabels.SCREEN |
                                   MRUKAnchor.SceneLabels.STORAGE |
                                   MRUKAnchor.SceneLabels.OTHER))
            {
                validSpots.Add(anchor);
                Debug.Log($"Found valid hiding spot: {anchor.name}");
            }
        }

        return validSpots;
    }

    private IEnumerator HideRoutine()
    {
        while (true)
        {
            foreach (var monster in monsters)
            {
                if (monster == null)
                {
                    Debug.LogWarning("Monster reference is missing. Skipping.");
                    continue;
                }

                // Choose a random hiding spot
                MRUKAnchor randomSpot = hidingSpots[Random.Range(0, hidingSpots.Count)];

                // Find a valid hiding position
                Vector3 hidingPosition = FindValidHidingPosition(randomSpot);

                // Smoothly move the monster to the hiding spot
                yield return StartCoroutine(MoveToPosition(monster, hidingPosition));

                Debug.Log($"Monster {monster.name} hidden at {randomSpot.name}");
            }

            // Wait for the next hide interval
            yield return new WaitForSeconds(hideInterval);
        }
    }

    private Vector3 FindValidHidingPosition(MRUKAnchor anchor)
    {
        Vector3 position = anchor.GetAnchorCenter(); // Start at anchor center
        int maxAttempts = 10; // Limit the number of adjustments to avoid infinite loops
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            if (!anchor.IsPositionInVolume(position, testVerticalBounds: true, distanceBuffer: 0.1f))
            {
                Debug.Log($"Found valid position: {position} for anchor {anchor.name}");
                return position; // Valid position found
            }

            // Adjust position by the default offset
            position += defaultOffset;
            attempts++;
            Debug.LogWarning($"Adjusting position for {anchor.name}. Attempt {attempts}");
        }

        Debug.LogError($"Could not find a valid position for anchor {anchor.name}. Defaulting to anchor center.");
        return anchor.GetAnchorCenter(); // Fallback to the anchor center if no valid position is found
    }

    private IEnumerator MoveToPosition(GameObject monster, Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = monster.transform.position;

        while (elapsedTime < 1f / moveSpeed)
        {
            // Smoothly move the monster to the target position
            monster.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime * moveSpeed);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure final position
        monster.transform.position = targetPosition;
    }
}



/*using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    [SerializeField] private GameObject monster;
    [SerializeField] private float force = 4.0f;

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            Vector3 controllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            Quaternion controllerRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
            GameObject spawnedMonster = Instantiate(monster,controllerPosition, controllerRotation);
            Rigidbody rigidbody = spawnedMonster.GetComponent<Rigidbody>();
            rigidbody.velocity = controllerRotation * Vector3.forward * force;
        }

    }


}*/





