using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSE165
{
    [RequireComponent(typeof(ColliderFocuser))]
    public class Selector : MonoBehaviour
    {
        private ColliderFocuser colliderFocuser;

        [SerializeField]
        private ControllerState controllerState;
        [SerializeField]
        private ManipulationManager manipulationManager;

        private GameObject selectedGameObject;
        private Selectable selectedGameObjectSelectable;

        void Start()
        {
            colliderFocuser = GetComponent<ColliderFocuser>();
        }

        // Update is called once per frame
        void Update()
        {
            bool indexDown = controllerState.rightIndexDown;
            bool indexUp = controllerState.rightIndexUp;

            if (indexDown)
            {
                Select(colliderFocuser.focusedGameObject);
            } else if (indexUp)
            {
                Deselect();
            }
        }

        public void Select(GameObject newSelectedGameObject)
        {
            manipulationManager.SelectGameObject(newSelectedGameObject);

            if (!newSelectedGameObject)
            {
                return;
            }

            // Disable collider selector while something is selected
            colliderFocuser.SetActive(false);

            selectedGameObject = newSelectedGameObject;
            selectedGameObjectSelectable = newSelectedGameObject.GetComponent<Selectable>();
            selectedGameObjectSelectable.Select(gameObject);
        }

        private void Deselect()
        {
            // Nothing currently selected, can return early
            if (!selectedGameObject)
            {
                return;
            }

            if (null != selectedGameObjectSelectable)
            {
                selectedGameObjectSelectable.Deselect();
            }
            selectedGameObjectSelectable = null;
            selectedGameObject = null;

            // Re-enable collider selector
            colliderFocuser.SetActive(true);
        }
    }
}
