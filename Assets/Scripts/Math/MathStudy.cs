using UnityEditor;
using UnityEngine;

namespace Math
{
    public class MathStudy : MonoBehaviour
    {
        [SerializeField] private Vector2 m_A;
        [SerializeField] private Vector2 m_B;
        private Vector2 _aaddB;
        private Vector2 _asubB;
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
            
            //a.x and a.y vector Direction
            style.normal.textColor =  Color.red;
            Handles.Label(new Vector3(-5f, -4f), "Sign(a.x) = " + Mathf.Sign(m_A.x), style);
            Handles.Label(new Vector3(-5f, -4.5f), "Sign(a.y) = " + Mathf.Sign(m_A.y), style);
            
            //b.x and b.y vector Direction
            style.normal.textColor = Color.green;
            Handles.Label(new Vector3(-5f, -5f), "Sign(b.x) = " + Mathf.Sign(m_B.x), style);
            Handles.Label(new Vector3(-5f, -5.5f), "Sign(b.y) = " + Mathf.Sign(m_B.y), style);
            
            //Point A and B Vector
            Gizmos.color = Color.red;
            Gizmos.DrawLine(Vector2.zero, m_A);
            style.normal.textColor = Color.red;
            Handles.Label(m_A/2f, "a", style);
            Gizmos.DrawSphere(m_A, 0.1f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(Vector2.zero, m_B);
            style.normal.textColor = Color.green;
            Handles.Label(m_B/2f, "b", style);
            Gizmos.DrawSphere(m_B, 0.1f);
            
            //2D Vector Add
            _aaddB = m_A + m_B;
            Gizmos.color = Color.yellow;
            style.normal.textColor = Color.yellow;
            Handles.Label(_aaddB, "a + b", style);
            Gizmos.DrawCube(_aaddB, new Vector3(0.2f, 0.2f, 0.2f));
            
            //2D Vector Sub
            _asubB = m_A - m_B;
            Gizmos.color = Color.blue;
            style.normal.textColor = Color.blue;
            Handles.Label(_asubB, "a - b", style);
            Gizmos.DrawCube(_asubB, new Vector3(0.2f, 0.2f, 0.2f));
            
            //Normal A + B Vector
            Vector2 aAddbNormalized = _aaddB.normalized;
            Gizmos.color = Color.yellow;
            style.normal.textColor = Color.yellow;
            Handles.Label(aAddbNormalized, "a + b N", style);
            Gizmos.DrawCube(aAddbNormalized, new Vector3(0.1f, 0.1f, 0.1f));
            
            //Length A and B vector
            style.normal.textColor = Color.red;
            Handles.Label(new Vector3(-5f, -6f), "Lenght Of a Vector = " + m_A.magnitude, style);
            style.normal.textColor = Color.green;
            Handles.Label(new Vector3(-5f, -6.5f), "Lenght Of b Vector = " + m_B.magnitude, style);
        }
        
        //------------------------------------------------------------------------------------------------------------------------------------
    }
}