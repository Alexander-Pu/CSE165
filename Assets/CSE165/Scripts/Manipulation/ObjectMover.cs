using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField]
    private GameObject raycastSelectorObject;
    private RaycastSelector raycastSelector;
    [SerializeField]
    private GameObject colliderSelectorObject;
    private ColliderSelector colliderSelector;
    private bool useRaycastSelector = true;
    private float rightVal = 0;
    private GameObject heldObject = null;

    void Start()
    {
        raycastSelector = raycastSelectorObject.GetComponent<RaycastSelector>();
        colliderSelector = colliderSelectorObject.GetComponent<ColliderSelector>();
        colliderSelectorObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float prevVal = rightVal;
        rightVal = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
        bool grabNew = 0 == prevVal && 0 != rightVal;
        bool release = 0 != prevVal && 0 == rightVal;

        bool xPress = OVRInput.GetDown(OVRInput.Button.Three);
        if (xPress)
        {
            SwitchSelector();
            return;
        }

        if (grabNew)
        {
            GameObject selectedObject;
            if (useRaycastSelector)
            {
                selectedObject = raycastSelector.GetSelectedObject();
            } else
            {
                selectedObject = colliderSelector.GetSelectedObject();
            }
            if (selectedObject)
            {
                GrabObject(selectedObject);
            }
            return;
        } 

        if (release)
        {
            LetGoObject();
        }
    }

    private void GrabObject(GameObject newHeldObject)
    {
        heldObject = newHeldObject;

        Rigidbody rigid = heldObject.GetComponent<Rigidbody>();
        if (rigid)
        {
            rigid.isKinematic = true;
        }

        heldObject.transform.SetParent(transform);
        raycastSelector.isEnabled = false;
        colliderSelector.isEnabled = false;
    }

    private void LetGoObject()
    {
        if (heldObject)
        {
            Rigidbody rigid = heldObject.GetComponent<Rigidbody>();
            if (rigid)
            {
                rigid.isKinematic = false;
            }

            heldObject.transform.SetParent(null);
            heldObject = null;
        }

        raycastSelector.isEnabled = true;
        colliderSelector.isEnabled = true;
    }

    private void SwitchSelector()
    {
        useRaycastSelector = !useRaycastSelector;
        raycastSelector.ClearSelections();
        colliderSelector.ClearSelections();
        raycastSelectorObject.SetActive(useRaycastSelector);
        colliderSelectorObject.SetActive(!useRaycastSelector);
    }
}
