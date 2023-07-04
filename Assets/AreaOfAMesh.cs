using UnityEngine;
public class AreaOfAMesh : MonoBehaviour
{
   public float m_Area;
   [SerializeField] private Mesh m_Mesh;

   private void OnValidate()
   {
      Vector3[] verts = m_Mesh.vertices;
      int[] tris = m_Mesh.triangles;
      
      m_Area = 0;

      for (int i = 0; i < tris.Length; i+=3)
      {
         Vector3 a = verts[tris[i]];
         Vector3 b = verts[tris[i+1]];
         Vector3 c = verts[tris[i+2]];

         m_Area += Vector3.Cross(b - a, c - a).magnitude;
      }
      m_Area /= 2;
   }

#if UNITY_EDITOR
   private void OnDrawGizmos()
   {
      Vector3[] verts = m_Mesh.vertices;
      for (int i = 0; i < verts.Length; i++)
      {
         Gizmos.color = Color.blue;
         Gizmos.DrawSphere(verts[i], 0.005f);
      }
   }   
#endif
   
}
