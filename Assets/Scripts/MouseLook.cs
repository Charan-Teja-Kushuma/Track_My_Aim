
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

    private float pitch;

    private void Start()
    {
        if (playerBody == null && transform.parent != null)
            playerBody = transform.parent;

        LockCursor();
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
