using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerState : MonoBehaviour
{
    [SerializeField]
    private Transform leftControllerTransform;
    [SerializeField]
    private Transform rightControllerTransform;

    // Left hand
    public Vector3 leftPrevWorldPosition { get; private set; }
    public Vector3 leftWorldPosition { get; private set; }
    public Vector3 leftPrevLocalPosition { get; private set; }
    public Vector3 leftLocalPosition { get; private set; }
    public float leftIndex { get; private set; }
    public bool leftIndexDown { get; private set; }
    public bool leftIndexHeld { get; private set; }
    public bool leftIndexUp { get; private set; }
    public float leftGrip { get; private set; }
    public bool leftGripDown { get; private set; }
    public bool leftGripHeld { get; private set; }
    public bool leftGripUp { get; private set; }

    // Right hand
    public Vector3 rightPrevWorldPosition { get; private set; }
    public Vector3 rightWorldPosition { get; private set; }
    public Vector3 rightPrevLocalPosition { get; private set; }
    public Vector3 rightLocalPosition { get; private set; }
    public float rightIndex { get; private set; }
    public bool rightIndexDown { get; private set; }
    public bool rightIndexHeld { get; private set; }
    public bool rightIndexUp { get; private set; }
    public float rightGrip { get; private set; }
    public bool rightGripDown { get; private set; }
    public bool rightGripHeld { get; private set; }
    public bool rightGripUp { get; private set; }

    void Update()
    {
        UpdatePositions();
        UpdateIndex();
        UpdateGrip();
    }

    private void UpdatePositions()
    {
        leftPrevWorldPosition = leftWorldPosition;
        leftPrevLocalPosition = leftLocalPosition;
        leftWorldPosition = leftControllerTransform.position;
        leftLocalPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);

        rightPrevWorldPosition = rightWorldPosition;
        rightPrevLocalPosition = rightLocalPosition;
        rightWorldPosition = rightControllerTransform.position;
        rightLocalPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
    }

    private void UpdateIndex()
    {
        float currLeftIndex = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
        leftIndexDown = 0 == leftIndex && 0 != currLeftIndex;
        leftIndexHeld = 0 != leftIndex && 0 != currLeftIndex;
        leftIndexUp = 0 != leftIndex && 0 == currLeftIndex;
        leftIndex = currLeftIndex;

        float currRightIndex = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
        rightIndexDown = 0 == rightIndex && 0 != currRightIndex;
        rightIndexHeld = 0 != rightIndex && 0 != currRightIndex;
        rightIndexUp = 0 != rightIndex && 0 == currRightIndex;
        rightIndex = currRightIndex;
    }

    private void UpdateGrip()
    {
        float currLeftGrip = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        leftGripDown = 0 == leftGrip && 0 != currLeftGrip;
        leftGripHeld = 0 != leftGrip && 0 != currLeftGrip;
        leftGripUp = 0 != leftGrip && 0 == currLeftGrip;
        leftGrip = currLeftGrip;

        float currRightGrip = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);
        rightGripDown = 0 == rightGrip && 0 != currRightGrip;
        rightGripHeld = 0 != rightGrip && 0 != currRightGrip;
        rightGripUp = 0 != rightGrip && 0 == currRightGrip;
        rightGrip = currRightGrip;
    }
}
