using UnityEngine;

namespace FPS.Player
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private Transform m_Player;
        [SerializeField] private Transform m_PlayerArm;
        [SerializeField] private float m_MouseSensitivity = 100f;
        
        private void Start()
        { 
            Cursor.lockState = CursorLockMode.Locked;
        }
 
        private void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") *  m_MouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") *  m_MouseSensitivity * Time.deltaTime;
            
            m_PlayerArm.Rotate(Vector3.left * mouseY);
            m_Player.Rotate(Vector3.up * mouseX);
        }
    }
}