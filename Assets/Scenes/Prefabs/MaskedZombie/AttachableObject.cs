using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableObject : MonoBehaviour
{
    [SerializeField]
    Vector3 offset;
    bool display;

    Transform parentTransform;
    SkinnedMeshRenderer skinnedMeshRenderer;
    List<Renderer> childRenderers;


    private void Start() {
        display = false;
        parentTransform = this.transform.parent;
        skinnedMeshRenderer = this.transform.GetComponent<SkinnedMeshRenderer>();
        childRenderers = new List<Renderer>();

        Transform[] childTransforms = GetComponentsInChildren<Transform>();
        foreach (Transform child in childTransforms) {
            if (this.transform != child) {
                Renderer[] realChildRenderers = child.GetComponentsInChildren<Renderer>();
                foreach (Renderer realChildRenderer in realChildRenderers) {
                    childRenderers.Add(realChildRenderer);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Bind the x to parent transform, because the mesh is shaking
        Vector3 newPos = skinnedMeshRenderer.bounds.center;
        newPos.x = parentTransform.position.x;
        newPos = newPos + offset;

        this.transform.position = newPos;

        // Hide or display children
        foreach (Renderer childRenderer in childRenderers) {
            childRenderer.enabled = this.display;
        }
    }

    public void SetDisplay(bool newDisplay) {
        this.display = newDisplay;
    }
}
