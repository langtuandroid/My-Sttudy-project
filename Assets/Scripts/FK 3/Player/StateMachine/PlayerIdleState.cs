using UnityEngine;

namespace FK_3.Player.StateMachine
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }
        
        public override void EnterState()
        {
            Ctx.AnimatorController.SetBool(Ctx.IsIdle, true);

            Ctx.ApplyMovementX = 0f;
            Ctx.ApplyMovementZ = 0f;
        }

        protected override void UpdateState()
        {
            CheckSwitchSates();
            HandleGravity();
        }

        protected override void ExitState()
        {
            Ctx.AnimatorController.SetBool(Ctx.IsIdle, false);
        }

        public override void CheckSwitchSates()
        {
            if (Ctx.IsMovementPressed) SwitchState(Factory.Walk());
            else if(Ctx.IsAimPressed) SwitchState(Factory.Aim());
            else if(Ctx.IsReloadPressed &&  !Ctx.IsReloading && !Ctx.IsReloaded) SwitchState(Factory.Reload());
        }

        public override void InitializeSubState() { }
        
        private void HandleGravity()
        {
            if(Ctx.IsJumping) return;
            
            bool isFalling = Ctx.CurrentMovementY <= 0.0f || !Ctx.IsJumpPressed;
            float fallMultiplier = 2.0f;
            
            if (Ctx.CharacterController.isGrounded)
            {
                Ctx.CurrentMovementY = Ctx.GroundedGravity;
                Ctx.ApplyMovementY  = Ctx.GroundedGravity;
            }
            
            else if (isFalling)
            {
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