using UnityEditor;
using UnityEngine;

namespace Math
{
    public class LerpAndSlerp : MonoBehaviour
    {
        [SerializeField] private Transform m_LerpPoint;
        [SerializeField] private Transform m_SlerpPoint;
        [SerializeField] private Transform m_TargetPos;

        [SerializeField] private float m_MoveSpeed;

        private void Update()
        {
            Vector3 targetPos = m_TargetPos.position;
            
            Vector3 newPositionLerpPoint = Vector3.Lerp(m_LerpPoint.position, targetPos, m_MoveSpeed * Time.deltaTime);
            m_LerpPoint.position = newPositionLerpPoint;
            
            Vector3 newPositionSlerpPoint = Vector3.Slerp(m_SlerpPoint.position, targetPos, m_MoveSpeed * Time.deltaTime);
            m_SlerpPoint.position = newPositionSlerpPoint;
        }

        private void OnDrawGizmos()
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.fontSize = 16;
            style.alignment = TextAnchor.MiddleLeft - 1;
            
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(m_LerpPoint.position, 0.08f);
            Handles.Label(m_LerpPoint.position, "Lerp", style);

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(m_SlerpPoint.position, 0.08f);
            Handles.Label(m_SlerpPoint.position, "Slerp", style);
            
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(m_TargetPos.position, 0.1f);
            Handles.Label(m_TargetPos.position, "Target", style);
        }
    }
}