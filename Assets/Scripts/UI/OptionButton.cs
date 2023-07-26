using System;
using UnityEngine;

namespace UI
{
    public class OptionButton : MonoBehaviour
    {
        public event Action<int> OnButtonClick;
        
        public bool m_IsActive;
        public int m_Index;
        public void Activate()
        {
            m_IsActive = true;
            OnButtonClick?.Invoke(m_Index);
        }

        public void Deactivate() => m_IsActive = false;
    }
}