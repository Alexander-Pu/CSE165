using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScaler : MonoBehaviour
{
    [SerializeField]
    private Transform manipulatedTransform;
    [SerializeField]
    private ControllerState controllerState;

    private float timeHeld = 0;
    private bool newBaseDistance = true;
    private float baseDistance;
    private Vector3 baseScale;

    // Update is called once per frame
    void Update()
    {
        bool leftHeld = controllerState.leftIndexHeld;
        bool rightHeld = controllerState.rightIndexHeld;

        bool leftUp = controllerState.leftIndexUp;
        bool rightUp = controllerState.rightIndexUp;

        if (leftHeld && rightHeld)
        {
            HandleHeld();
            timeHeld += Time.deltaTime;
        } else if (leftUp || rightUp)
        {
            HandleReleased();
            timeHeld = 0;
            newBaseDistance = true;
        }
    }

    private void HandleHeld()
    {
        if (newBaseDistance)
        {
            baseDistance = Vector3.Distance(controllerState.leftPrevWorldPosition, controllerState.rightPrevWorldPosition);
            baseScale = manipulatedTransform.localScale;
            newBaseDistance = false;
        }
        float currDistance = Vector3.Distance(controllerState.leftWorldPosition, controllerState.rightWorldPosition);

        Vector3 newScale = currDistance / baseDistance * baseScale;

        manipulatedTransform.localScale = newScale;
    }

    private void HandleReleased()
    {
        if (0 < timeHeld && timeHeld < 0.5)
        {
            manipulatedTransform.localScale = new Vector3(1, 1, 1);
        }
    }
}
