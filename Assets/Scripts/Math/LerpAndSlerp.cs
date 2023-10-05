using DG.Tweening;
using UnityEngine;

namespace Math
{
    public class LerpAndSlerp : MonoBehaviour
    {
        [SerializeField] private Transform m_LerpPoint;
        [SerializeField] private Transform m_SlerpPoint;
        [SerializeField] private Transform m_Point1;
        [SerializeField] private Transform m_Point2;
        [SerializeField] private Transform m_Point3;
        [SerializeField] private Transform m_Point4;

        [SerializeField] private float m_MoveSpeed;

        private void Start()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(m_LerpPoint.DOMove(m_Point1.position, m_MoveSpeed).SetSpeedBased(true));
            sequence.OnComplete(() => { sequence.Append(m_LerpPoint.DOMove(m_Point2.position, m_MoveSpeed).SetSpeedBased(true)); });
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
            Gizmos.DrawSphere(m_Point3.position, 0.1f);
            Gizmos.DrawSphere(m_Point4.position, 0.1f);
        }
    }
}