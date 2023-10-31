using UnityEngine;
using UnityEngine.InputSystem;

namespace FK_3.Player.StateMachine
{
    public class PlayerStateMachine : MonoBehaviour
    {

        [SerializeField] private float m_WalkMultiplier = 3f;
        [SerializeField] private float m_RunMultiplier = 6f;
        
        
        private PlayerInputAction playerInputAction;
        private Vector2 currentRotationInput;
        private Vector3 currentMovement;
        private Vector3 applyMovement;
        private Vector2 currentRotation;
        public bool IsMovementPressed { get; private set; }
        private bool isRunPressed;

        public CharacterController CharacterController { get; private set; }
        public Animator AnimatorController { get; private set; }

        public float MoveMultiplier { get; private set; }
        
        private Transform trans;
        
        [SerializeField] private Transform m_PlayerArm;
        [SerializeField] private float m_MinimumX = -90.0f;
        [SerializeField] private float m_MaximumX = 90.0f;
        [SerializeField] private float m_MouseSpeed = 10f;
        
        private float rotationX;

        public float m_MaxJumpHeight = 3f;
        public float m_MaxJumpTime = 0.75f;

        public PlayerBaseState CurrentState { get; set; }
        private PlayerStateFactory states;
        
        public bool IsJumpPressed { get; private set; }
        
        public int IsIdle { get; } = Animator.StringToHash("isIdle");
        public int IsWalk { get; } = Animator.StringToHash("isWalk");
        public int IsJumpUp { get; } = Animator.StringToHash("isJumpUp");
        public int IsJumpFall { get; } = Animator.StringToHash("isJumpFall");
        public int IsJumpLand { get; } = Animator.StringToHash("isJumpLand");
        
        public bool RequireNewJumpPress { get; set; }
        public bool IsJumping { get; set; }

        public float CurrentMovementY { get => currentMovement.y; set => currentMovement.y = value; }
        public float ApplyMovementY { get => applyMovement.y; set => applyMovement.y = value; }
        public float ApplyMovementX { get => applyMovement.x; set => applyMovement.x = value; }
        public float ApplyMovementZ { get => applyMovement.z; set => applyMovement.z = value; }
        
        public Vector2 CurrentMovementInput { get; private set; }
        
        public float InitialJumpVelocity { get; private set; }

        public float Gravity { get; private set; } = -9.8f;

        public float GroundedGravity { get; set; } = -0.05f;

        private void Awake()
        {
            AnimatorController = GetComponentInChildren<Animator>();
            CharacterController = GetComponent<CharacterController>();
            
            states = new PlayerStateFactory(this);
            CurrentState = states.Grounded();
            CurrentState.EnterState();
            
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
        
        private void OnMovementInput(InputAction.CallbackContext context)
        {
            CurrentMovementInput = context.ReadValue<Vector2>();
            currentMovement.x = CurrentMovementInput.x;
            currentMovement.z = CurrentMovementInput.y;
            IsMovementPressed = CurrentMovementInput.x != 0 || CurrentMovementInput.y != 0;
        }
        
        private void OnRotationInput(InputAction.CallbackContext context)
        {
            currentRotationInput = context.ReadValue<Vector2>();
            currentRotation.x = currentRotationInput.x;
            currentRotation.y = currentRotationInput.y;
        }
        
        private void OnRun(InputAction.CallbackContext context) => isRunPressed = context.ReadValueAsButton();

        private void OnJump(InputAction.CallbackContext context)
        {
            IsJumpPressed = context.ReadValueAsButton();
            RequireNewJumpPress = false;
        }
        
        private void SetupJumpVariables()
        {
            float timeToApex = m_MaxJumpTime / 2;
            Gravity = (-2 * m_MaxJumpHeight) / Mathf.Pow(timeToApex, 2);
            InitialJumpVelocity = (2 * m_MaxJumpHeight) / timeToApex;
        }
        
        private void Update()
        {
            MoveMultiplier = isRunPressed ? m_RunMultiplier : m_WalkMultiplier;
            
            applyMovement = currentMovement;
            applyMovement.x *= MoveMultiplier;
            applyMovement.z *= MoveMultiplier;
            
            trans = transform;
            Vector3 move = (trans.right * applyMovement.x) + (trans.up * applyMovement.y) + (trans.forward * applyMovement.z);
            
            CharacterController.Move(move * Time.deltaTime);
            
            
            CurrentState.UpdateStates();
            
            
            float mouseX = currentRotation.x * m_MouseSpeed * Time.deltaTime;
            float mouseY = currentRotation.y * m_MouseSpeed * Time.deltaTime;
            
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, m_MinimumX, m_MaximumX);
            
            m_PlayerArm.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }
        
        private void OnEnable() =>  playerInputAction.CharacterControls.Enable();
        private void OnDisable() => playerInputAction.CharacterControls.Disable();
    }
}