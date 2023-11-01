using DG.Tweening;
using UnityEngine;

namespace FK_3.Player.StateMachine
{
    public class PlayerReloadState : PlayerBaseState
    {
        public PlayerReloadState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
            : base(currentContext, playerStateFactory) { }

        public override void EnterState()
        {
            Ctx.IsReloading = true;
            Ctx.AnimatorController.SetTrigger(Ctx.IsReload);
            Ctx.IsReloaded = true;
            ResetReload();
        }

        protected override void UpdateState()
        {
            CheckSwitchSates();
            HandleGravity();
        }

        protected override void ExitState()
        {

        }

        public override void CheckSwitchSates()
        {
            if(!Ctx.IsMovementPressed) SwitchState(Factory.Idle());
            else if(Ctx.IsMovementPressed) SwitchState(Factory.Walk());
        }

        public override void InitializeSubState()
        {

        }
        
        private void ResetReload()
        {
            AnimatorStateInfo stateInfo = Ctx.AnimatorController.GetCurrentAnimatorStateInfo(0);
            float normalizedTime = stateInfo.normalizedTime;
            float animationTime = normalizedTime * stateInfo.length;
            DOVirtual.DelayedCall(animationTime, () => { Ctx.IsReloading = false; }, false);
        }

        private void HandleGravity()
        {
            if (Ctx.IsJumping) return;

            bool isFalling = Ctx.CurrentMovementY <= 0.0f || !Ctx.IsJumpPressed;
            float fallMultiplier = 2.0f;

            if (Ctx.CharacterController.isGrounded)
            {
                Ctx.CurrentMovementY = Ctx.GroundedGravity;
                Ctx.ApplyMovementY = Ctx.GroundedGravity;
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