using UnityEngine;

namespace FK_3.Player.StateMachine
{
    public class PlayerGroundState : PlayerBaseState
    {
        public override void EnterState()
        {
            Debug.Log("Player is Grounded.");
        }
        public override void UpdateState()
        {
        }
        public override void ExitState()
        {
        }
        public override void CheckSwitchStates()
        {

        }
        public override void InitializeSubState()
        {
        }
    }
}