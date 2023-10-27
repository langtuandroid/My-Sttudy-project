﻿namespace FK_3.Player.StateMachine
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
    }
}