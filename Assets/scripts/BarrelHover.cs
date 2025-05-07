using UnityEngine;

public class BarrelHover : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0f, 50f, 0f); // Speed of rotation along each axis

    [Header("Hover Settings")]
    [SerializeField] private float hoverAmplitude = 0.5f; // Amplitude of the hover (distance up and down)
    [SerializeField] private float hoverSpeed = 2f;       // Speed of the hover

    private Vector3 initialPosition;


    private void Start()
    {
        // Store the initial position of the barrel
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Apply rotation
        RotateBarrel();

        // Apply hover motion
        HoverBarrel();
    }


    private void RotateBarrel()
    {

        // Rotate the barrel based on rotationSpeed
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    private void HoverBarrel()
    {
        // Calculate the new Y position using a sine wave for smooth up and down motion
        float newY = initialPosition.y + Mathf.Sin(Time.time * hoverSpeed) * hoverAmplitude;

        // Apply the new position while keeping X and Z constant
        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
}
