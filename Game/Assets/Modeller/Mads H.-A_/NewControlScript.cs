using System;
using UnityEngine;

public class NewControlScript : MonoBehaviour
{

    public Camera Look;

    public Rigidbody body;
    
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;

    private float veritcalRotation = 0f;
    public float MaxLookAngle = 80f;

    private void Update()
    {
        //movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = body.transform.forward * vertical + body.transform.right * horizontal;
        body.MovePosition(body.position+moveDirection*moveSpeed * Time.deltaTime);
        
        //mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        //body rotaion
        body.transform.Rotate(Vector3.up*mouseX);
        
        //Camera up and down
        veritcalRotation -= mouseY;
        veritcalRotation = Mathf.Clamp(veritcalRotation, -MaxLookAngle, MaxLookAngle);
        Look.transform.localRotation = Quaternion.Euler(veritcalRotation,0,0);

    }
}
