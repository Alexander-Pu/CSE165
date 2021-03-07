using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSE165
{
    public class ColliderFocuser : MonoBehaviour
    {
        public GameObject focusedGameObject { get; private set; }
        public Selectable focusedGameObjectSelectable { get; private set; }

        private int layerMask = 1 << 8;

        void Update()
        {
            if (focusedGameObject && !focusedGameObject.activeInHierarchy)
            {
                RefocusCurrentObject();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (!enabled)
            {
                return;
            }

            FocusNewObject(other.gameObject);
        }

        private void FocusNewObject(GameObject newGameObject)
        {
            Selectable newSelectable = newGameObject.GetComponent<Selectable>();
            if (null == newSelectable)
            {
                return;
            }

            // Unfocus current selectable if possible
            if (focusedGameObject)
            {
                focusedGameObjectSelectable.Unfocus();
            }

            // Focus new selectable
            focusedGameObject = newGameObject;
            focusedGameObjectSelectable = newSelectable;
            focusedGameObjectSelectable.Focus();
        }

        void OnTriggerExit(Collider other)
        {
            if (!focusedGameObject)
            {
                return;
            }

            if (focusedGameObject != other.gameObject)
            {
                return;
            }

            RefocusCurrentObject();
        }

        private void RefocusCurrentObject()
        {
            // Handle exiting currently focused object
            GameObject currentFocusedGameObject = focusedGameObject;
            if (null != focusedGameObjectSelectable)
            {
                focusedGameObjectSelectable.Unfocus();
            }

            focusedGameObject = null;
            focusedGameObjectSelectable = null;

            // Try to get a new focusable object if there is one nearby
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.049f, layerMask);
            foreach (var hitCollider in hitColliders)
            {
                // Skip any attempts to focus the previously focused object
                GameObject hitColliderGameObject = hitCollider.gameObject;
                if (currentFocusedGameObject != hitColliderGameObject)
                {
                    FocusNewObject(hitColliderGameObject);
                    if (focusedGameObject)
                    {
                        return;
                    }
                }
            }
        }

        public void SetActive(bool active)
        {
            enabled = active;
            if (!enabled && focusedGameObject)
            {
                focusedGameObjectSelectable.Unfocus();
                focusedGameObject = null;
                focusedGameObjectSelectable = null;
            } else if (enabled)
            {
                RefocusCurrentObject();
            }
        }
    }
}
