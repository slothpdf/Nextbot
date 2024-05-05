using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerControls : MonoBehaviour
{
    public float walkSpeed = 17f;
    public float runSpeed = 30f; // Speed when running
    private float speed; // Current speed (either walk or run)
    public float gravity = -9.8f;

    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;

    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float verticalRot = 0;
    private CharacterController charController;
    private PlayerJump playerJump;

    private bool isRunning = false;
    private int jumpCount = 0; // Number of jumps
    private int maxJumpCount = 4; // Maximum number of jumps

    void Start()
    {
        charController = GetComponent<CharacterController>();
        playerJump = GetComponent<PlayerJump>();

        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        speed = walkSpeed; //walking
    }

    void Update()
    {
        // Jump logic
        if (charController.isGrounded)
        {
            jumpCount = 0; // Reset jump count when grounded
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
            speed = runSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            speed = walkSpeed;
        }

        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaZ, 0, -deltaX);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        charController.Move(movement);

        // Rotation logic
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

            Camera.main.transform.localEulerAngles = new Vector3(verticalRot, Camera.main.transform.localEulerAngles.y, 0);
        }
        else
        {
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float horizontalRot = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(0, horizontalRot, 0);
            Camera.main.transform.localEulerAngles = new Vector3(verticalRot, Camera.main.transform.localEulerAngles.y, 0);
        }

        // Reset mouse position to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Player momentum logic
        if (charController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount < maxJumpCount)
            {
                playerJump.Jump();
                if (isRunning && speed < runSpeed + (maxJumpCount - 1) * 5) // Check if speed is less than the maximum possible speed
                {
                    speed += 5; // Increase speed by 10 after each jump when running
                }
                jumpCount++;
                Debug.Log("Speed: " + speed);
            }
        }
    }
}



