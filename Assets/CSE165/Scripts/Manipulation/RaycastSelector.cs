using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSelector : MonoBehaviour
{
    [SerializeField]
    private int selectableLayer = 8;
    private int layerMask;

    private GameObject selectedObject;

    public bool isEnabled { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << selectableLayer;
        isEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnabled)
        {
            return;
        }

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 50, layerMask))
        {
            GameObject newGameObject = hit.collider.gameObject;
            if (null != selectedObject && selectedObject != newGameObject)
            {
                DisableOutline();
            }

            selectedObject = newGameObject;
            EnableOutline();
        }
        else
        {
            DisableOutline();
            selectedObject = null;
        }
    }

    public GameObject GetSelectedObject()
    {
        return selectedObject;
    }

    public void ClearSelections()
    {
        DisableOutline();
        selectedObject = null;
    }

    private void EnableOutline()
    {
        Outline outline = selectedObject.GetComponent<Outline>();
        if (null != outline)
        {
            float width = outline.OutlineWidth;
            if (width == 0)
            {
                outline.OutlineWidth = 10;
            }
            outline.enabled = true;
        }
    }

    private void DisableOutline()
    {
        if (selectedObject)
        {
            Outline outline = selectedObject.GetComponent<Outline>();
            if (null != outline)
            {
                outline.enabled = false;
            }
        }
    }
}
