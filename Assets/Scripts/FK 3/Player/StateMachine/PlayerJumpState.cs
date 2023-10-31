using UnityEngine;

namespace FK_3.Player.StateMachine
{
    public class PlayerJumpState : PlayerBaseState
    {
        public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory)
        {
            IsRootState = true;
            InitializeSubState();
        }
        
        public override void EnterState()
        {
           HandleJump();
        }

        public override void UpdateState()
        {
            CheckSwitchSates();
            HandleGravity();
        }

        public override void ExitState()
        {
            Ctx.IsJumping = false;
            
            Ctx.AnimatorController.SetBool(Ctx.IsJumpFall, false);
            Ctx.AnimatorController.SetBool(Ctx.IsJumpLand, true);
            if (Ctx.IsJumpPressed)
            {
                Ctx.RequireNewJumpPress = true;
            }
        }

        public override void CheckSwitchSates()
        {
            if (Ctx.CharacterController.isGrounded)
            {
                SwitchState(Factory.Grounded());
            }
        }

        public override void InitializeSubState()
        {
            SetSubState(!Ctx.IsMovementPressed ? Factory.Idle() : Factory.Walk());
            
        }

        private void HandleJump()
        {
            Ctx.AnimatorController.SetBool(Ctx.IsJumpLand, false);
            Ctx.AnimatorController.SetBool(Ctx.IsJumpUp, true);
                    
            Ctx.IsJumping = true;
                
            Ctx.CurrentMovementY = Ctx.InitialJumpVelocity;
            Ctx.ApplyMovementY = Ctx.InitialJumpVelocity;
        }

        private void HandleGravity()
        {
            bool isFalling = Ctx.CurrentMovementY <= 0.0f || !Ctx.IsJumpPressed;
            float fallMultiplier = 2.0f;
            
            if (isFalling)
            {
                if(Ctx.IsJumping){
                    Ctx.AnimatorController.SetBool(Ctx.IsJumpUp, false);
                    Ctx.AnimatorController.SetBool(Ctx.IsJumpFall, true);
                }

                float previousYVelocity = Ctx.CurrentMovementY;
                Ctx.CurrentMovementY += Ctx.Gravity * fallMultiplier * Time.deltaTime;
                Ctx.ApplyMovementY = Mathf.Max((previousYVelocity + Ctx.CurrentMovementY) * 0.5f, -20.0f);
            }
            else
            {
                float previousYVelocity = Ctx.CurrentMovementY;
                Ctx.CurrentMovementY += Ctx.Gravity * Time.deltaTime;
                Ctx.ApplyMovementY = (previousYVelocity + Ctx.CurrentMovementY) * 0.5f;
            }
        }
    }
}