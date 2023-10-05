using UnityEngine;

namespace Math
{
    public class LerpAndSlerp : MonoBehaviour
    {
        [SerializeField] private Transform m_LerpPoint;
        [SerializeField] private Transform m_SlerpPoint;
        [SerializeField] private Transform m_Point1;
        [SerializeField] private Transform m_Point2;

        [SerializeField] private float m_MoveSpeed;

        private void Start()
        {
            Vector3 intItPos = m_Point1.position;
            m_LerpPoint.position = intItPos;
            m_SlerpPoint.position = intItPos;
        }

        private void Update()
        {
            Vector3 targetPos = m_Point2.position;
            
            Vector3 newPositionLerpPoint = Vector3.Lerp(m_LerpPoint.position, targetPos, m_MoveSpeed * Time.deltaTime);
            m_LerpPoint.position = newPositionLerpPoint;
            
            
            Vector3 newPositionSlerpPoint = Vector3.Slerp(m_SlerpPoint.position, targetPos, m_MoveSpeed * Time.deltaTime);
            m_SlerpPoint.position = newPositionSlerpPoint;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(m_LerpPoint.position, 0.05f);

            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(m_SlerpPoint.position, 0.05f);
            
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(m_Point1.position, 0.1f);
            Gizmos.DrawSphere(m_Point2.position, 0.1f);
        }
    }
}