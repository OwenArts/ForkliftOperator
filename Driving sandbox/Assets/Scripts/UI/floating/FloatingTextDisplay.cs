using System;
using UnityEngine;

namespace UI.floating
{
    public class FloatingTextDisplay : MonoBehaviour
    {
        [SerializeField]
        private Transform mainCam;
        [SerializeField]
        private GameObject target;
        [SerializeField]
        private Transform worldSpaceCanvas;
        [SerializeField]
        private Vector3 positionOffset;
        [SerializeField]
        private Quaternion rotationOffset;
        [SerializeField]
        private float triggerDistance;
        [SerializeField]
        private bool m_isVisible = false;

        /*------------------------------------------*/
        [SerializeField]
        private GameObject textObject; // Reference to the text GameObject

        private void Start()

        {
            mainCam = Camera.main!.transform;
            textObject.transform.SetParent(worldSpaceCanvas);
            /*------------------------------------------*/
            textObject.SetActive(false);
        }

        void Update()
        {
            if (m_isVisible)
            {
                textObject.transform.rotation =
                    Quaternion.LookRotation(textObject.transform.position - mainCam.transform.position);
                textObject.transform.position = transform.position + positionOffset;

                //make system to check if key has been down for a while, has to reset on key going up.
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) // Change the tag as needed
            {
                // Activate the text object to make it visible
                textObject.SetActive(true);
                m_isVisible = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) // Change the tag as needed
            {
                // Deactivate the text object to make it invisible
                textObject.SetActive(false);
                m_isVisible = false;
                Debug.Log("Hiding text!");
            }
        }
    }
}