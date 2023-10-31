using Unity.VisualScripting;
using UnityEngine;

namespace FK_3.Player.StateMachine
{
    public class PlayerJumpState : PlayerBaseState
    {
        public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }
        
        public override void EnterState()
        {
           HandleJump();
        }

        public override void UpdateState()
        {
            CheckSwitchSate();
            HandleGravity();
        }

        public override void ExitState()
        {
            ctx.AnimatorController.SetBool(ctx.IsJumpFall, false);
            ctx.AnimatorController.SetBool(ctx.IsJumpLand, true);
            if (ctx.IsJumpPressed)
            {
                ctx.RequireNewJumpPress = true;
            }
        }

        public override void CheckSwitchSate()
        {
            if (ctx.CharacterController.isGrounded)
            {
                SwitchState(factory.Grounded());
            }
        }

        public override void InitializeSubState()
        {
            
        }

        private void HandleJump()
        {
            ctx.AnimatorController.SetBool(ctx.IsJumpLand, false);
            ctx.AnimatorController.SetBool(ctx.IsJumpUp, true);
                    
            ctx.IsJumping = true;
                
            ctx.CurrentMovementY = ctx.InitialJumpVelocity;
            ctx.ApplyMovementY = ctx.InitialJumpVelocity;
        }

        private void HandleGravity()
        {
            bool isFalling = ctx.CurrentMovementY <= 0.0f || !ctx.IsJumpPressed;
            float fallMultiplier = 2.0f;
            
            if (isFalling)
            {
                if(ctx.IsJumping){
                    ctx.AnimatorController.SetBool(ctx.IsJumpUp, false);
                    ctx.AnimatorController.SetBool(ctx.IsJumpFall, true);
                }

                float previousYVelocity = ctx.CurrentMovementY;
                ctx.CurrentMovementY += ctx.Gravity * fallMultiplier * Time.deltaTime;
                ctx.ApplyMovementY = Mathf.Max((previousYVelocity + ctx.CurrentMovementY) * 0.5f, -20.0f);
            }
            else
            {
                float previousYVelocity = ctx.CurrentMovementY;
                ctx.CurrentMovementY += ctx.Gravity * Time.deltaTime;
                ctx.ApplyMovementY = (previousYVelocity + ctx.CurrentMovementY) * 0.5f;
            }
        }
    }
}