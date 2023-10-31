﻿namespace FK_3.Player.StateMachine
{
    public abstract class PlayerBaseState
    {
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
        public abstract void CheckSwitchSate();
        public abstract void InitializeSubState();
        
        private void UpdateStates(){}
        private void SwitchState(){}
        private void SetSuperState(){}
        private void SetSubState(){}
        
    }
}