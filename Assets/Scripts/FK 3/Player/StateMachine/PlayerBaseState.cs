namespace FK_3.Player.StateMachine
{
    public abstract class PlayerBaseState
    {
        private PlayerBaseState _currentSubState;
        private PlayerBaseState _currentSuperState;
        
        protected bool IsRootState { get; set; }
        protected PlayerStateMachine Ctx { get; }
        protected PlayerStateFactory Factory { get; }
        public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        {
            Ctx = currentContext;
            Factory = playerStateFactory;
        }
        
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
        public abstract void CheckSwitchStates();
        public abstract void InitializeSubState();

        public void UpdateStates()
        {
            UpdateState();
            _currentSubState?.UpdateStates();
        }
        protected void SwitchState(PlayerBaseState newState)
        {
            ExitState();
            
            newState.EnterState();

            if (IsRootState)
            {
                Ctx.CurrentState = newState;
            }
            else
            {
                _currentSuperState?.SetSubState(newState);
            }
        }
        protected void SetSuperState(PlayerBaseState newSuperState)
        {
            _currentSuperState = newSuperState;
        }
        protected void SetSubState(PlayerBaseState newSubState)
        {
            _currentSubState = newSubState;
            newSubState.SetSuperState(this);
        }
    }
}