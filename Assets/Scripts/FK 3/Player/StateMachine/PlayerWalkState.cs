using UnityEngine;

namespace FK_3.Player.StateMachine
{
    public class PlayerWalkState : PlayerBaseState
    {
        public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory){}
        
        public override void EnterState()
        {
            Debug.Log("Player Enter In Walk State.");
            
            Ctx.AnimatorController.SetBool(Ctx.IsWalk, true);
        }
        public override void UpdateState()
        {
            CheckSwitchStates();
            
            Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x;
            Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y;
        }
        public override void ExitState()
        {
        }
        public override void CheckSwitchStates()
        {
            if (!Ctx.IsMovementPressed)
            {
                Debug.Log("Start Idle From Walk");
                SwitchState(Factory.Idle());
            }
            else if (Ctx.IsMovementPressed && Ctx.IsRunPressed)
            {
                SwitchState(Factory.Run());
            }
        }
        public override void InitializeSubState()
        {
            
        }
    }
}