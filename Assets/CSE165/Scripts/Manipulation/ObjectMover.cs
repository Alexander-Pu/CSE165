using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField]
    private Transform world;
    [SerializeField]
    private GameObject raycastSelectorObject;
    private RaycastSelector raycastSelector;
    [SerializeField]
    private GameObject colliderSelectorObject;
    private ColliderSelector colliderSelector;
    private bool useRaycastSelector = true;
    private float rightVal = 0;
    private GameObject heldObject = null;

    [SerializeField]
    private float gogoCutOff = .4f;
    [SerializeField]
    private float gogoRate = 15f;
    [SerializeField]
    private Transform realHand;
    [SerializeField]
    private Transform gogoDistanceReference;

    void Start()
    {
        raycastSelector = raycastSelectorObject.GetComponent<RaycastSelector>();
        colliderSelector = colliderSelectorObject.GetComponent<ColliderSelector>();
        colliderSelectorObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GogoUpdate();

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

    private void GogoUpdate()
    {
        if (useRaycastSelector)
        {
            transform.localPosition = new Vector3(0, 0, 0);
            return;
        }

        Vector3 currPosition = realHand.transform.position;
        Vector3 refPosition = gogoDistanceReference.position;

        float distance = Vector3.Distance(currPosition, refPosition);

        if (distance > gogoCutOff)
        {
            float offset = distance - gogoCutOff;
            float desiredOffset = gogoRate * Mathf.Pow(offset, 2);
            transform.localPosition = new Vector3(0, 0, desiredOffset);
        }
        else
        {
            transform.localPosition = new Vector3(0, 0, 0);
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

            heldObject.transform.SetParent(world);
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
