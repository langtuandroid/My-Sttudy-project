namespace FK_3.Player.StateMachine
{
    public class PlayerWalkState : PlayerBaseState
    {
        public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }
        
        public override void EnterState()
        {
            Ctx.AnimatorController.SetBool(Ctx.IsIdle, false);
            Ctx.AnimatorController.SetBool(Ctx.IsWalk, true);
        }

        public override void UpdateState()
        {
            CheckSwitchSates();
            
            Ctx.ApplyMovementX = Ctx.CurrentMovementInput.x * Ctx.MoveMultiplier;
            Ctx.ApplyMovementZ = Ctx.CurrentMovementInput.y * Ctx.MoveMultiplier;
        }

        public override void ExitState()
        {
          
        }

        public override void CheckSwitchSates()
        {
            if (!Ctx.IsMovementPressed)
            {
                SwitchState(Factory.Idle());
            }
        }

        public override void InitializeSubState()
        {
            
        }
    }
}