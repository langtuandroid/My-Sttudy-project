using UnityEngine;

namespace FK_3.Player.Ammo
{
    public class PlayerAmmo : MonoBehaviour
    {
        [SerializeField] private ProjectileTypeSo m_ProjectileTypeSo;
        [SerializeField] private Transform m_ProjectileGravingPoint;
        [SerializeField] private Transform m_ProjectileReloadPoint;
        
        private GameObject projectile;

        #region Animation Function
        
        public void SpawnProjectile()
        {
            projectile = Instantiate(m_ProjectileTypeSo.m_Projectile, m_ProjectileGravingPoint.position, Quaternion.identity);
            projectile.transform.SetParent(m_ProjectileGravingPoint);
        }
        public void ReloadProjectile()
        {
            projectile.transform.SetParent(null);
            projectile.transform.SetParent(m_ProjectileReloadPoint);
        }
        
        #endregion
    }
}