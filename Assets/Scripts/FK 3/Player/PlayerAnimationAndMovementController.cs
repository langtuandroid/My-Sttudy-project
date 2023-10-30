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
        private Vector3 applyMovement;
        private Vector2 currentRotation;
        private bool isMovementPressed;
        private bool isRunPressed;

        private CharacterController characterController;
        private Animator animatorController;

        readonly float walkMultiplier = 3f;
        readonly float runMultiplier = 5f;
        
        private static readonly int IsIdle = Animator.StringToHash("isIdle");
        
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
        public float m_MaxJumpHeight = 3f;
        public float m_MaxJumpTime = 0.75f;
        private bool isJumpPressed;
        private bool isJumping;
        private static readonly int IsJumpUp = Animator.StringToHash("isJumpUp");
        private static readonly int IsJumpFall = Animator.StringToHash("isJumpFall");
        private static readonly int IsJumpLand = Animator.StringToHash("isJumpLand");
        private bool isJumpingAnimating;

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
            animatorController.SetBool(IsIdle, true);
        }

        private void SetupJumpVariables()
        {
            float timeToApex = m_MaxJumpTime / 2;
            m_Gravity = (-2 * m_MaxJumpHeight) / Mathf.Pow(timeToApex, 2);
            initialJumpVelocity = (2 * m_MaxJumpHeight) / timeToApex;
        }

        private void HandleJump()
        {
            if (!isJumping && characterController.isGrounded && isJumpPressed)
            {
                animatorController.SetBool(IsJumpLand, false);
                animatorController.SetBool(IsJumpUp, true);
                isJumpingAnimating = true;
                    
                isJumping = true;
                
                currentMovement.y = initialJumpVelocity;
                applyMovement.y = initialJumpVelocity;
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
                
                animatorController.SetBool(IsIdle, false);
            }
            if (!isMovementPressed && isWalk)
            {
                animatorController.SetBool(IsIdle, true);
                
                animatorController.SetBool(IsWalk, false);
            }
        }

        private void HandleGravity()
        {
            bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
            float fallMultiplier = 2.0f;
            
            if (characterController.isGrounded)
            {
                if (isJumpingAnimating)
                {
                    animatorController.SetBool(IsJumpFall, false);
                    animatorController.SetBool(IsJumpLand, true);
                    isJumpingAnimating = false;
                }

                currentMovement.y = m_GroundedGravity;
                applyMovement.y = m_GroundedGravity;
            }
            else if (isFalling)
            {
                if (isJumpingAnimating)
                {
                    animatorController.SetBool(IsJumpUp, false);
                    animatorController.SetBool(IsJumpFall, true);
                }

                float previousYVelocity = currentMovement.y;
                currentMovement.y +=  m_Gravity * fallMultiplier * Time.deltaTime;
                applyMovement.y = Mathf.Max((previousYVelocity + currentMovement.y) * 0.5f, -20.0f);
            }
            else
            {
                float previousYVelocity = currentMovement.y;
                currentMovement.y += m_Gravity * Time.deltaTime;
                applyMovement.y = (previousYVelocity + currentMovement.y) * 0.5f;
            }
        }
        
        private void Update()
        {
            if (characterController.isGrounded)
            {
                Debug.Log("Player is Grounded..............");
            }

            HandleAnimation();
            
            trans = transform;
            Vector3 move;
            Vector3 gravityMove;
            
            if (isRunPressed)
            {
                applyMovement = currentRunMovement;
                
                move = trans.right * applyMovement.x + trans.forward * applyMovement.z;
                gravityMove = trans.up * applyMovement.y;
            }
            else
            { 
                applyMovement= currentMovement;
                
                move = trans.right * applyMovement.x + trans.forward * applyMovement.z;
                gravityMove = trans.up * applyMovement.y;
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