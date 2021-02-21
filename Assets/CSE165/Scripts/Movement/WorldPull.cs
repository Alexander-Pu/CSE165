using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPull : MonoBehaviour
{
    [SerializeField]
    private Transform manipulatedTransform;
    [SerializeField]
    private Transform leftAnchor;
    [SerializeField]
    private Transform rightAnchor;
    [SerializeField]
    private float translateMultiplier = 5;
    [SerializeField]
    private float rotationMuliplier = 1;
    
    private Vector3 prevLeftWorld;
    private Vector3 prevRightWorld;
    private Vector3 prevLeftLocal;
    private Vector3 prevRightLocal;
    private float leftVal = 0;
    private float rightVal = 0;
    private bool canTranslate = false;

    void Start()
    {
        UpdatePositions();
    }

    // Update is called once per frame
    void Update()
    {
        leftVal = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        rightVal = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        if (0 == leftVal && 0 == rightVal) {
            // Reset. Let translate happen
            canTranslate = true;
        } else if (canTranslate && (0 == leftVal || 0 == rightVal))
        {
            // Handle single hand grab case
            if (0 != rightVal)
            {
                HandleTranslation(rightAnchor, prevRightWorld);
            } else
            {
                HandleTranslation(leftAnchor, prevLeftWorld);
            }
            
        } else if (0 != leftVal && 0 != rightVal)
        {
            // Handle both hands grab case
            HandleRotation();
            canTranslate = false;
        }

        UpdatePositions();
    }

    private void UpdatePositions()
    {
        prevLeftWorld = leftAnchor.position;
        prevRightWorld = rightAnchor.position;
        prevLeftLocal = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        prevRightLocal = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
    }

    private void HandleTranslation(Transform anchorToUse, Vector3 prevPosition)
    {
        Vector3 currentAnchorPosition = anchorToUse.position;
        Vector3 translateVector = translateMultiplier * (prevPosition - currentAnchorPosition);
        translateVector.y = 0;

        manipulatedTransform.Translate(translateVector, Space.World);
    }

    private void HandleRotation()
    {
        Vector3 rightHandLocal = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        float angleRight = Vector3.SignedAngle(rightHandLocal, prevRightLocal, Vector3.up);

        float rotationAngle = angleRight * rotationMuliplier * Time.deltaTime;

        manipulatedTransform.Rotate(new Vector3(0, rotationAngle, 0));
    }
}
