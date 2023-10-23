using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

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
        [SerializeField] private float m_MouseSpeed = 10f;
        
        private float rotationX;

        public float m_Gravity = -9.8f;
        public float m_GroundedGravity = -0.05f;
        
        private float initialJumpVelocity;
        private float maxJumpHeight = 3f;
        private float maxJumpTime = 0.75f;
        private bool isJumpPressed;
        private bool isJumping;

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
            
            playerInputAction.CharacterControls.Jump.started += OnJump;
            playerInputAction.CharacterControls.Jump.canceled += OnJump;

            SetupJumpVariables();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void SetupJumpVariables()
        {
            float timeToApex = maxJumpTime / 2;
            m_Gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
            initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
        }

        private void HandleJump()
        {
            if (!isJumping && characterController.isGrounded && isJumpPressed)
            {
                isJumping = true;
                
                currentMovement.y = initialJumpVelocity * 0.5f;
                currentRunMovement.y = initialJumpVelocity * 0.5f;
            }
            else if(!isJumpPressed && isJumping && characterController.isGrounded)
            {
                isJumping = false;
            }
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

        private void OnJump(InputAction.CallbackContext context)
        {
            isJumpPressed = context.ReadValueAsButton();
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
            bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
            float fallMultiplier = 2.0f;
            
            if (characterController.isGrounded)
            {
                currentMovement.y = m_GroundedGravity;
                currentRunMovement.y = m_GroundedGravity;
            }
            else if (isFalling)
            {
                float previousYVelocity = currentMovement.y;
                float newYVelocity = currentMovement.y + (m_Gravity * fallMultiplier * Time.deltaTime);
                float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
                currentMovement.y = nextYVelocity;
                currentRunMovement.y = nextYVelocity;
            }
            else
            {
                float previousYVelocity = currentMovement.y;
                float newYVelocity = currentMovement.y + (m_Gravity * Time.deltaTime);
                float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
                currentMovement.y = nextYVelocity;
                currentRunMovement.y = nextYVelocity;
            }
        }
        
        private void Update()
        {
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
            
            
            
            float mouseX = currentRotation.x * m_MouseSpeed * Time.deltaTime;
            float mouseY = currentRotation.y * m_MouseSpeed * Time.deltaTime;
            
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, m_MinimumX, m_MaximumX);
            
            m_PlayerArm.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
            
            
            
            HandleGravity();
            HandleJump();
            
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