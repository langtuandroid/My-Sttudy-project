using UnityEngine;

namespace FK_3.Player
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private Transform m_Player;
        [SerializeField] private Transform m_PlayerArm;
        [SerializeField] private float m_MouseSensitivity = 200f;
        
        [SerializeField] private float m_MinimumX = -90.0f;
        [SerializeField] private float m_MaximumX = 90.0f;

        private float rotationX;
        
        private void Start()
        { 
            Cursor.lockState = CursorLockMode.Locked;
        }
 
        private void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") *  m_MouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") *  m_MouseSensitivity * Time.deltaTime;
            
            m_Player.Rotate(Vector3.up * mouseX);
            
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, m_MinimumX, m_MaximumX);
            m_PlayerArm.localRotation = Quaternion.Euler(rotationX, 0, 0);
        }
    }
}