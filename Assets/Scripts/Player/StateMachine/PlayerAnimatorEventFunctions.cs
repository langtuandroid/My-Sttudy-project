using Sirenix.Utilities;
using UnityEngine;

namespace Player.StateMachine
{
    public class PlayerAnimatorEventFunctions : MonoBehaviour
    {
        [SerializeField] private GameObject[] m_KatanaBox;
        [SerializeField] private GameObject[] m_ActionKatanaBox;
        
        public void ActionKatanaBoxActivator()
        {
            m_KatanaBox.ForEach(k => k.SetActive(false));
            m_ActionKatanaBox.ForEach(k => k.SetActive(true));
        }
    }
}