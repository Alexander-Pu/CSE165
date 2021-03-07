using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSE165
{
    [RequireComponent(typeof(Outline), typeof(AudioSource))]
    public class GravitySlider : MonoBehaviour, Selectable
    {
        [SerializeField]
        private float lowerBound = 0.1f;
        [SerializeField]
        private float upperBound = 10;
        private float gravity = -9.81f;

        private Outline outline;
        private AudioSource audioSource;
        private bool isFocused = false;

        private GameObject trackedSelectorGameObject;
        private bool trackSelector = false;

        void Start()
        {
            outline = GetComponent<Outline>();
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (isFocused)
            {
                outline.enabled = true;
            }

            if (trackSelector)
            {
                Vector3 selectorPosition = trackedSelectorGameObject.transform.position;
                
                Vector3 selectorLocalPosition = transform.parent.InverseTransformPoint(selectorPosition);

                Vector3 newSliderPosition = new Vector3(Mathf.Clamp(selectorLocalPosition.x, 0, 1), 0, 0);

                transform.localPosition = newSliderPosition;

                SetGravity();
            }
        }

        private void SetGravity()
        {
            float localX = transform.localPosition.x;
            bool lessGravity = localX < 0.5;
            float adjustedT = 1;
            float gravityModifier = 1;
            if (lessGravity)
            {
                adjustedT = localX * 2;
                gravityModifier = Mathf.Lerp(lowerBound, 1, adjustedT);
            } else
            {
                adjustedT = (localX - 0.5f) * 2;
                gravityModifier = Mathf.Lerp(1, upperBound, adjustedT);
            }

            Physics.gravity = new Vector3(0, gravityModifier * gravity, 0);

            audioSource.pitch = 3 - 3 * localX;
        }

        public void Focus()
        {
            isFocused = true;
        }

        public void Unfocus()
        {
            outline.enabled = false;
            isFocused = false;
        }

        public void Select(GameObject selectorGameObject)
        {
            trackSelector = true;
            trackedSelectorGameObject = selectorGameObject;
            audioSource.Play();
        }

        public void Deselect()
        {
            trackSelector = false;
            audioSource.Stop();
        }
    }
}
