using System.Collections.Generic;
using DefaultNamespace;
using JetBrains.Annotations;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 10.0f;
        public float jumpForce = 5.0f;
        public float horizontalAxis;
        public float verticalAxis;
        [NotNull] public List<GameObject> objects;
        
        private CharacterController m_Controller;
        private Vector3 m_MoveDirection = Vector3.zero;
        private readonly Timeout m_JumpTimeout = new Timeout(.5f);


        void Start()
        {
            m_Controller = GetComponent<CharacterController>();
        }

        void FixedUpdate()
        {
            UpdateMovement();
            UpdateJump();
        }

        private void UpdateJump()
        {
            if (Input.GetKey(KeyCode.Space) && m_JumpTimeout.TimeOutCompleted())
            {
                if (verticalAxis < 0)
                    m_MoveDirection.y = jumpForce;
                else
                    m_MoveDirection.y = CheckRunning(jumpForce);
            }

            m_MoveDirection.y -= 9.81f * Time.deltaTime;

            m_Controller.Move(m_MoveDirection * Time.deltaTime);
        }

        private void UpdateMovement()
        {
            horizontalAxis = CheckRunning(Input.GetAxis("Horizontal"));
            verticalAxis = CheckRunning(Input.GetAxis("Vertical"));

            Vector3 moveDirection = new Vector3(horizontalAxis, 0.0f, verticalAxis);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            m_Controller.SimpleMove(moveDirection);
        }

        private float CheckRunning(float value)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return 1.8f * value;
            return value;
        }
    }
}