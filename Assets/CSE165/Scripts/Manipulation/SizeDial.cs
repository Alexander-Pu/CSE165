using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSE165
{
    [RequireComponent(typeof(Outline), typeof(AudioSource))]
    public class SizeDial : MonoBehaviour, Selectable
    {
        [SerializeField]
        private ManipulationManager manipulationManager;
        [SerializeField]
        private float resizeSpeed = 1.0f/360.0f;

        private Outline outline;
        private Vector3 dialFaceForward;
        private bool isFocused = false;

        private AudioSource audioSource;

        private GameObject trackedSelectorGameObject;
        private bool trackSelector = false;
        private Vector3 prevSelectorUp;
        private Vector3 currSelectorUp;

        void Start()
        {
            outline = GetComponent<Outline>();
            audioSource = GetComponent<AudioSource>();
            dialFaceForward = transform.up;
        }
        
        void Update()
        {
            if (isFocused)
            {
                outline.enabled = true;
            }
        }

        void FixedUpdate()
        {
            if (trackSelector)
            {
                currSelectorUp = trackedSelectorGameObject.transform.up;
                float angle = Vector3.SignedAngle(prevSelectorUp, currSelectorUp, dialFaceForward);
                transform.RotateAround(transform.position, dialFaceForward, angle);
                prevSelectorUp = currSelectorUp;
                
                float sizeDelta = resizeSpeed * angle;
                manipulationManager.ChangeSize(sizeDelta);

                audioSource.pitch = Mathf.Clamp(audioSource.pitch - sizeDelta, 0.05f, 3);
            }
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
            prevSelectorUp = selectorGameObject.transform.up;
            currSelectorUp = selectorGameObject.transform.up;
            trackSelector = true;
            trackedSelectorGameObject = selectorGameObject;

            audioSource.pitch = 1;
            audioSource.Play();
        }

        public void Deselect()
        {
            trackSelector = false;

            audioSource.Stop();
        }
    }
}
