using UnityEngine;

namespace FPS.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator m_PlayerAnimator;
        [SerializeField] private Animator m_GunAnimator;
        [SerializeField] private PlayerMovement m_PlayerMovement;
        
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        private static readonly int IsIdle = Animator.StringToHash("isIdle");

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