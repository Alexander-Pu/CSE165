using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlyController : MonoBehaviour
{
    [SerializeField]
    GameObject head;
    [SerializeField]
    float moveSpeed = 10;
    CharacterController characterController;
    PlayerInput playerInput;

    float forwardSpeed = 0;
    float horizontalSpeed = 0;

    private void Start() {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnFly(InputAction.CallbackContext context) {
        Vector2 moveDir = context.ReadValue<Vector2>();

        HandleFly(moveDir.normalized);
    }

    private void HandleFly(Vector2 moveDir) {
        float baseMoveSpeed = Time.deltaTime * moveSpeed;
        forwardSpeed = baseMoveSpeed * moveDir.y;
        horizontalSpeed = baseMoveSpeed * moveDir.x;
    }
    public void SwitchActionMap(InputAction.CallbackContext context) {
        if (context.performed) {
            playerInput.SwitchCurrentActionMap("MouseDrag");
        }
    }

    private void Update() {
        if (forwardSpeed != 0 || horizontalSpeed != 0) {
            Vector3 forwardVelocity = head.transform.forward * forwardSpeed;
            Vector3 horizontalVelocity = head.transform.right * horizontalSpeed;
            characterController.Move(forwardVelocity + horizontalVelocity);
        }
    }
}
