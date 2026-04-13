using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class SimpleFPS : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 0.5f;
    public Transform cameraTransform;

    float yRotation;
    float verticalVelocity;
    public float gravity = -9.8f;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null)
            cameraTransform = GetComponentInChildren<Camera>().transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Look();
        Move();
    }

    void Look()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * mouseSensitivity;
        float mouseY = mouseDelta.y * mouseSensitivity;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Move()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) input.y += 1;
        if (Keyboard.current.sKey.isPressed) input.y -= 1;
        if (Keyboard.current.aKey.isPressed) input.x -= 1;
        if (Keyboard.current.dKey.isPressed) input.x += 1;

        Vector3 move = transform.right * input.x + transform.forward * input.y;

        // Gravity
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;

        float delta = Time.unscaledDeltaTime;
        controller.Move(move * moveSpeed * delta);
    }
}