using UnityEngine;
using UnityEngine.InputSystem;

namespace FK_3.Player.StateMachine
{
    public class PlayerStateMachine : MonoBehaviour
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

        public CharacterController m_CharacterController;
        private Animator animatorController;

        readonly float walkMultiplier = 3f;
        readonly float runMultiplier = 5f;
        
        private Transform trans;
        private readonly int isWalk = Animator.StringToHash("isWalk");
        
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
        private readonly int isJumpUp = Animator.StringToHash("isJumpUp");
        private readonly int isJumpFall = Animator.StringToHash("isJumpFall");
        private readonly int isJumpLand = Animator.StringToHash("isJumpLand");
        private bool requireNewJumpPress;


        private PlayerBaseState currentState;
        private PlayerStateFactory states;

        public PlayerBaseState CurrentState { get { return currentState; } set { currentState = value; } }
        public Animator AnimatorController { get { return animatorController; } }
        public float InitialJumpVelocity { get { return initialJumpVelocity; } }
        public bool RequireNewJumpPress { get { return requireNewJumpPress;} set { requireNewJumpPress = value; } }
        public bool IsJumpPressed { get { return isJumpPressed; } set { isJumpPressed = value; } }
        public float CurrentMovementY { get { return currentMovement.y; } set { currentMovement.y = value; } }
        public float AppliedMovementY { get { return applyMovement.y; } set { applyMovement.y = value; } }
        public  int IsJumpUp { get { return isJumpUp; } }
        public int IsJumpFall{ get { return isJumpFall; } }
        public int IsJumpLand { get { return isJumpLand; } }
        public bool IsMovementPressed
        {
            get { return isMovementPressed; }
            set { IsMovementPressed = value; }
        }
        public bool IsRunPressed
        {
            get { return isRunPressed; }
        }
        public  int IsWalk { get { return isWalk; } }
        
        public float AppliedMovementX { get { return applyMovement.x; } set { applyMovement.x = value; } }
        public float AppliedMovementZ { get { return applyMovement.z; } set { applyMovement.z = value; } }

        public Vector2 CurrentMovementInput { get { return currentMovementInput; } }


        private void Awake()
        {
            animatorController = GetComponentInChildren<Animator>();
            m_CharacterController = GetComponent<CharacterController>();

            states = new PlayerStateFactory(this);
            currentState = states.Grounded();
            currentState.EnterState();
            
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
        
        private void SetupJumpVariables()
        {
            float timeToApex = m_MaxJumpTime / 2;
            m_Gravity = (-2 * m_MaxJumpHeight) / Mathf.Pow(timeToApex, 2);
            initialJumpVelocity = (2 * m_MaxJumpHeight) / timeToApex;
        }
        
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
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
        
        private void OnRun(InputAction.CallbackContext context)
        {
            isRunPressed = context.ReadValueAsButton();
        }
        
        private void OnJump(InputAction.CallbackContext context)
        {
            isJumpPressed = context.ReadValueAsButton();
            requireNewJumpPress = false;
        }

        private void Update()
        {
            currentState.UpdateStates();
            
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
            m_CharacterController.Move(move * Time.deltaTime);
            m_CharacterController.Move(gravityMove * Time.deltaTime);
            
            
            float mouseX = currentRotation.x * m_MouseSpeed * Time.deltaTime;
            float mouseY = currentRotation.y * m_MouseSpeed * Time.deltaTime;
            
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, m_MinimumX, m_MaximumX);
            
            m_PlayerArm.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
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