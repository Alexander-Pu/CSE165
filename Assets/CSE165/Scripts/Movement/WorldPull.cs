using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPull : MonoBehaviour
{
    [SerializeField]
    private Transform pulledTransform;
    [SerializeField]
    private Transform leftAnchor;
    [SerializeField]
    private Transform rightAnchor;
    [SerializeField]
    private float translateMultiplier = 5;

    private Vector3 prevAnchorPosition;
    private Transform referenceAnchor = null;
    private float prevLeftVal = 0;
    private float prevRightVal = 0;

    // Update is called once per frame
    void Update()
    {
        float leftVal = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        float rightVal = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        // Nothing to do if neither grips are active
        // OR
        // Clear values if either of the grips are released
        // This makes it stutter for a frame, but it's not really noticable
        if ((0 == leftVal && 0 == rightVal) || 
            (IsNewRelease(prevLeftVal, leftVal) || IsNewRelease(prevRightVal, rightVal)))
        {
            UpdateValues(0, 0);
            return;
        }

        // Determine if a new press has been encountered
        bool rightNew = IsNewPress(prevRightVal, rightVal);
        bool leftNew = IsNewPress(prevLeftVal, leftVal);

        // Right hand is the dominant hand, let right hand resolve ties
        if (rightNew)
        {
            referenceAnchor = rightAnchor;
            UpdateValues(leftVal, rightVal);
            return;
        } else if (leftNew)
        {
            referenceAnchor = leftAnchor;
            UpdateValues(leftVal, rightVal);
            return;
        }

        // At least one hand is active, and neither are new
        Vector3 anchorCurrentPosition = referenceAnchor.position;
        Vector3 translateVector = translateMultiplier * (prevAnchorPosition - anchorCurrentPosition);

        pulledTransform.Translate(translateVector);

        UpdateValues(leftVal, rightVal);
    }

    private void UpdateValues(float leftVal, float rightVal)
    {
        prevLeftVal = leftVal;
        prevRightVal = rightVal;
        if (null != referenceAnchor)
        {
            prevAnchorPosition = referenceAnchor.position;
        }
    }

    private bool IsNewPress(float prevVal, float newVal)
    {
        return 0 == prevVal && 0 != newVal;
    }

    private bool IsNewRelease(float prevVal, float newVal)
    {
        return 0 != prevVal && 0 == newVal;
    }
}
