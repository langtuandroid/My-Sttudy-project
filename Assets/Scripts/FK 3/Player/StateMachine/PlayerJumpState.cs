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
            Ctx.AnimatorController.SetBool(Ctx.IsJumpLand, false);
            HandleJump();
        }
        public override void UpdateState()
        {
            CheckSwitchStates();
            HandleGravity();
        }
        public override void ExitState()
        {
            if (Ctx.IsJumpPressed)
            {
                Ctx.RequireNewJumpPress = true;
            }
        }
        public override void CheckSwitchStates()
        {
            if (Ctx.m_CharacterController.isGrounded)
            {
                SwitchState(Factory.Grounded());
            }
        }
        public override void InitializeSubState()
        {
        }

        private void HandleJump()
        {
            Ctx.AnimatorController.SetBool(Ctx.IsJumpUp, true);
            Ctx.RequireNewJumpPress = true;
            
            Ctx.CurrentMovementY = Ctx.InitialJumpVelocity;
            Ctx.AppliedMovementY = Ctx.InitialJumpVelocity;
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
                Ctx.CurrentMovementY +=  Ctx.m_Gravity * fallMultiplier * Time.deltaTime;
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