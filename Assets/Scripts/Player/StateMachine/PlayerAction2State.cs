using UnityEngine;

namespace Player.StateMachine
{
    public class PlayerAction2State : PlayerBaseState
    {
        private static readonly int Action2 = Animator.StringToHash("Action2");

        public override void EnterState(PlayerStateManager player)
        {
            player.m_Animator.SetBool(Action2, true);
        }
    }
}