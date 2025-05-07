/*using UnityEngine;

public class FoxBehavior : MonoBehaviour
{
    [SerializeField] private Transform centerEyeAnchor; // Reference to the camera or center eye anchor
    [SerializeField] private float leashDistance = 5f; // Distance at which the fox waits
    [SerializeField] private float followSpeed = 3f; // Speed at which the fox follows
    [SerializeField] private float rotationSpeed = 2f; // Speed of fox's rotation
    private Animator animator; // Reference to Unity Animator component

    private bool isFollowing = false;
    private bool hasReachedLeashDistance = false; // Check if the fox is at the leash distance

    private void Start()
    {
        animator = GetComponent<Animator>(); // Initialize the Animator component
        if (animator == null)
        {
            Debug.LogError("Animator component is missing. Please attach an Animator to the fox.");
        }
    }

    private void Update()
    {
        if (centerEyeAnchor == null)
        {
            Debug.LogWarning("CenterEyeAnchor is not assigned. Please assign it in the Inspector.");
            return;
        }

        // Calculate the distance between the fox and the center eye anchor
        float distance = Vector3.Distance(transform.position, centerEyeAnchor.position);

        if (distance > leashDistance)
        {
            // Follow the player if they are beyond the leash distance
            FollowPlayer();
            hasReachedLeashDistance = false; // Reset the leash state
        }
        else
        {
            // Wait at the leash distance
            WaitAtLeashDistance();
        }
    }

    private void FollowPlayer()
    {
        isFollowing = true;

        // Calculate the direction towards the center eye anchor
        Vector3 direction = (centerEyeAnchor.position - transform.position).normalized;

        // Rotate the fox to face the player
        RotateTowards(centerEyeAnchor.position);

        // Update the animation to show running
        if (animator != null)
        {
            animator.SetBool("isMoving", true); // Start the running animation
        }

        // Move towards the player
        transform.position = Vector3.MoveTowards(transform.position, centerEyeAnchor.position - direction * leashDistance, followSpeed * Time.deltaTime);

        Debug.Log("Fox is following the player.");
    }

    private void WaitAtLeashDistance()
    {
        if (!hasReachedLeashDistance)
        {
            Debug.Log("Fox has reached the leash distance and is now waiting.");
            isFollowing = false;
            hasReachedLeashDistance = true; // Mark that the fox is at leash distance

            // Update the animation to stop running
            if (animator != null)
            {
                animator.SetBool("isMoving", false); // Stop the running animation
            }

            // Rotate the fox to face forward (straight) instead of the player
            FaceForward();
        }
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        // Calculate the direction to the target
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;
        directionToTarget.y = 0; // Keep the rotation horizontal

        // Smoothly rotate the fox to face the target
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        Debug.Log("Fox is rotating towards the player.");
    }

    private void FaceForward()
    {
        // Define a "forward" direction based on the world space
        Vector3 forwardDirection = new Vector3(0, 0, 1); // Modify if you want a specific direction
        Quaternion targetRotation = Quaternion.LookRotation(forwardDirection);

        // Smoothly rotate the fox to face forward
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        Debug.Log("Fox is now facing forward.");
    }
}
*/

using UnityEngine;
using System.Collections;

public class FoxBehavior : MonoBehaviour
{
    [SerializeField] private Transform centerEyeAnchor; // Reference to the camera or center eye anchor
    [SerializeField] private float leashDistance = 5f; // Distance at which the fox waits
    [SerializeField] private float followSpeed = 3f; // Speed at which the fox follows
    [SerializeField] private float rotationSpeed = 2f; // Speed of fox's rotation
    private Animator animator; // Reference to Unity Animator component

    private bool isFollowing = false;
    private bool hasReachedLeashDistance = false; // Check if the fox is at the leash distance
    private bool canStartFollowing = false; // Flag to delay the behavior

    private void Start()
    {
        animator = GetComponent<Animator>(); // Initialize the Animator component
        if (animator == null)
        {
            Debug.LogError("Animator component is missing. Please attach an Animator to the fox.");
        }

        // Start the delay coroutine
        StartCoroutine(StartFollowingAfterDelay(3f));
    }

    private IEnumerator StartFollowingAfterDelay(float delay)
    {
        Debug.Log($"Fox will start following after {delay} seconds.");
        yield return new WaitForSeconds(delay);
        canStartFollowing = true;
        Debug.Log("Fox is now ready to start following.");
    }

    private void Update()
    {
        if (!canStartFollowing) return; // Wait until the delay is over

        if (centerEyeAnchor == null)
        {
            Debug.LogWarning("CenterEyeAnchor is not assigned. Please assign it in the Inspector.");
            return;
        }

        // Calculate the distance between the fox and the center eye anchor
        float distance = Vector3.Distance(transform.position, centerEyeAnchor.position);

        if (distance > leashDistance)
        {
            // Follow the player if they are beyond the leash distance
            FollowPlayer();
            hasReachedLeashDistance = false; // Reset the leash state
        }
        else
        {
            // Wait at the leash distance
            WaitAtLeashDistance();
        }
    }

    private void FollowPlayer()
    {
        isFollowing = true;

        // Calculate the direction towards the center eye anchor
        Vector3 direction = (centerEyeAnchor.position - transform.position).normalized;

        // Rotate the fox to face the player
        RotateTowards(centerEyeAnchor.position);

        // Update the animation to show running
        if (animator != null)
        {
            animator.SetBool("isMoving", true); // Start the running animation
        }

        // Move towards the player
        transform.position = Vector3.MoveTowards(transform.position, centerEyeAnchor.position - direction * leashDistance, followSpeed * Time.deltaTime);

        Debug.Log("Fox is following the player.");
    }

    private void WaitAtLeashDistance()
    {
        if (!hasReachedLeashDistance)
        {
            Debug.Log("Fox has reached the leash distance and is now waiting.");
            isFollowing = false;
            hasReachedLeashDistance = true; // Mark that the fox is at leash distance

            // Update the animation to stop running
            if (animator != null)
            {
                animator.SetBool("isMoving", false); // Stop the running animation
            }

            // Rotate the fox to face forward (straight) instead of the player
            FaceForward();
        }
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        // Calculate the direction to the target
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;
        directionToTarget.y = 0; // Keep the rotation horizontal

        // Smoothly rotate the fox to face the target
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        Debug.Log("Fox is rotating towards the player.");
    }

    private void FaceForward()
    {
        // Define a "forward" direction based on the world space
        Vector3 forwardDirection = new Vector3(0, 0, 1); // Modify if you want a specific direction
        Quaternion targetRotation = Quaternion.LookRotation(forwardDirection);

        // Smoothly rotate the fox to face forward
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        Debug.Log("Fox is now facing forward.");
    }
}

