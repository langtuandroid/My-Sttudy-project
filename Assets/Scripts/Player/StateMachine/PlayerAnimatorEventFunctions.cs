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
        
        public void ActionKatanaBoxActivator()
        {
            m_KatanaBox.ForEach(k => k.SetActive(false));
            m_ActionKatanaBox.ForEach(k => k.SetActive(true));
        }
        
        public void Jump()
        {
            transform.DOMove(m_PlayerJumpPosition, 1f).SetEase(Ease.Linear);
        }
        
    }
}