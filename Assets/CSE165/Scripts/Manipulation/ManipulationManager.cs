using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSE165
{
    public class ManipulationManager : MonoBehaviour
    {
        private GameObject selectedGameObject;
        private Outline selectedGameObjectOutline;

        // Update is called once per frame
        void Update()
        {
            if(selectedGameObjectOutline)
            {
                selectedGameObjectOutline.enabled = true;
                selectedGameObjectOutline.OutlineColor = Color.red;
            }
        }

        public void SelectGameObject(GameObject newGameObject)
        {
            // Only react to null or ObjectSelectable game objects
            if (!newGameObject || newGameObject.GetComponent<ObjectSelectable>())
            {
                UnselectGameObject();

                selectedGameObject = newGameObject;
                selectedGameObjectOutline = selectedGameObject.GetComponent<Outline>();
            }
        }

        private void UnselectGameObject()
        {
            if (selectedGameObjectOutline)
            {
                selectedGameObjectOutline.enabled = false;
            }
            selectedGameObject = null;
            selectedGameObjectOutline = null;
        }

        public void SetMaterial(Material material)
        {
            if (!selectedGameObject)
            {
                return;
            }

            MeshRenderer meshRenderer = selectedGameObject.GetComponent<MeshRenderer>();
            if (meshRenderer)
            {
                meshRenderer.material = material;
            }
        }

        public void ChangeSize(float sizeDelta)
        { 
            if (!selectedGameObject)
            {
                return;
            }

            float scaleValue = 1 + sizeDelta;
            Vector3 currentScale = selectedGameObject.transform.localScale;
            Vector3 newScale = Vector3.Scale(currentScale, new Vector3(scaleValue, scaleValue, scaleValue));
            selectedGameObject.transform.localScale = newScale;
        }
    }
}
