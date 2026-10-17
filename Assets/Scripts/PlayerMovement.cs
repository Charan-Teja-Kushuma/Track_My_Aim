
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float deceleration = 25f;

    [Header("Jump and Gravity")]
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -20f;

    [Header("Ground Check")]
    [SerializeField] private float groundCheckDistance = 0.15f;

    private CharacterController controller;
    private Vector3 horizontalVelocity;
    private float verticalVelocity;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CheckGrounded();
        HandleMovement();
        HandleJump();
        ApplyGravity();
        MovePlayer();
    }

    private void CheckGrounded()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f;

        IsGrounded = Physics.SphereCast(
            origin,
            controller.radius * 0.9f,
            Vector3.down,
            out _,
            controller.height * 0.5f + groundCheckDistance,
            Physics.DefaultRaycastLayers,
            QueryTriggerInteraction.Ignore
        );

        if (IsGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }
    }

    private void HandleMovement()
    {
        Vector2 input = Vector2.zero;
        Keyboard keyboard = Keyboard.current;

        if (keyboard != null)
        {
            if (keyboard.wKey.isPressed) input.y += 1f;
            if (keyboard.sKey.isPressed) input.y -= 1f;
            if (keyboard.dKey.isPressed) input.x += 1f;
            if (keyboard.aKey.isPressed) input.x -= 1f;
        }

        input = Vector2.ClampMagnitude(input, 1f);

        Vector3 moveDirection =
            transform.right * input.x +
            transform.forward * input.y;

        bool sprinting = keyboard != null &&
                         keyboard.leftShiftKey.isPressed;

        float targetSpeed = sprinting ? sprintSpeed : walkSpeed;
        Vector3 targetVelocity = moveDirection * targetSpeed;

        float rate = input.sqrMagnitude > 0f
            ? acceleration
            : deceleration;

        horizontalVelocity = Vector3.MoveTowards(
            horizontalVelocity,
            targetVelocity,
            rate * Time.deltaTime
        );
    }

    private void HandleJump()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard != null &&
            keyboard.spaceKey.wasPressedThisFrame &&
            IsGrounded)
        {
            verticalVelocity = Mathf.Sqrt(
                jumpHeight * -2f * gravity
            );
        }
    }

    private void ApplyGravity()
    {
        verticalVelocity += gravity * Time.deltaTime;
    }

    private void MovePlayer()
    {
        Vector3 velocity = horizontalVelocity;
        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);
    }
}
