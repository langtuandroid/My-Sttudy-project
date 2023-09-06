using UnityEngine;

namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator m_Animator;
        private static readonly int WorryIdle = Animator.StringToHash("WorryIdle");
        private static readonly int Action1 = Animator.StringToHash("Action1");

        public void WorryIdleState()
        {
            m_Animator.SetBool(WorryIdle, true);
        }
        
        public void Action1State()
        {
            m_Animator.SetBool(Action1, true);
        }
    }
}