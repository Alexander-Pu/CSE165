using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CSE165
{
    [RequireComponent(typeof(Image), typeof(AudioSource))]
    public class ObjectSpawner : MonoBehaviour, Selectable
    {
        [SerializeField]
        private GameObject objectPrefab;

        private Image image;
        [SerializeField]
        private AudioClip focusSound;
        [SerializeField]
        private AudioClip selectSound;
        private AudioSource audioSource;

        void Awake()
        {
            image = GetComponent<Image>();
            audioSource = GetComponent<AudioSource>();
        }

        public void Focus()
        {
            image.color = Color.white;
            audioSource.PlayOneShot(focusSound);
        }

        public void Unfocus()
        {
            image.color = Color.gray;
        }

        public void Select(GameObject selectorGameObject)
        {
            GameObject newObject = Instantiate(objectPrefab, selectorGameObject.transform.position, selectorGameObject.transform.rotation);
            
            // Object Spawner is special such that when it gets selected, it
            // forces the selector to select the new object
            Selector selector = selectorGameObject.GetComponent<Selector>();
            if (selector)
            {
                selector.Select(newObject);
            }

            audioSource.PlayOneShot(selectSound);
        }

        public void Deselect()
        {
        }
    }
}
