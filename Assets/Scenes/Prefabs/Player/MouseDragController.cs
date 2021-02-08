using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseDragController : MonoBehaviour
{
    bool mouseDown;
    float originalMouseX;
    float originalMouseY;

    CharacterController characterController;
    PlayerInput playerInput;
    [SerializeField]
    float moveSpeed = 10;
    [SerializeField]
    float rotateSpeed = 10000;
    [SerializeField]
    GameObject head;

    private void Start() {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnMouseDown(InputAction.CallbackContext context) {
        switch (context.phase) {
            case InputActionPhase.Started:
                mouseDown = true;
                UpdateMousePositions();
                break;

            case InputActionPhase.Canceled:
                mouseDown = false;
                break;
        }
    }

    private void Update() {
        if (mouseDown) {
            Vector2 pos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());

            float currMouseX = pos.x;
            float currMouseY = pos.y;

            if (currMouseX != originalMouseX) {
                HandleXOffset(currMouseX - originalMouseX);
            }

            if (currMouseY != originalMouseY) {
                HandleYOffset(currMouseY - originalMouseY);
            }
        }
    }

    private void UpdateMousePositions() {
        Vector2 pos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());

        originalMouseX = pos.x;
        originalMouseY = pos.y;
    }

    private void HandleXOffset(float diff) {
        float rotation = 1000 * Time.deltaTime * rotateSpeed * diff;
        transform.Rotate(0, rotation, 0);
    }

    private void HandleYOffset(float diff) {
        Vector3 headForward = head.transform.forward;
        headForward.y = 0;
        Vector3 moveDir = Time.deltaTime * moveSpeed * headForward * diff;
        characterController.Move(moveDir);
    }

    public void SwitchActionMap(InputAction.CallbackContext context) {
        if (context.performed) {
            playerInput.SwitchCurrentActionMap("FlyMove");
        }
    }
}
