using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSE165
{
    [RequireComponent(typeof(MeshRenderer), typeof(Outline), typeof(AudioSource))]
    public class MaterialChanger : MonoBehaviour, Selectable
    {
        [SerializeField]
        private ManipulationManager manipulationManager;
        [SerializeField]
        private float pressDisplacement = 0.01f;

        private Material material;
        private Outline outline;
        private AudioSource audioSource;
        private Vector3 originalLocalPosition;
        private Vector3 displacement;
        private bool isFocused = false;

        void Awake()
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            material = meshRenderer.material;
            outline = GetComponent<Outline>();
            audioSource = GetComponent<AudioSource>();
            originalLocalPosition = transform.localPosition;
            displacement = new Vector3(0, pressDisplacement);
        }
        
        void Update()
        {
            if (isFocused)
            {
                outline.enabled = true;
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
            manipulationManager.SetMaterial(material);
            transform.localPosition = originalLocalPosition - displacement;
            audioSource.Play();
        }

        public void Deselect()
        {
            transform.localPosition = originalLocalPosition;
        }
    }
}