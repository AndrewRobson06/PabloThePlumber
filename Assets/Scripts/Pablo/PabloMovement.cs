using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PabloMovement : MonoBehaviour
{
    [Header("Movement & Jump")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpSpeed = 8f;
    [SerializeField] private float risingGravity = 16f;
    [SerializeField] private float fallingGravity = 30f;

    [Header("Wall Jump")]
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float wallSlideSpeed = -2.5f;
    [SerializeField] private float wallJumpUpForce = 8f;
    [SerializeField] private float wallJumpAwayForce = 10f;
    [SerializeField] private float wallInputLockTime = 0.2f;

    private CharacterController controller;
    private Vector3 velocity;
    private Vector3 wallNormal;
    private Collider lastWallCollider;
    private Collider currentWallCollider;
    private bool isTouchingWall;
    private float inputLockTimer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        bool jumpPressed = keyboard != null && keyboard.spaceKey.wasPressedThisFrame;

        if (controller.isGrounded && velocity.y <= 0f)
        {
            velocity.y = -2f;
            lastWallCollider = null;
        }

        if (inputLockTimer > 0f)
        {
            inputLockTimer -= Time.deltaTime;
        }
        else
        {
            Vector3 moveDirection = GetCameraRelativeDirection(keyboard);
            velocity.x = moveDirection.x * moveSpeed;
            velocity.z = moveDirection.z * moveSpeed;

            if (moveDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveDirection);
            }
        }

        if (jumpPressed)
        {
            if (controller.isGrounded)
            {
                velocity.y = jumpSpeed;
            }
            else if (isTouchingWall && currentWallCollider != lastWallCollider)
            {
                velocity = (wallNormal * wallJumpAwayForce) + (Vector3.up * wallJumpUpForce);
                lastWallCollider = currentWallCollider;
                inputLockTimer = wallInputLockTime;
                isTouchingWall = false;

                Vector3 lookDir = new Vector3(wallNormal.x, 0f, wallNormal.z);
                if (lookDir != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(lookDir);
                }
            }
        }

        ApplyGravityOrWallSlide();

        isTouchingWall = false;
        currentWallCollider = null;

        controller.Move(velocity * Time.deltaTime);
    }

    private Vector3 GetCameraRelativeDirection(Keyboard keyboard)
    {
        if (keyboard == null)
        {
            return Vector3.zero;
        }

        float cameraY = Camera.main != null ? Camera.main.transform.eulerAngles.y : 0f;
        Quaternion cameraTurn = Quaternion.Euler(0f, cameraY, 0f);
        Vector3 direction = Vector3.zero;

        if (keyboard.wKey.isPressed) direction += cameraTurn * Vector3.forward;
        if (keyboard.sKey.isPressed) direction += cameraTurn * Vector3.back;
        if (keyboard.aKey.isPressed) direction += cameraTurn * Vector3.left;
        if (keyboard.dKey.isPressed) direction += cameraTurn * Vector3.right;

        return direction.normalized;
    }

    private void ApplyGravityOrWallSlide()
    {
        if (isTouchingWall && !controller.isGrounded && velocity.y < wallSlideSpeed)
        {
            velocity.y = wallSlideSpeed;
            return;
        }

        float gravityRate = velocity.y > 6f ? risingGravity : fallingGravity;
        velocity.y -= gravityRate * Time.deltaTime;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (((1 << hit.gameObject.layer) & wallLayer) != 0 && Mathf.Abs(hit.normal.y) < 0.1f)
        {
            if (!controller.isGrounded)
            {
                isTouchingWall = true;
                wallNormal = hit.normal;
                currentWallCollider = hit.collider;
            }
        }
    }
}
