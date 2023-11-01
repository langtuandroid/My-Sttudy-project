namespace FK_3.Player.StateMachine
{
    public class PlayerStateFactory
    {
        private readonly PlayerStateMachine context;

        public PlayerStateFactory(PlayerStateMachine currentContext)
        {
            context = currentContext;
        }

        public PlayerBaseState Idle()
        {
            return new PlayerIdleState(context, this);
        }

        public PlayerBaseState Walk()
        {
            return new PlayerWalkState(context, this);
        }

        public PlayerBaseState Jump()
        {
            return new PlayerJumpState(context, this);
        }

        public PlayerBaseState Grounded()
        {
            return new PlayerGroundState(context, this);
        }
        
        public PlayerBaseState Aim()
        {
            return new PlayerAimState(context, this);
        }
        
        public PlayerBaseState Shoot()
        {
            return new PlayerShootState(context, this);
        }
    }
}