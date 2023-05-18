using UnityEngine;

namespace Projectile_Reflection_Scripts
{
    public class ProjectileReflectionController : MonoBehaviour
    {
        [SerializeField] private GameObject m_Projectile;
        
        private RaycustController _raycustController;

        private void Awake()
        {
            _raycustController = GetComponent<RaycustController>();
        }

        private void Start()
        {
            _raycustController.OnRayHit += ThrowProjectile;
        }

        private void ThrowProjectile(Vector3 hitPoint)
        {
            GameObject projectile = Instantiate(m_Projectile, transform.position, Quaternion.identity);
            
            
        }
    }
}