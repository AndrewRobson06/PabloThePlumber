using UnityEngine;
using UnityEngine.InputSystem;

public class PabloMovement : MonoBehaviour
{
    private float verticalSpeed;
    float moveSpeed = 8f;

    private CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        MovePlayer(keyboard);
        JumpGravity(keyboard);
    }

    void MovePlayer(Keyboard keyboard)
    {
        float cameraY = Camera.main.transform.eulerAngles.y;    // Camera y Angle reference
        Quaternion cameraTurn = Quaternion.Euler(0f, cameraY, 0f); // Create a variable to add the camera offset

        Vector3 direction = Vector3.zero;

        // Moves player relative to cameras direction

        // Forward
        if (keyboard != null && keyboard.wKey.isPressed) direction += cameraTurn * Vector3.forward;

        // Back
        if (keyboard != null && keyboard.sKey.isPressed) direction += cameraTurn * Vector3.back;

        // Left
        if (keyboard != null && keyboard.aKey.isPressed) direction += cameraTurn * Vector3.left;

        // Right
        if (keyboard != null && keyboard.dKey.isPressed) direction += cameraTurn * Vector3.right;

        // Applies
        Vector3 velocity = direction.normalized * moveSpeed;
        velocity.y = verticalSpeed;

        controller.Move(velocity * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void JumpGravity(Keyboard keyboard)
    {
        // applies jump when on ground
        if (controller.isGrounded && verticalSpeed <= 0f)
        {
            verticalSpeed = -2f;

            if (keyboard != null && keyboard.spaceKey.wasPressedThisFrame)
            {
                verticalSpeed = 8f;
            }
        }

        // increases falling early when falling speed reaches 6f
        if (verticalSpeed > 6f)
        {
            // Pablo is rising, keep gravity normal.
            verticalSpeed -= 16f * Time.deltaTime;
        }
        else
        {
            // Pablo is falling, increase gravity.
            verticalSpeed -= 30f * Time.deltaTime;
        }
    }
}
