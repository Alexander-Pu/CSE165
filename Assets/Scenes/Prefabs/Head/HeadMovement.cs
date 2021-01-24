using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeadMovement : MonoBehaviour
{
    [SerializeField]
    float lookSensitivity = 10;
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Look(InputAction.CallbackContext context) {
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
}
