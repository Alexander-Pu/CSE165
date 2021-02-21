using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSelector : MonoBehaviour
{
    [SerializeField]
    private int selectableLayer = 8;

    private GameObject selectedObject;

    public bool isEnabled { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        isEnabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isEnabled)
        {
            return;
        }

        if (selectableLayer != other.gameObject.layer)
        {
            return;
        }

        GameObject newGameObject = other.gameObject;
        if (null != selectedObject && selectedObject != newGameObject)
        {
            DisableOutline();
        }

        selectedObject = newGameObject;
        EnableOutline();
    }

    void OnTriggerExit(Collider other)
    {
        if (!isEnabled)
        {
            return;
        }
        DisableOutline();
        selectedObject = null;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.04f);
        foreach (var hitCollider in hitColliders)
        {
            OnTriggerEnter(hitCollider);
            return;
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
