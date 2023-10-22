using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FK_3.Player
{
    public class PlayerAnimationAndMovementController : MonoBehaviour
    {
        private PlayerInputAction playerInputAction;
        private Vector2 currentMovementInput;
        private Vector2 currentRotationInput;
        private Vector3 currentMovement;
        private Vector3 currentRunMovement;
        private Vector2 currentRotation;
        private bool isMovementPressed;
        private bool isRunPressed;

        private CharacterController characterController;
        private Animator animatorController;

        float walkMultiplier = 3f;
        float runMultiplier = 5f;
        
        private Transform trans;
        private static readonly int IsWalk = Animator.StringToHash("isWalk");
        
        [SerializeField] private Transform m_PlayerArm;
        [SerializeField] private float m_MinimumX = -90.0f;
        [SerializeField] private float m_MaximumX = 90.0f;

        private float rotationX;

        private void Awake()
        {
            animatorController = GetComponentInChildren<Animator>();
            characterController = GetComponent<CharacterController>();
            
            playerInputAction = new PlayerInputAction();
            playerInputAction.CharacterControls.Move.started += OnMovementInput;
            playerInputAction.CharacterControls.Move.canceled += OnMovementInput;
            playerInputAction.CharacterControls.Move.performed += OnMovementInput;
            
            playerInputAction.CharacterControls.Run.started += OnRun;
            playerInputAction.CharacterControls.Run.canceled += OnRun;

            playerInputAction.CharacterControls.Rotation.started += OnRotationInput;
            playerInputAction.CharacterControls.Rotation.canceled += OnRotationInput;
            playerInputAction.CharacterControls.Rotation.performed += OnRotationInput;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnRun(InputAction.CallbackContext context)
        {
            isRunPressed = context.ReadValueAsButton();
        }

        private void OnMovementInput(InputAction.CallbackContext context)
        {
            currentMovementInput = context.ReadValue<Vector2>();
            currentMovement.x = currentMovementInput.x * walkMultiplier;
            currentMovement.z = currentMovementInput.y * walkMultiplier;
            currentRunMovement.x = currentMovementInput.x * runMultiplier;
            currentRunMovement.z = currentMovementInput.y * runMultiplier;
            isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;    
        }
        
        private void OnRotationInput(InputAction.CallbackContext context)
        {
            currentRotationInput = context.ReadValue<Vector2>();
            currentRotation.x = currentRotationInput.x;
            currentRotation.y = currentRotationInput.y;
        }
        
        private void HandleAnimation()
        {
            bool isWalk = animatorController.GetBool(IsWalk);

            if (isMovementPressed && !isWalk)
            {
                animatorController.SetBool(IsWalk, true);
            }
            if (!isMovementPressed && isWalk)
            {
                animatorController.SetBool(IsWalk, false);
            }
        }

        private void HandleGravity()
        {
            if (characterController.isGrounded)
            {
                float groundedGravity = -0.05f;
                currentMovement.y = groundedGravity;
                currentRunMovement.y = groundedGravity;
            }
            else
            {
                float gravity = -9.8f;
                currentMovement.y += gravity;
                currentRunMovement.y += gravity;
            }
        }
        
        private void Update()
        {
            HandleGravity();
            HandleAnimation();
            
            trans = transform;
            Vector3 move;
            Vector3 gravityMove;
            
            if (isRunPressed)
            {
                move = trans.right * currentRunMovement.x + trans.forward * currentRunMovement.z;
                gravityMove = trans.up * currentRunMovement.y;
            }
            else
            { 
                move = trans.right * currentMovement.x + trans.forward * currentMovement.z;
                gravityMove = trans.up * currentMovement.y;
            }
            
            characterController.Move(move * Time.deltaTime);
            characterController.Move(gravityMove * Time.deltaTime);
            
            
            
            
            float mouseX = currentRotation.x * 200f * Time.deltaTime;
            float mouseY = currentRotation.y * 200f * Time.deltaTime;
            
            transform.Rotate(Vector3.up * mouseX);
            
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, m_MinimumX, m_MaximumX);
            m_PlayerArm.localRotation = Quaternion.Euler(rotationX, 0, 0);
            
            
        }
        
        private void OnEnable()
        {
            playerInputAction.CharacterControls.Enable();
        }

        private void OnDisable()
        {
            playerInputAction.CharacterControls.Disable();
        }
    }
}