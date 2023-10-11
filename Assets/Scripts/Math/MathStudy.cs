using Sirenix.OdinInspector;
using UnityEngine;

namespace Math
{
    public class MathStudy : MonoBehaviour
    {
        //------------------------------------------------------------------------------------------------------------------------------------
        [SerializeField] private float m_SignXInput;
        [Button]
        private void IDVectorDirection()
        {
            if (m_SignXInput == 0f)
            {
                Debug.Log("Sign Input can't be ZERO!!");
                return;
            }
            Debug.Log("Direction using Sign = " + Mathf.Sign(m_SignXInput));
        }
        //------------------------------------------------------------------------------------------------------------------------------------
        [SerializeField] private float m_PointA;
        [SerializeField] private float m_PointB;
        [Button]
        private void IDVectorTwoPointDistance() => Debug.Log("PointA and PointB Distance = " + Mathf.Abs(m_PointA - m_PointB));
        //------------------------------------------------------------------------------------------------------------------------------------
    }
}