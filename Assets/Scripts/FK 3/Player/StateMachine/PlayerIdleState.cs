namespace FK_3.Player.StateMachine
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }
        
        public override void EnterState()
        {
            Ctx.AnimatorController.SetBool(Ctx.IsWalk, false);
            Ctx.AnimatorController.SetBool(Ctx.IsIdle, true);

            Ctx.ApplyMovementX = 0f;
            Ctx.ApplyMovementZ = 0f;
        }

        public override void UpdateState()
        {
            CheckSwitchSates();
        }

        public override void ExitState()
        {
            
        }

        public override void CheckSwitchSates()
        {
            if (Ctx.IsMovementPressed)
            {
                SwitchState(Factory.Walk());
            }
        }

        public override void InitializeSubState()
        {
            
        }
    }
}