﻿using UnityEngine;
using UnityEngine.InputSystem;

namespace FK_3.Player.StateMachine
{
    public class PlayerStateMachine : MonoBehaviour
    {
        private PlayerInputAction _playerInputAction;
        private Vector2 _currentMovementInput;
        private Vector2 _currentRotationInput;
        private Vector3 _currentMovement;
        private Vector3 _currentRunMovement;
        private Vector3 _applyMovement;
        private Vector2 _currentRotation;
        private bool _isMovementPressed;
        private bool _isRunPressed;

        public CharacterController m_CharacterController;
        private Animator _animatorController;

        private const float walkMultiplier = 3f;
        private const float runMultiplier = 5f;

        private Transform _trans;
        private readonly int _isWalk = Animator.StringToHash("isWalk");
        
        [SerializeField] private Transform m_PlayerArm;
        [SerializeField] private float m_MinimumX = -90.0f;
        [SerializeField] private float m_MaximumX = 90.0f;
        [SerializeField] private float m_MouseSpeed = 10f;
        
        private float _rotationX;

        public float m_Gravity = -9.8f;
        public float m_GroundedGravity = -0.05f;
        
        private float _initialJumpVelocity;
        public float m_MaxJumpHeight = 3f;
        public float m_MaxJumpTime = 0.75f;
        private bool _isJumpPressed;
        private readonly int _isJumpUp = Animator.StringToHash("isJumpUp");
        private readonly int _isJumpFall = Animator.StringToHash("isJumpFall");
        private readonly int _isJumpLand = Animator.StringToHash("isJumpLand");
        private bool _requireNewJumpPress;


        private PlayerBaseState _currentState;
        private PlayerStateFactory _states;

        public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
        public Animator AnimatorController { get { return _animatorController; } }
        public float InitialJumpVelocity { get { return _initialJumpVelocity; } }
        public bool RequireNewJumpPress { get { return _requireNewJumpPress;} set { _requireNewJumpPress = value; } }
        public bool IsJumpPressed { get { return _isJumpPressed; } set { _isJumpPressed = value; } }
        public float CurrentMovementY { get { return _currentMovement.y; } set { _currentMovement.y = value; } }
        public float AppliedMovementY { get { return _applyMovement.y; } set { _applyMovement.y = value; } }
        public  int IsJumpUp { get { return _isJumpUp; } }
        public int IsJumpFall{ get { return _isJumpFall; } }
        public int IsJumpLand { get { return _isJumpLand; } }
        public bool IsMovementPressed
        {
            get { return _isMovementPressed; }
            set { IsMovementPressed = value; }
        }
        public bool IsRunPressed
        {
            get { return _isRunPressed; }
        }
        public  int IsWalk { get { return _isWalk; } }
        
        public float AppliedMovementX { get { return _applyMovement.x; } set { _applyMovement.x = value; } }
        public float AppliedMovementZ { get { return _applyMovement.z; } set { _applyMovement.z = value; } }

        public Vector2 CurrentMovementInput { get { return _currentMovementInput; } }


        private void Awake()
        {
            _animatorController = GetComponentInChildren<Animator>();
            m_CharacterController = GetComponent<CharacterController>();

            _states = new PlayerStateFactory(this);
            _currentState = _states.Grounded();
            _currentState.EnterState();
            
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
            _initialJumpVelocity = (2 * m_MaxJumpHeight) / timeToApex;
        }
        
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        private void OnMovementInput(InputAction.CallbackContext context)
        {
            _currentMovementInput = context.ReadValue<Vector2>();
            _currentMovement.x = _currentMovementInput.x * walkMultiplier;
            _currentMovement.z = _currentMovementInput.y * walkMultiplier;
            _currentRunMovement.x = _currentMovementInput.x * runMultiplier;
            _currentRunMovement.z = _currentMovementInput.y * runMultiplier;
            _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;    
        }
        
        private void OnRotationInput(InputAction.CallbackContext context)
        {
            _currentRotationInput = context.ReadValue<Vector2>();
            _currentRotation.x = _currentRotationInput.x;
            _currentRotation.y = _currentRotationInput.y;
        }
        
        private void OnRun(InputAction.CallbackContext context)
        {
            _isRunPressed = context.ReadValueAsButton();
        }
        
        private void OnJump(InputAction.CallbackContext context)
        {
            _isJumpPressed = context.ReadValueAsButton();
            _requireNewJumpPress = false;
        }

        private void Update()
        {
            _currentState.UpdateStates();
            
            _trans = transform;
            Vector3 move;
            Vector3 gravityMove;
            
            if (_isRunPressed)
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