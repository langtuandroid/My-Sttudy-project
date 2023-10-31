namespace FK_3.Player.StateMachine
{
    public abstract class PlayerBaseState
    {
        protected bool IsRootState { get; set; }
        protected PlayerStateMachine Ctx { get; }
        protected PlayerStateFactory Factory { get; }
        
        private PlayerBaseState currentSuperState;
        private PlayerBaseState currentSubState;
        
        public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        {
            Ctx = currentContext;
            Factory = playerStateFactory;
        }
        
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
        public abstract void CheckSwitchSates();
        public abstract void InitializeSubState();

        public void UpdateStates()
        {
            UpdateState();
            if (currentSubState != null)
            {
                currentSubState.UpdateStates();
            }
        }

        protected void SwitchState(PlayerBaseState newState)
        {
            ExitState();
            newState.EnterState();

            if (IsRootState)
            {
                Ctx.CurrentState = newState;
            }
            else if(currentSuperState != null)
            {
                currentSuperState.SetSubState(newState);
            }
        }

        protected void SetSuperState(PlayerBaseState newSuperState)
        {
            currentSuperState = newSuperState;
        }
        protected void SetSubState(PlayerBaseState newSubState)
        {
            currentSubState = newSubState;
            newSubState.SetSuperState(this);
        }
        
    }
}