using Sirenix.Utilities;
using UnityEngine;

namespace Player
{
    public class PlayerActionController : MonoBehaviour
    {
        [SerializeField] private PlayerAnimationController m_PlayerAnimationController;


        [SerializeField] private GameObject[] m_KatanaBox;
        [SerializeField] private GameObject[] m_ActionKatanaBox;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                m_PlayerAnimationController.WorryIdleState();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                m_PlayerAnimationController.Action1State();
            }
        }

        public void ActionKatanaBoxActivator()
        {
            m_KatanaBox.ForEach(k => k.SetActive(false));
            m_ActionKatanaBox.ForEach(k => k.SetActive(true));
        }
    }
}