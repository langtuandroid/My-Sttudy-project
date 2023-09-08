using DG.Tweening;
using UnityEngine;

namespace Player.StateMachine
{
    public class PlayerWorryIdleState : PlayerBaseState
    {
        private static readonly int WorryIdle = Animator.StringToHash("WorryIdle");

        public override void EnterState(PlayerStateManager player)
        {
            player.m_Animator.SetBool(WorryIdle, true);

            DOVirtual.DelayedCall(player.m_WorryIdleDuration, delegate
            {
                player.m_Animator.SetBool(WorryIdle, false);
            }, false);
        }
    }
}