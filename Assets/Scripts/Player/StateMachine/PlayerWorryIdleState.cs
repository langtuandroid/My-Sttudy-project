using UnityEngine;

namespace Player.StateMachine
{
    public class PlayerWorryIdleState : PlayerBaseState
    {
        private static readonly int WorryIdle = Animator.StringToHash("WorryIdle");

        public override void EnterState(PlayerStateManager player)
        {
            player.m_Animator.SetBool(WorryIdle, true);
        }
    }
}