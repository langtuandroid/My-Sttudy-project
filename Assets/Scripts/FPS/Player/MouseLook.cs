using Sirenix.OdinInspector;
using UnityEngine;

namespace FPS.Player
{
    public class MouseLook : MonoBehaviour
    {

        [SerializeField] private Transform m_Player;
        [SerializeField] private float m_MouseSensitivity = 100f;
        [SerializeField, MinMaxSlider(-360f, 360f, true)] private Vector2 m_XClampRotation;

        private float xRotation;

        private void Start()
        { 
            xRotation = 90f;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") *  m_MouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") *  m_MouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, m_XClampRotation.x, m_XClampRotation.y);
            
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            m_Player.Rotate(Vector3.up * mouseX);
        }
    }
}