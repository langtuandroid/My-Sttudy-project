using UnityEngine;
using UnityEngine.InputSystem;

namespace FK_3.Player
{
    public class PlayerAnimationAndMovementController : MonoBehaviour
    {
        private PlayerInputAction playerInputAction;

        private Vector2 currentMovementInput;
        private Vector3 currentMovement;
        private bool isMovementPressed;

        private CharacterController characterController;
        private Animator animatorController;

        private Transform trans;

        private void Awake()
        {
            animatorController = GetComponentInChildren<Animator>();
            characterController = GetComponent<CharacterController>();
            
            playerInputAction = new PlayerInputAction();
            playerInputAction.CharacterControls.Move.started += OnMovementInput;
            playerInputAction.CharacterControls.Move.canceled += OnMovementInput;
            playerInputAction.CharacterControls.Move.performed += OnMovementInput;
        }

        private void OnMovementInput(InputAction.CallbackContext context)
        {
            currentMovementInput = context.ReadValue<Vector2>();
            currentMovement.x = currentMovementInput.x;
            currentMovement.z = currentMovementInput.y;
            isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;    
        }
        
        private void HandleAnimation()
        {
            bool isIdle = animatorController.GetBool("isIdle");
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
            Vector3 move = trans.right * currentMovement.x + trans.forward * currentMovement.z;
            
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