using UnityEngine;

namespace Math
{
    public class LookAtTrigger : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float m_Accuracy = 0.5f;

        [SerializeField] private Transform m_PlayerTransform;
        private void OnDrawGizmos()
        {
            Vector2 center = transform.position;
            Vector2 playerPosition = m_PlayerTransform.position;
            Vector2 playerLookDir = m_PlayerTransform.right;

            Vector2 playerToCenterDir = (center - playerPosition).normalized;
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(playerPosition, playerPosition + playerToCenterDir);
            
            float playerDotObject = Vector2.Dot(playerToCenterDir, playerLookDir);
            
            Gizmos.color = playerDotObject >= m_Accuracy ? Color.green : Color.red;
            Gizmos.DrawLine(playerPosition, playerPosition + playerLookDir);
             
        }
    }
} 
