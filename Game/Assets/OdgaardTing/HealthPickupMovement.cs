using UnityEngine;

public class HealthPickupMovement : MonoBehaviour
{
    public float hoverHeight = 0.5f;    // How high it will move
    public float hoverSpeed = 2f;       // Speed of hovering up and down
    public float rotationSpeedX = 45f;  // Speed of rotation around the X-axis
    public float rotationSpeedY = 45f;  // Speed of rotation around the Y-axis
    public float rotationSpeedZ = 45f;  // Speed of rotation around the Z-axis

    private Vector3 startPosition;

    void Start()
    {
        // Store the initial position to offset the hover movement from its start position
        startPosition = transform.position;
    }

    void Update()
    {
        // Hover movement: Oscillate up and down using Mathf.Sin
        float hoverOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = startPosition + new Vector3(0, hoverOffset, 0);

        // Rotation: Rotate independently around X, Y, and Z axes
        transform.Rotate(rotationSpeedX * Time.deltaTime, rotationSpeedY * Time.deltaTime, rotationSpeedZ * Time.deltaTime);
    }
}