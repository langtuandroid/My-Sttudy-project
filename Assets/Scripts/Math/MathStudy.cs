using Sirenix.OdinInspector;
using UnityEditor;
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
            //------------------------------------------
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.cyan;
            style.fontSize = 12;
            
            Vector2 posX = new Vector2(-5, 0);
            Vector2 posY = new Vector2(0, -5);
            
            for(int i=-5; i<= 5; i++)
            {
                Handles.Label(posX, i.ToString(), style);
                if(i != 0) Handles.Label(posY, i.ToString(), style);
                
                posX += new Vector2(1, 0);
                posY += new Vector2(0, 1);
            }
            
            Gizmos.DrawLine(Vector2.left * 5f, Vector2.right * 5f);
            Gizmos.DrawLine(Vector2.down * 5f, Vector2.up * 5f);
            Gizmos.DrawWireSphere(Vector2.zero, 1f);
            //------------------------------------------
            
            //Point A and B Vector
            Gizmos.color = Color.red;
            Gizmos.DrawLine(Vector2.zero, m_A);
            Gizmos.DrawSphere(m_A, 0.1f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(Vector2.zero, m_B);
            Gizmos.DrawSphere(m_B, 0.1f);
            
            //2D Vector Add
            m_AaddB = m_A + m_B;
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(m_AaddB, new Vector3(0.2f, 0.2f, 0.2f));
            
            //2D Vector Sub
            m_AsubB = m_A - m_B;
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(m_AsubB, new Vector3(0.2f, 0.2f, 0.2f));
        }
        
        //------------------------------------------------------------------------------------------------------------------------------------
    }
}