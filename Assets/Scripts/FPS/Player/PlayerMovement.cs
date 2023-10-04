using Sirenix.OdinInspector;
using UnityEngine;

namespace FPS.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Title("Player Movement Data")]
        [SerializeField] private CharacterController m_CharacterController;
        [SerializeField] private float m_Speed;
        [SerializeField] private float m_Gravity = -9.81f;
        [SerializeField] private float m_JumpHeight = 3f;
        [SerializeField] private Transform m_GroundCheck;
        [SerializeField] private float m_GroundCheckDistance = 0.4f;
        [SerializeField] private LayerMask m_GroundCheckLayerMask;
        
        private Vector3 velocity;
        private bool isGrounded;
        
        private void Update()
        {
            GroundCheck();
            Move();
            if (Input.GetButtonDown("Jump") && isGrounded) Jump();
            ApplyGravity();
        }

        private void GroundCheck()
        {
            isGrounded = Physics.CheckSphere(m_GroundCheck.position, m_GroundCheckDistance, m_GroundCheckLayerMask);
            if (isGrounded && velocity.y < 0) velocity.y = -5f;
        }

        private void Move()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Transform trans = transform;
            Vector3 move = trans.right * x + trans.forward * z;

            m_CharacterController.Move(move * (m_Speed * Time.deltaTime));
        }

        private void Jump()
        {
            velocity.y += Mathf.Sqrt(m_JumpHeight * -2 * m_Gravity);
        }

        private void ApplyGravity()
        {
            velocity.y += m_Gravity * Time.deltaTime;
            m_CharacterController.Move(velocity * Time.deltaTime);
        }
    }
}