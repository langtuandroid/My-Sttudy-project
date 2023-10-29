using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Math
{
    public class MathStudy : MonoBehaviour
    {
        [SerializeField] private Vector2 m_A;
        [SerializeField] private Vector2 m_B;
        private Vector2 aaddB;
        private Vector2 asubB;
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
            Handles.Label(m_A/2f, "A", style);
            Gizmos.DrawSphere(m_A, 0.1f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(Vector2.zero, m_B);
            style.normal.textColor = Color.green;
            Handles.Label(m_B/2f, "B", style);
            Gizmos.DrawSphere(m_B, 0.1f);
            
            //2D Vector Add
            aaddB = m_A + m_B;
            Gizmos.color = Color.yellow;
            style.normal.textColor = Color.yellow;
            Handles.Label(aaddB, "A + B", style);
            Gizmos.DrawCube(aaddB, new Vector3(0.2f, 0.2f, 0.2f));
            
            //2D Vector Sub
            asubB = m_A - m_B;
            Gizmos.color = Color.blue;
            style.normal.textColor = Color.blue;
            Handles.Label(asubB, "A - B", style);
            Gizmos.DrawCube(asubB, new Vector3(0.2f, 0.2f, 0.2f));
            
            //Normal A + B Vector
            Vector2 aAddbNormalized = aaddB.normalized;
            Gizmos.color = Color.yellow;
            style.normal.textColor = Color.yellow;
            Handles.Label(aAddbNormalized, "A + B normalized", style);
            Gizmos.DrawCube(aAddbNormalized, new Vector3(0.1f, 0.1f, 0.1f));
            //*** aAddbNormalized / aAddbNormalized.magnitude ***
            
            //Length A and B vector
            style.normal.textColor = Color.red;
            Handles.Label(new Vector3(-5f, -6f), "Lenght Of a Vector = " + m_A.magnitude, style);
            style.normal.textColor = Color.green;
            Handles.Label(new Vector3(-5f, -6.5f), "Lenght Of b Vector = " + m_B.magnitude, style);
            
            //A and B Distance
            style.normal.textColor = Color.white;
            Handles.Label(new Vector3(-5f, -7f), "A and B Vector Distance = " + Vector2.Distance(m_A, m_B));
            //*** sqrt((a.x - b.x)^2 + (a.y - b.y)^2) ***
            
            //A to B Direction
            Vector2 aTob = (m_B - m_A);
            Vector2 aTobDir = aTob.normalized;
            Gizmos.color = Color.white;
            Gizmos.DrawLine(m_A, m_A + aTobDir);
            Gizmos.DrawCube(m_A + aTobDir, new Vector3(0.1f, 0.1f, 0.1f));
            Handles.Label(m_A + aTobDir, "A to B Dir", style);
            
            //Dot Product Dot(a, b) = (a.x . b.x) + (a.y . b.y) *** a Lenght = sqrt(Dot(a, a)) ***
            Vector2 aNormalize = m_A.normalized;
            Vector2 bNormalize = m_B.normalized;
            float aDotb = Vector2.Dot(aNormalize, bNormalize);
            Handles.Label(new Vector3(-5f, -7.5f), "A Dot B Vector  = " + aDotb);
            if (aDotb > 0f) Gizmos.color = Color.green;
            else Gizmos.color = Color.red;
            Gizmos.DrawLine(aNormalize,  bNormalize * Mathf.Abs(aDotb));
            Gizmos.DrawSphere(bNormalize * Mathf.Abs(aDotb), 0.08f);
            Handles.Label(bNormalize * Mathf.Abs(aDotb), "A Dot B");
        }
        //------------------------------------------------------------------------------------------------------------------------------------
    }
}