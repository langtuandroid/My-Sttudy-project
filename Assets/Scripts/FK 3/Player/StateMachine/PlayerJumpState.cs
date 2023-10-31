namespace FK_3.Player.StateMachine
{
    public class PlayerJumpState : PlayerBaseState
    {
        public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }
        
        public override void EnterState()
        {
           HandleJump();
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

        private void HandleJump()
        {
            ctx.AnimatorController.SetBool(ctx.IsJumpLand, false);
            ctx.AnimatorController.SetBool(ctx.IsJumpUp, true);
            ctx.IsJumpingAnimating = true;
                    
            ctx.IsJumping = true;
                
            ctx.CurrentMovementY = ctx.InitialJumpVelocity;
            ctx.ApplyMovementY = ctx.InitialJumpVelocity;
        }
    }
}