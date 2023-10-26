using UnityEngine;

namespace FK_3.Player.StateMachine
{
    public class PlayerGroundState : PlayerBaseState
    {
        public PlayerGroundState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory)
        {
            IsRootState = true;
            InitializeSubState();
        }
        
        public override void EnterState()
        {
            Debug.Log("Enter Ground State.");
            
            Ctx.CurrentMovementY = Ctx.m_GroundedGravity;
            Ctx.AppliedMovementY = Ctx.m_GroundedGravity;
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
            if (Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress)
            {
                SwitchState(Factory.Jump());
            }
        }
        public override void InitializeSubState()
        {
            if (!Ctx.IsMovementPressed && !Ctx.IsRunPressed)
            {
                SetSubState(Factory.Idle());
            }
            else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed)
            {
                SetSubState(Factory.Walk());
            }
            else
            {
                Debug.Log("Run State Not Implemented Yet!");
            }
        }
    }
}