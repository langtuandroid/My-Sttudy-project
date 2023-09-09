using DG.Tweening;
using Sirenix.Utilities;
using UnityEngine;

namespace Player.StateMachine
{
    public class PlayerAnimatorEventFunctions : MonoBehaviour
    {
        [SerializeField] private Transform m_Player;
        
        [SerializeField] private GameObject[] m_KatanaBox;
        [SerializeField] private GameObject[] m_ActionKatanaBox;

        [SerializeField] private Vector3 m_PlayerJumpPosition;
        [SerializeField] private float m_JumpDuration;
        
        public void ActionKatanaBoxActivator()
        {
            m_KatanaBox.ForEach(k => k.SetActive(false));
            m_ActionKatanaBox.ForEach(k => k.SetActive(true));
        }
        
        public void Jump()
        {
            m_Player.DOMove(m_PlayerJumpPosition, m_JumpDuration).SetEase(Ease.OutCubic);
        }
        
        public void JumpCut()
        {
            m_Player.DOMove(m_PlayerJumpPosition + new Vector3(0f, 5f, 5f), 0.1f).SetEase(Ease.Linear);
        }

        public void Land()
        {
            m_Player.DOJump(new Vector3(0f, 0f, m_PlayerJumpPosition.z + 10f), 10, 1, 0.6f);
        }
    }
}