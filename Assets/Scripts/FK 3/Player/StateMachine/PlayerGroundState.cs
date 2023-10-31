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
            Ctx.CurrentMovementY = Ctx.GroundedGravity;
            Ctx.ApplyMovementY = Ctx.GroundedGravity;
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
            if (Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress)
            {
                SwitchState(Factory.Jump());
            }
        }

        public override void InitializeSubState()
        {
            SetSubState(!Ctx.IsMovementPressed ? Factory.Idle() : Factory.Walk());
        }
    }
}