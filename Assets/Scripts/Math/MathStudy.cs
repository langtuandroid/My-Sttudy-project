﻿using Sirenix.OdinInspector;
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

        [SerializeField] private Vector2 m_A;
        [SerializeField] private Vector2 m_B;
        [SerializeField] private Vector2 m_AaddB;
        [SerializeField] private Vector2 m_AsubB;
        private void OnDrawGizmos()
        {
            m_AaddB = m_A + m_B;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(Vector2.zero, m_A);
            Gizmos.DrawSphere(m_A, 0.1f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(Vector2.zero, m_B);
            Gizmos.DrawSphere(m_B, 0.1f);
            
            //2D Vector Add
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(m_AaddB, m_A);
            Gizmos.DrawSphere(m_AaddB, 0.1f);
            
            //2D Vector Sub
            m_AsubB = m_A - m_B;
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(m_AsubB, m_A);
            Gizmos.DrawSphere(m_AsubB, 0.1f);
        }
        
        //------------------------------------------------------------------------------------------------------------------------------------
    }
}