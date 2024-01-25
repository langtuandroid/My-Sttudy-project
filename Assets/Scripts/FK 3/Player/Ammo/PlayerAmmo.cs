using DG.Tweening;
using UnityEngine;

namespace FK_3.Player.Ammo
{
    public class PlayerAmmo : MonoBehaviour
    {
        [SerializeField] private ProjectileTypeSo m_ProjectileTypeSo;
        [SerializeField] private Transform m_ProjectileGravingPoint;
        [SerializeField] private Transform m_ProjectileReloadPoint;
        [SerializeField] private Transform m_ProjectileSetPoint;
        [SerializeField]private float m_ProjectileHitSpeed = 50f;
        
        private GameObject projectile;

        #region Animation Function
        public void SpawnProjectile()
        {
            projectile = Instantiate(m_ProjectileTypeSo.m_Projectile, m_ProjectileGravingPoint.position, transform.rotation);
            projectile.transform.SetParent(m_ProjectileGravingPoint);
            projectile.transform.position = m_ProjectileGravingPoint.position;
        }
        public void ReloadProjectile()
        {
            projectile.transform.SetParent(m_ProjectileReloadPoint);
            projectile.transform.position = m_ProjectileReloadPoint.position;
        }
        public void SetProjectile()
        {
            projectile.transform.SetParent(m_ProjectileSetPoint);
            projectile.transform.position = m_ProjectileSetPoint.position;
        }
        #endregion
        
        public void HideProjectile()
        {
            projectile.transform.SetParent(null);
            projectile.SetActive(false);
        }

    }
}