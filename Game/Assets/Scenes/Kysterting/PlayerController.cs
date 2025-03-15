using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runModifier;
    public float sensitivity = 2f; // Adjust for faster/slower look speed
    public Transform cameraTransform; // Assign your camera in the Inspector

    public float currentStamina, maxStamina, staminaRegainRate, staminaConsumeRate;
    bool consumingStamina;
    public RectTransform staminaBarUI;

    private Vector3 moveDirection;
    private Rigidbody rb;
    private float pitch = 0f; // Vertical rotation (clamped)


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Start()
    {
        currentStamina = maxStamina;

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMoveUp(InputAction.CallbackContext context)
    {
        moveDirection.z = context.performed ? 1f : 0f;
    }

    public void OnMoveDown(InputAction.CallbackContext context)
    {
        moveDirection.z = context.performed ? -1f : 0f;
    }

    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        moveDirection.x = context.performed ? -1f : 0f;
    }

    public void OnMoveRight(InputAction.CallbackContext context)
    {
        moveDirection.x = context.performed ? 1f : 0f;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookInput = context.ReadValue<Vector2>();
        float yaw = lookInput.x * sensitivity * Time.deltaTime;  // Horizontal look
        float pitchChange = -lookInput.y * sensitivity * Time.deltaTime;  // Vertical look (inverted)

        // Apply rotation
        transform.Rotate(Vector3.up, yaw);

        // Clamp vertical rotation to prevent flipping
        pitch = Mathf.Clamp(pitch + pitchChange, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    private void FixedUpdate()
    {
        // Get the camera's forward and right directions (in local space)
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Flatten them to prevent moving on the Y-axis (up/down)
        forward.y = 0f;
        right.y = 0f;

        // Normalize the vectors
        forward.Normalize();
        right.Normalize();

        // Combine the input movement with the camera's direction
        Vector3 move = forward * moveDirection.z + right * moveDirection.x;

        // Apply movement
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina >= 0)
        {
            rb.MovePosition(rb.position + move * moveSpeed * runModifier * Time.fixedDeltaTime);
            consumingStamina = true;
        }
        else
        {
            rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
            consumingStamina= false;
        }

        UpdateStamina();
    }

    public void UpdateStamina()
    {
        if(consumingStamina == true)
        {
            currentStamina -= staminaConsumeRate * Time.fixedDeltaTime;

            if (Mathf.Abs(rb.angularVelocity.magnitude) > 0) //Dette skal bruges til, at man kun dræner stamina NÅR man bevæger sig!
            {
               
            }
        }
        else if(consumingStamina == false && currentStamina < maxStamina)
        {
            currentStamina += staminaRegainRate * Time.fixedDeltaTime;

        }

        Mathf.Clamp(currentStamina, 0, 100); 

        //Update stamina UI
        float barHeight = currentStamina;            
        Mathf.Clamp(barHeight, 0, maxStamina);
        staminaBarUI.sizeDelta = new Vector2(staminaBarUI.sizeDelta.x, barHeight);
    }
}
