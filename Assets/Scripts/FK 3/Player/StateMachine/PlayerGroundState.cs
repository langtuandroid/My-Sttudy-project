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

        protected override void UpdateState()
        {
            CheckSwitchSates();
        }

        protected override void ExitState() { }

        public override void CheckSwitchSates()
        {
            if (Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress) SwitchState(Factory.Jump());
        }

        public sealed override void InitializeSubState()
        {
            if (!Ctx.IsMovementPressed) SetSubState(Factory.Idle());
            else if (Ctx.IsMovementPressed) SetSubState(Factory.Walk());
        }
    }
}