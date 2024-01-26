using UnityEngine;

namespace FK_3.Player.Ammo
{
    public class PlayerAmmo : MonoBehaviour
    {
        [SerializeField] private ProjectileTypeSo m_ProjectileTypeSo;
        [SerializeField] private Transform m_ProjectileGravingPoint;
        [SerializeField] private Transform m_ProjectileReloadPoint;
        [SerializeField] private Transform m_ProjectileSetPoint;
        
        private GameObject _projectile;

        #region Animation Function
        public void SpawnProjectile()
        {
            _projectile = Instantiate(m_ProjectileTypeSo.m_Projectile, m_ProjectileGravingPoint.position, transform.rotation);
            _projectile.transform.SetParent(m_ProjectileGravingPoint);
            _projectile.transform.position = m_ProjectileGravingPoint.position;
        }
        public void ReloadProjectile()
        {
            _projectile.transform.SetParent(m_ProjectileReloadPoint);
            _projectile.transform.position = m_ProjectileReloadPoint.position;
        }
        public void SetProjectile()
        {
            _projectile.transform.SetParent(m_ProjectileSetPoint);
            _projectile.transform.position = m_ProjectileSetPoint.position;
        }
        #endregion
        
        public void HideProjectile()
        {
            _projectile.transform.SetParent(null);
            _projectile.SetActive(false);
        }

    }
}