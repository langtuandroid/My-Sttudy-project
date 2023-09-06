using UnityEngine;

namespace Player.StateMachine
{
    public class PlayerAction1State : PlayerBaseState
    {
        private static readonly int Action1 = Animator.StringToHash("Action1");

        public override void EnterState(PlayerStateManager player)
        {
            player.m_Animator.SetBool(Action1, true);
        }
    }
}