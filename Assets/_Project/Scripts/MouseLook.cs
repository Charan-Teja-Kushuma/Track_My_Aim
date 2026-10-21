
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerBody;

    [Header("Mouse Settings")]
    [SerializeField] private float sensitivity = 0.1f;
    [SerializeField] private bool invertY = false;

    [Header("Vertical Look Limits")]
    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 80f;


    [Header("Aim Down Sights (ADS)")]
    [SerializeField] private float normalFOV = 60f;
    [SerializeField] private float adsFOV = 40f;
    [SerializeField] private float adsTransitionSpeed = 10f;

    private Camera playerCamera;


    private float pitch;

    private void Start()
    {
        if (playerBody == null && transform.parent != null)
            playerBody = transform.parent;

        LockCursor();

        playerCamera = GetComponent<Camera>();

        if (playerCamera != null)
            playerCamera.fieldOfView = normalFOV;
    }

    private void Update()
    {
        // Escape unlocks the cursor.
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            UnlockCursor();
            return;
        }

        // While unlocked, wait for a click before looking again.
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            if (Mouse.current != null &&
                Mouse.current.leftButton.wasPressedThisFrame)
            {
                LockCursor();
            }

            return;
        }

        if (Mouse.current == null || playerBody == null)
            return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * sensitivity;
        float mouseY = mouseDelta.y * sensitivity;

        if (invertY)
            mouseY = -mouseY;

        // Rotate the player horizontally.
        playerBody.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically.
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // Smooth ADS zoom while holding the right mouse button.
        if (playerCamera != null && Mouse.current != null)
        {
            float targetFOV = Mouse.current.rightButton.isPressed
                ? adsFOV
                : normalFOV;

            playerCamera.fieldOfView = Mathf.Lerp(
                playerCamera.fieldOfView,
                targetFOV,
                adsTransitionSpeed * Time.deltaTime
            );
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        // Restore locking when the game regains focus.
        if (hasFocus && Application.isPlaying)
            LockCursor();
    }
}
