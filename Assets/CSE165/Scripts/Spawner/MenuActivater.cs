using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSE165
{
    public class MenuActivater : MonoBehaviour
    {
        [SerializeField]
        private GameObject menu;
        [SerializeField]
        private Transform lookAtTarget;
        [SerializeField]
        private float activeDegreeCutoff = 60;

        void Start()
        {
            menu.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 menuForward = -menu.transform.forward;
            Vector3 lookAtVector = lookAtTarget.position - menu.transform.position;
            float angle = Vector3.Angle(menuForward, lookAtVector);

            bool angleLessThanActiveDegreeCutoff = angle < activeDegreeCutoff;
            menu.SetActive(angleLessThanActiveDegreeCutoff);
        }
    }
}
