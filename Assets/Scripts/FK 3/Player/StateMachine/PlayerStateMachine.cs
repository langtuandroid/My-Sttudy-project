using UnityEngine;
using UnityEngine.InputSystem;

namespace FK_3.Player.StateMachine
{
    public class PlayerStateMachine : MonoBehaviour
    {
        private PlayerInputAction _playerInputAction;
        private Vector2 _currentRotationInput;
        private Vector3 _currentMovement;
        private Vector3 _currentRunMovement;
        private Vector3 _applyMovement;
        private Vector2 _currentRotation;

        public CharacterController m_CharacterController;

        private const float WalkMultiplier = 3f;
        private const float RunMultiplier = 5f;

        private Transform _trans;

        [SerializeField] private Transform m_PlayerArm;
        [SerializeField] private float m_MinimumX = -90.0f;
        [SerializeField] private float m_MaximumX = 90.0f;
        [SerializeField] private float m_MouseSpeed = 10f;
        
        private float _rotationX;

        public float m_Gravity = -9.8f;
        public float m_GroundedGravity = -0.05f;

        public float m_MaxJumpHeight = 3f;
        public float m_MaxJumpTime = 0.75f;


        private PlayerStateFactory _states;

        public PlayerBaseState CurrentState { get; set; }

        public Animator AnimatorController { get; private set; }
        public float InitialJumpVelocity { get; private set; }
        public bool RequireNewJumpPress { get; set; }

        public bool IsJumpPressed { get; set; }

        public float CurrentMovementY { get { return _currentMovement.y; } set { _currentMovement.y = value; } }
        public float AppliedMovementY { get { return _applyMovement.y; } set { _applyMovement.y = value; } }
        public  int IsJumpUp { get; } = Animator.StringToHash("isJumpUp");
        public int IsJumpFall { get; } = Animator.StringToHash("isJumpFall");
        public int IsJumpLand { get; } = Animator.StringToHash("isJumpLand");
        public bool IsMovementPressed { get; private set; }
        public bool IsRunPressed { get; private set; }
        public  int IsWalk { get; } = Animator.StringToHash("isWalk");

        public float AppliedMovementX { set => _applyMovement.x = value; }
        public float AppliedMovementZ { set => _applyMovement.z = value; }

        public Vector2 CurrentMovementInput { get; private set; }


        private void Awake()
        {
            AnimatorController = GetComponentInChildren<Animator>();
            m_CharacterController = GetComponent<CharacterController>();

            _states = new PlayerStateFactory(this);
            CurrentState = _states.Grounded();
            CurrentState.EnterState();
            
            _playerInputAction = new PlayerInputAction();
            _playerInputAction.CharacterControls.Move.started += OnMovementInput;
            _playerInputAction.CharacterControls.Move.canceled += OnMovementInput;
            _playerInputAction.CharacterControls.Move.performed += OnMovementInput;
            
            _playerInputAction.CharacterControls.Run.started += OnRun;
            _playerInputAction.CharacterControls.Run.canceled += OnRun;

            _playerInputAction.CharacterControls.Rotation.started += OnRotationInput;
            _playerInputAction.CharacterControls.Rotation.canceled += OnRotationInput;
            _playerInputAction.CharacterControls.Rotation.performed += OnRotationInput;
            
            _playerInputAction.CharacterControls.Jump.started += OnJump;
            _playerInputAction.CharacterControls.Jump.canceled += OnJump;

            SetupJumpVariables();
        }
        
        private void SetupJumpVariables()
        {
            float timeToApex = m_MaxJumpTime / 2;
            m_Gravity = (-2 * m_MaxJumpHeight) / Mathf.Pow(timeToApex, 2);
            InitialJumpVelocity = (2 * m_MaxJumpHeight) / timeToApex;
        }
        
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        private void OnMovementInput(InputAction.CallbackContext context)
        {
            CurrentMovementInput = context.ReadValue<Vector2>();
            _currentMovement.x = CurrentMovementInput.x * WalkMultiplier;
            _currentMovement.z = CurrentMovementInput.y * WalkMultiplier;
            _currentRunMovement.x = CurrentMovementInput.x * RunMultiplier;
            _currentRunMovement.z = CurrentMovementInput.y * RunMultiplier;
            IsMovementPressed = CurrentMovementInput.x != 0 || CurrentMovementInput.y != 0;    
        }
        
        private void OnRotationInput(InputAction.CallbackContext context)
        {
            _currentRotationInput = context.ReadValue<Vector2>();
            _currentRotation.x = _currentRotationInput.x;
            _currentRotation.y = _currentRotationInput.y;
        }
        
        private void OnRun(InputAction.CallbackContext context)
        {
            IsRunPressed = context.ReadValueAsButton();
        }
        
        private void OnJump(InputAction.CallbackContext context)
        {
            IsJumpPressed = context.ReadValueAsButton();
            RequireNewJumpPress = false;
        }

        private void Update()
        {
            CurrentState.UpdateStates();
            
            _trans = transform;
            Vector3 move;
            Vector3 gravityMove;
            
            if (IsRunPressed)
            {
                _applyMovement = _currentRunMovement;
                
                move = _trans.right * _applyMovement.x + _trans.forward * _applyMovement.z;
                gravityMove = _trans.up * _applyMovement.y;
            }
            else
            { 
                _applyMovement= _currentMovement;
                
                move = _trans.right * _applyMovement.x + _trans.forward * _applyMovement.z;
                gravityMove = _trans.up * _applyMovement.y;
            }
            m_CharacterController.Move(move * Time.deltaTime);
            m_CharacterController.Move(gravityMove * Time.deltaTime);
            
            
            float mouseX = _currentRotation.x * m_MouseSpeed * Time.deltaTime;
            float mouseY = _currentRotation.y * m_MouseSpeed * Time.deltaTime;
            
            _rotationX -= mouseY;
            _rotationX = Mathf.Clamp(_rotationX, m_MinimumX, m_MaximumX);
            
            m_PlayerArm.localRotation = Quaternion.Euler(_rotationX, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }

        private void OnEnable()
        {
            _playerInputAction.CharacterControls.Enable();
        }

        private void OnDisable()
        {
            _playerInputAction.CharacterControls.Disable();
        }
    }
}