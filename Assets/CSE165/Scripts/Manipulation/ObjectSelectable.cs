using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSE165
{
    [RequireComponent(typeof(Outline), typeof(Rigidbody))]
    public class ObjectSelectable : MonoBehaviour, Selectable
    {
        private Outline outline;
        private Rigidbody rigidBody;
        private int deletionLayerMask = 1 << 4;

        private bool isFocused = false;
        private bool isSelected = false;

        private Vector3 prevPosition;
        private Vector3 currPosition;

        void Awake()
        {
            outline = GetComponent<Outline>();
            rigidBody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isFocused)
            {
                outline.enabled = true;
                outline.OutlineColor = Color.cyan;
            }
        }

        void FixedUpdate()
        {
            if (isSelected)
            {
                prevPosition = currPosition;
                currPosition = transform.position;
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
            transform.SetParent(selectorGameObject.transform);
            rigidBody.isKinematic = true;
            isSelected = true;
            prevPosition = transform.position;
            currPosition = transform.position;
        }

        public void Deselect()
        {
            transform.SetParent(null);
            rigidBody.isKinematic = false;
            rigidBody.velocity = (currPosition - prevPosition) / Time.fixedDeltaTime;
            isSelected = false;

            // Check deletion cube
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.05f, deletionLayerMask);
            foreach (var hitCollider in hitColliders)
            {
                AudioSource audioSource = hitCollider.gameObject.GetComponent<AudioSource>();
                if (audioSource)
                {
                    audioSource.Play();
                }
                Destroy(gameObject);
            }
        }
    }
}