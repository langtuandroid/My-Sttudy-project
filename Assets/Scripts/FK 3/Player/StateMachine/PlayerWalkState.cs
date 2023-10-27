namespace FK_3.Player.StateMachine
{
    public class PlayerWalkState : PlayerBaseState
    {
        public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory){}
        
        public override void EnterState()
        {
            Ctx.AnimatorController.SetBool(Ctx.IsWalk, true);
        }
        public override void UpdateState()
        {
            CheckSwitchStates();
            
            Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x;
            Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y;
        }
        public override void ExitState()
        {
        }
        public override void CheckSwitchStates()
        {
            if (!Ctx.IsMovementPressed)
            {
                SwitchState(Factory.Idle());
            }
            else if (Ctx.IsMovementPressed && Ctx.IsRunPressed)
            {
                SwitchState(Factory.Run());
            }
        }
        public override void InitializeSubState()
        {
            
        }
    }
}