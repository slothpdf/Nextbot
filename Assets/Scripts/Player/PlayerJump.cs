using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerJump : MonoBehaviour
{
    public float baseJumpHeight = 12.0f;
    public float gravity = -18.0f;

    private CharacterController charController;
    private float verticalSpeed;

    void Start()
    {
        charController = GetComponent<CharacterController>();
        verticalSpeed = 0.0f;
    }

    void Update()
    {
        if (charController.isGrounded)
        {
            verticalSpeed = -gravity * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                float jumpForce = Mathf.Sqrt(2 * CalculateJumpHeight() * -gravity);
                verticalSpeed = jumpForce;
            }
        }
        else
        {
            float slopeAngle = Vector3.Angle(Vector3.up, charController.transform.forward);
            float slopeMultiplier = Mathf.Clamp01(slopeAngle / 45f); 

            float slopeGravity = Mathf.Lerp(gravity, gravity * 2f, slopeMultiplier);
            verticalSpeed += slopeGravity * Time.deltaTime;
        }

        Vector3 moveDirection = new Vector3(0, verticalSpeed, 0);
        charController.Move(moveDirection * Time.deltaTime);
    }

    float CalculateJumpHeight()
    {
        float slopeAngle = Vector3.Angle(Vector3.up, charController.transform.forward);
        float slopeMultiplier = Mathf.Clamp01(slopeAngle / 45f); 
        return baseJumpHeight / slopeMultiplier;
    }
}



