using UnityEngine;
using UnityEngine.InputSystem;

namespace FK_3.Player
{
    public class PlayerAnimationAndMovementController : MonoBehaviour
    {
        private PlayerInputAction playerInputAction;

        private Vector2 currentMovementInput;
        private Vector3 currentMovement;
        private Vector3 currentRunMovement;
        private bool isMovementPressed;
        private bool isRunPressed;

        private CharacterController characterController;
        private Animator animatorController;

        float runMultiplier = 3f;
        
        private Transform trans;

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
        }

        private void OnRun(InputAction.CallbackContext context)
        {
            isRunPressed = context.ReadValueAsButton();
        }

        private void OnMovementInput(InputAction.CallbackContext context)
        {
            currentMovementInput = context.ReadValue<Vector2>();
            currentMovement.x = currentMovementInput.x;
            currentMovement.z = currentMovementInput.y;
            currentRunMovement.x = currentMovementInput.x * runMultiplier;
            currentRunMovement.z = currentMovementInput.y * runMultiplier;
            isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;    
        }
        
        private void HandleAnimation()
        {
            bool isWalk = animatorController.GetBool("isWalk");

            if (isMovementPressed && !isWalk)
            {
                animatorController.SetBool("isWalk", true);
            }
            if (!isMovementPressed && isWalk)
            {
                animatorController.SetBool("isWalk", false);
            }
        }
        
        private void Update()
        {
            HandleAnimation();
            
            trans = transform;
            Vector3 move;
            
            if (isRunPressed)
            {
                move = trans.right * currentRunMovement.x + trans.forward * currentRunMovement.z;
            }
            else
            { 
                move = trans.right * currentMovement.x + trans.forward * currentMovement.z;
            }
            
            characterController.Move(move * Time.deltaTime);
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