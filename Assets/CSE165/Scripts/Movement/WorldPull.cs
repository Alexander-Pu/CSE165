using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPull : MonoBehaviour
{
    [SerializeField]
    private ControllerState controllerState;
    [SerializeField]
    private Transform manipulatedTransform;
    [SerializeField]
    private float translateMultiplier = 1;
    [SerializeField]
    private float rotationMuliplier = 1;

    private bool newRotatePivot = true;
    private Vector3 rotatePivot;

    // Update is called once per frame
    void Update()
    {
        bool leftHeld = controllerState.leftGripHeld;
        bool rightHeld = controllerState.rightGripHeld;

        if (leftHeld != rightHeld)
        {
            // Handle single hand grab case
            if (rightHeld)
            {
                HandleTranslation(controllerState.rightLocalPosition, controllerState.rightPrevLocalPosition);
            }
            else if (leftHeld)
            {
                HandleTranslation(controllerState.leftLocalPosition, controllerState.leftPrevLocalPosition);
            }
            newRotatePivot = true;
        } else if (leftHeld && rightHeld)
        {
            HandleRotation();
            newRotatePivot = false;
        } else
        {
            newRotatePivot = true;
        }
    }

    private void HandleTranslation(Vector3 currPosition, Vector3 prevPosition)
    {
        Vector3 translateVector = translateMultiplier * (prevPosition - currPosition);
        translateVector.y = 0;

        manipulatedTransform.Translate(translateVector);
    }

    private void HandleRotation()
    {
        if (newRotatePivot)
        {
            rotatePivot = (controllerState.leftWorldPosition + controllerState.rightWorldPosition) / 2.0f;
        }

        Vector3 prevVec = controllerState.rightPrevLocalPosition - controllerState.leftPrevLocalPosition;
        Vector3 currVec = controllerState.rightLocalPosition - controllerState.leftLocalPosition;

        float rotationAngle = Vector3.SignedAngle(currVec, prevVec, Vector3.up);

        float rotationSpeed = rotationAngle * rotationMuliplier;
        
        manipulatedTransform.RotateAround(rotatePivot, Vector3.up, rotationSpeed);
    }
}
