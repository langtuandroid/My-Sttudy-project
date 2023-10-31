namespace FK_3.Player.StateMachine
{
    public class PlayerStateFactory
    {
        private PlayerStateMachine context;

        public PlayerStateFactory(PlayerStateMachine currentContext)
        {
            context = currentContext;
        }

        public PlayerBaseState Idle()
        {
            return new PlayerIdleState();
        }

        public PlayerBaseState Walk()
        {
            return new PlayerWalkState();
        }

        public PlayerBaseState Jump()
        {
            return new PlayerJumpState();
        }

        public PlayerBaseState Grounded()
        {
            return new PlayerGroundState();
        }
    }
}