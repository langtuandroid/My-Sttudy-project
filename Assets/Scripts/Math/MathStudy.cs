using Sirenix.OdinInspector;
using UnityEngine;

namespace Math
{
    public class MathStudy : MonoBehaviour
    {
        [SerializeField] private float m_SignXInput;
        [ButtonGroup]
        private void IDVectorDirection()
        {
            if (m_SignXInput == 0f)
            {
                Debug.Log("Sign Input can't be ZERO!!");
                return;
            }
            Debug.Log("Direction using Sign = " + Mathf.Sign(m_SignXInput));
        }
    }
}