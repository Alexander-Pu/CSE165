using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeadMovement : MonoBehaviour
{
    [SerializeField]
    float lookSensitivity = 10;
    bool headMovementEnabled = false;

    private void Start() {
        HandleDisabled();
    }

    public void Look(InputAction.CallbackContext context) {
        if (!headMovementEnabled) {
            return;
        }


        Vector2 look = lookSensitivity * Time.deltaTime * context.ReadValue<Vector2>();

        if (look.x != 0) {
            this.transform.Rotate(Vector3.up * look.x, Space.World);
        }

        if (look.y != 0) {
            Vector3 rotationAxis = this.transform.right * -look.y;
            rotationAxis.y = 0;

            this.transform.Rotate(rotationAxis, Space.World);
        }
    }

    public void EnableHeadMovement(InputAction.CallbackContext context) {
        headMovementEnabled = !headMovementEnabled;

        if (headMovementEnabled) {
            HandleEnabled();
        } else {
            HandleDisabled();
        }
    }

    private void HandleEnabled() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void HandleDisabled() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
