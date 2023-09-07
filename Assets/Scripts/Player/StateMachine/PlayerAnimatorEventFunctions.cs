using DG.Tweening;
using Sirenix.Utilities;
using UnityEngine;

namespace Player.StateMachine
{
    public class PlayerAnimatorEventFunctions : MonoBehaviour
    {
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
            transform.DOMove(m_PlayerJumpPosition, m_JumpDuration).SetEase(Ease.Linear);
        }
        
    }
}