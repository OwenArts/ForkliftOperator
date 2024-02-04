using System;
using UnityEngine;

namespace Player.Movement
{
    public class MouseManager : MonoBehaviour
    {
        public Transform playerTransform; // Reference to the player's transform
        public float mouseSensitivity = 2.0f; // Adjust this value to control mouse sensitivity
        public float smoothing = 2.0f; // Adjust this value to control camera smoothing

        private Vector2 smoothedVelocity;
        private Vector2 currentLookingPos;

        private void Start()
        {
            transform.Rotate(Vector3.left, 0f);
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            // CheckMouseMovement();
            
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            // Smooth the mouse movement using Lerp
            smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, mouseDelta.x, 1f / smoothing);
            smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, mouseDelta.y, 1f / smoothing);

            currentLookingPos += smoothedVelocity * mouseSensitivity;

            // Limit vertical camera rotation to avoid flipping
            currentLookingPos.y = Mathf.Clamp(currentLookingPos.y, -90, 90);

            // Rotate the player's transform based on horizontal mouse movement
            // playerTransform.localRotation = Quaternion.Euler(0, currentLookingPos.x, 0);
            

            // Rotate the camera based on vertical mouse movement
            // transform.localRotation = Quaternion.Euler(-currentLookingPos.y, 0, 0);
         
            transform.localRotation = Quaternion.Euler(-currentLookingPos.y, currentLookingPos.x, 0);
        }
    }
}