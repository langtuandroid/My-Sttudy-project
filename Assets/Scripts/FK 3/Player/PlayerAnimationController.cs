using UnityEngine;

namespace FK_3.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator m_PlayerAnimator;
        [SerializeField] private PlayerMovement m_PlayerMovement;
        
        private static readonly int IsWalking = Animator.StringToHash("isWalk");
        private static readonly int IsIdle = Animator.StringToHash("isIdle");
        private static readonly int IsReload = Animator.StringToHash("isReload");
        private static readonly int IsJump = Animator.StringToHash("isJump");
        private static readonly int IsFire = Animator.StringToHash("isFire");

        private void Update()
        {
            if (m_PlayerMovement.IsMoving)
            {
                m_PlayerAnimator.SetBool(IsWalking, true);
                m_PlayerAnimator.SetBool(IsIdle, false);
            }
            else
            {
                m_PlayerAnimator.SetBool(IsIdle, true);
                m_PlayerAnimator.SetBool(IsWalking, false);
            }
        }
    }
}