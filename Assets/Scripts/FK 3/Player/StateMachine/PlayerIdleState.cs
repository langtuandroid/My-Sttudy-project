using UnityEngine;

namespace FK_3.Player.StateMachine
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory){}
        
        public override void EnterState()
        {
            Ctx.AnimatorController.SetBool(Ctx.IsWalk, false);
            
            Ctx.AppliedMovementX = 0f;
            Ctx.AppliedMovementZ = 0f;
        }
        public override void UpdateState()
        {
            CheckSwitchStates();
            HandleGravity();
        }
        public override void ExitState()
        {
        }
        public override void CheckSwitchStates()
        {
            if (Ctx.IsMovementPressed && Ctx.IsRunPressed)
            {
                SwitchState(Factory.Run());
            }
            else if (Ctx.IsMovementPressed)
            {
                SwitchState(Factory.Walk());
            }
        }
        public override void InitializeSubState()
        {
            
        }

        private void HandleGravity()
        {
            bool isFalling = Ctx.CurrentMovementY <= 0.0f || !Ctx.IsJumpPressed;
            float fallMultiplier = 2.0f;

            if (Ctx.m_CharacterController.isGrounded)
            {
                Ctx.AnimatorController.SetBool(Ctx.IsJumpFall, false);
                Ctx.AnimatorController.SetBool(Ctx.IsJumpLand, true);
            }
            else if (isFalling)
            {
                Ctx.AnimatorController.SetBool(Ctx.IsJumpUp, false);
                Ctx.AnimatorController.SetBool(Ctx.IsJumpFall, true);

                float previousYVelocity = Ctx.CurrentMovementY;
                Ctx.CurrentMovementY += Ctx.m_Gravity * fallMultiplier * Time.deltaTime;
                Ctx.AppliedMovementY = Mathf.Max((previousYVelocity + Ctx.CurrentMovementY) * 0.5f, -20.0f);
            }
            else
            {
                float previousYVelocity = Ctx.CurrentMovementY;
                Ctx.CurrentMovementY += Ctx.m_Gravity * Time.deltaTime;
                Ctx.AppliedMovementY = (previousYVelocity + Ctx.CurrentMovementY) * 0.5f;
            }
        }
    }
}