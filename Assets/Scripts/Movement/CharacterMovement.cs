using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movement
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1f;

        private CharacterController _characterController;
        public bool isGrounded;
        public GameObject plauyer;
        public VariableJoystick variableJoystick;
        private Transform startPos;

        void Awake() => _characterController = GetComponent<CharacterController>();

        private void Start()
        {
            plauyer = gameObject;
            startPos = transform;
        }

        private void FixedUpdate()
        {
            Vector3 JoyDirection = -Vector3.right * variableJoystick.Vertical +
                                   Vector3.forward * variableJoystick.Horizontal;
            isGrounded = _characterController.SimpleMove(JoyDirection * moveSpeed);

        }
    }
}
