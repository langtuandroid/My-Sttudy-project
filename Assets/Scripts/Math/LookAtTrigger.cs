using UnityEngine;

namespace Math
{
    public class LookAtTrigger : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float m_Accuracy = 0.5f;

        [SerializeField] private Transform m_PlayerTransform;
        private void OnDrawGizmosSelected()
        {
            Vector2 playerDir = m_PlayerTransform.right;
            
            Vector2 center = transform.position;
            var playerPosition = m_PlayerTransform.position;
            Vector2 objPos = playerPosition;
            
            Gizmos.color = Color.white;
            Gizmos.DrawLine(center, objPos);
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(playerPosition, (Vector2)playerPosition + playerDir);
             
        }
    }
} 
