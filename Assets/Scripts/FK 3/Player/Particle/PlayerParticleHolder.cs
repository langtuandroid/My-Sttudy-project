using UnityEngine;

namespace FK_3.Player.Particle
{
    public class PlayerParticleHolder : MonoBehaviour
    {
        [SerializeField] private ParticleSystem m_BerryDrop;

        #region Animation Function
        public void PlayerBerryDrop()
        {
            m_BerryDrop.Simulate(0);
            m_BerryDrop.Play();
        }
        #endregion
    }
}