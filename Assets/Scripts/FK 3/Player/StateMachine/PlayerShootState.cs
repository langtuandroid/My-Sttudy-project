namespace FK_3.Player.StateMachine
{
    public class PlayerShootState : PlayerBaseState
    {
        public PlayerShootState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
            : base(currentContext, playerStateFactory) { }

        public override void EnterState()
        {
            Ctx.AnimatorController.SetTrigger(Ctx.IsShoot);
        }

        protected override void UpdateState()
        {
            CheckSwitchSates();
        }

        protected override void ExitState()
        {
        }

        public override void CheckSwitchSates()
        {
            if (Ctx.IsAimPressed) SwitchState(Factory.Aim());
            else if (!Ctx.IsMovementPressed) SwitchState(Factory.Idle());
            else if (Ctx.IsMovementPressed) SwitchState(Factory.Walk());
        }

        public override void InitializeSubState()
        {
        }
    }
}