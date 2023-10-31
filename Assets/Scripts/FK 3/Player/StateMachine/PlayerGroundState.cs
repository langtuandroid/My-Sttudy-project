
using UnityEngine;

namespace FK_3.Player.StateMachine
{
    public class PlayerGroundState : PlayerBaseState
    {
        public override void EnterState()
        {
            Debug.Log("Player In Ground State");
        }

        public override void UpdateState()
        {
            
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