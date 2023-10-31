namespace FK_3.Player.StateMachine
{
    public class PlayerWalkState : PlayerBaseState
    {
        public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }
        
        public override void EnterState()
        {

        }

        public override void UpdateState()
        {
            CheckSwitchSate();
        }

        public override void ExitState()
        {
          
        }

        public override void CheckSwitchSate()
        {
            
        }

        public override void InitializeSubState()
        {
            
        }
    }
}