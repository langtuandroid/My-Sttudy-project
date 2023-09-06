using UnityEngine;

namespace Player.StateMachine
{
    public class PlayerIdleState : PlayerBaseState
    {
        private static readonly int Idle = Animator.StringToHash("Idle");

        public override void EnterState(PlayerStateManager player)
        {
            player.m_Animator.SetBool(Idle, true);
        }
    }
}