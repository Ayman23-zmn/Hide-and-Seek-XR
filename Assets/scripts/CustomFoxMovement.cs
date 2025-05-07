/*using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Meta.XR.MRUtilityKit;
using System.Collections;

public class CustomFoxMovement : MonoBehaviour
{
    public float speed = 0.5f;     // Speed at which the fox moves
    public float jumpForce = 5.0f; // Force applied for jumping (adjustable in Inspector)
    public float rotationSpeed; // Speed for smooth rotation towards the movement direction
    private Animator animator;     // Reference to Unity Animator component
    private Rigidbody rb;          // Reference to the fox's Rigidbody
    private bool isGrounded = true; // Flag to check if the fox is grounded or not
    public AudioSource jumpSound; // Audio source for fox jump sound
    public GameObject feetCollider; // Feet Collider Game object
    private Vector3 lastValidPosition; // Tracks the last valid position of the fox
    public float yThreshold = -5f; // Y threshold for respawn


    private void Start()
    {
        animator = GetComponent<Animator>(); // Get animator component
        rb = GetComponent<Rigidbody>();      // Get Rigidbody component

    }

    void Update()
    {
        if (transform.position.y < yThreshold)
        {
            Debug.LogWarning("Fox fell below the Y threshold. Respawning...");
            RespawnFox();
        }

        // Get the right-hand/left-hand controller's thumbstick input
        Vector2 inputAxis;
        InputDevice rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        // Check if the right controller exists and get the thumbstick value
        if (leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis))
        {
            // If character is moving (i.e., there is stick movement)
            if (inputAxis.sqrMagnitude > 0.01f)
            {
                // Set the moving animation to true
                animator.SetBool("isMoving", true);

                // Calculate the movement direction based on the input
                Vector3 moveDirection = new Vector3(inputAxis.x, 0, inputAxis.y);

                // Move and rotate the fox towards the movement direction
                MoveAndRotate(moveDirection);
            }
            else
            {
                // If no movement, set to idle animation
                animator.SetBool("isMoving", false);
            }
        }

        // Jump if 'A' button is pressed and fox is grounded
        if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool isJumping) && isJumping && isGrounded)
        {
            Jump();
        }
    }

    // Method to move and rotate the fox in the direction of movement
    private void MoveAndRotate(Vector3 moveDirection)
    {
        // Move the fox
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        // Check if there is any movement to avoid rotating without direction
        if (moveDirection != Vector3.zero)
        {
            // Calculate the target rotation based on movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            // Smoothly rotate the fox towards the movement direction
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    // Method to jump (apply upward force)
    private void Jump()
    {
        if (isGrounded)
        {
            jumpSound.Play(); // Play jump sound
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isGrounded = false; // Set to false as the fox is now in the air
            Debug.Log("Fox jumped. Grounded: " + isGrounded);
        }
    }

    // Method to check if the fox is grounded using collision
    private void OnCollisionEnter(Collision collision)
    {
        // Only set grounded to true if the feet collider is involved
        if (collision.gameObject == feetCollider || collision.contacts.Length > 0 && collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
            Debug.Log($"Fox landed on: {collision.gameObject.name}. Grounded: {isGrounded}");
        }
    }

    // Method to detect when the fox leaves the ground
    private void OnCollisionExit(Collision collision)
    {
        // Only set grounded to false if the feet collider is involved
        if (collision.gameObject == feetCollider)
        {
            isGrounded = false;
            Debug.Log("Fox left ground. Grounded: " + isGrounded);
        }
    }

    private void RespawnFox()
    {
        transform.position = lastValidPosition; // Respawn at the last valid position
        rb.velocity = Vector3.zero; // Reset velocity
        Debug.Log("Fox respawned at last valid position.");
    }

}


*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Meta.XR.MRUtilityKit;
using System.Collections;

public class CustomFoxMovement : MonoBehaviour
{
    public float speed = 0.5f;     // Speed at which the fox moves
    public float jumpForce = 5.0f; // Force applied for jumping (adjustable in Inspector)
    public float rotationSpeed; // Speed for smooth rotation towards the movement direction
    private Animator animator;     // Reference to Unity Animator component
    private Rigidbody rb;          // Reference to the fox's Rigidbody
    private bool isGrounded = true; // Flag to check if the fox is grounded or not
    public AudioSource jumpSound; // Audio source for fox jump sound
    public GameObject feetCollider; // Feet Collider Game object
    private Vector3 lastValidPosition; // Tracks the last valid position of the fox
    public float yThreshold = -5f; // Y threshold for respawn
    public GameObject foxObject; // Reference to the fox object itself

    private void Start()
    {
        animator = GetComponent<Animator>(); // Get animator component
        rb = GetComponent<Rigidbody>();      // Get Rigidbody component

        PlaceOnFloor(); // Place the fox on the floor at startup
    }

    void Update()
    {
        if (transform.position.y < yThreshold)
        {
            Debug.LogWarning("Fox fell below the Y threshold. Respawning...");
            RespawnFox();
        }

        // Get the right-hand/left-hand controller's thumbstick input
        Vector2 inputAxis;
        InputDevice rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        // Check if the right controller exists and get the thumbstick value
        if (leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis))
        {
            // If character is moving (i.e., there is stick movement)
            if (inputAxis.sqrMagnitude > 0.01f)
            {
                // Set the moving animation to true
                animator.SetBool("isMoving", true);

                // Calculate the movement direction based on the input
                Vector3 moveDirection = new Vector3(inputAxis.x, 0, inputAxis.y);

                // Move and rotate the fox towards the movement direction
                MoveAndRotate(moveDirection);
            }
            else
            {
                // If no movement, set to idle animation
                animator.SetBool("isMoving", false);
            }
        }

        // Jump if 'A' button is pressed and fox is grounded
        if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool isJumping) && isJumping && isGrounded)
        {
            Jump();
        }
    }

    private void MoveAndRotate(Vector3 moveDirection)
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            jumpSound.Play(); // Play jump sound
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            isGrounded = false; // Set to false as the fox is now in the air
            Debug.Log("Fox jumped. Grounded: " + isGrounded);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts.Length > 0 && collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
            lastValidPosition = transform.position; // Update last valid position
            Debug.Log($"Fox landed on: {collision.gameObject.name}. Grounded: {isGrounded}");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
        Debug.Log("Fox left ground. Grounded: " + isGrounded);
    }

    private void RespawnFox()
    {
        transform.position = lastValidPosition; // Respawn at the last valid position
        rb.linearVelocity = Vector3.zero; // Reset velocity
        Debug.Log("Fox respawned at last valid position.");
    }

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
                lastValidPosition = foxObject.transform.position; // Update last valid position
                return;
            }
        }

        Debug.LogWarning("No FLOOR anchor found. Fox position remains unchanged.");
    }
}
