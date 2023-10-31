﻿using UnityEngine;

namespace FK_3.Player.StateMachine
{
    public class PlayerAimState : PlayerBaseState
    {
        public PlayerAimState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }

        public override void EnterState()
        {
            Ctx.AnimatorController.SetBool(Ctx.IsAim, true);
        }

        protected override void UpdateState()
        {
            CheckSwitchSates();
            HandleGravity();
        }

        protected override void ExitState()
        {
            Ctx.AnimatorController.SetBool(Ctx.IsAim, false);
        }

        public override void CheckSwitchSates()
        {
            if(!Ctx.IsAimPressed && !Ctx.IsMovementPressed) SwitchState(Factory.Idle());
            if(!Ctx.IsAimPressed && Ctx.IsMovementPressed) SwitchState(Factory.Walk());
        }

        public sealed override void InitializeSubState() { }
        
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