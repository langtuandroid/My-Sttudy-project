namespace FK_3.Player.StateMachine
{
    public abstract class PlayerBaseState
    {
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
        public abstract void CheckSwitchStates();
        public abstract void InitializeSubState();

        private void UpdateStates()
        {
        }
        private void SwitchState(PlayerBaseState newState)
        {
            ExitState();
            newState.ExitState();
        }
        private void SetSuperState()
        {
        }
        private void SetSubState()
        {
        }
    }
}