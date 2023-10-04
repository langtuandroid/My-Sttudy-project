using UnityEngine;

namespace FPS.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController m_CharacterController;
        [SerializeField] private float m_Speed;
        private void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Transform trans = transform;
            Vector3 move = trans.right * x + trans.forward * z;

            m_CharacterController.Move(move * (m_Speed * Time.deltaTime));
        }
    }
}