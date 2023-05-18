using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Projectile_Reflection_Scripts
{
    public class ProjectileReflectionController : MonoBehaviour
    {
        [Title("Projectile Data")] 
        [SerializeField] private float m_Speed;
        
        [Title("Projectile Prefab")] 
        [SerializeField] private GameObject m_Projectile;
        
        private RaycustController _raycustController;

        private void Awake()
        {
            _raycustController = GetComponent<RaycustController>();
        }

        private void Start()
        {
            _raycustController.OnRayHit += SpawnProjectile;
        }

        private void SpawnProjectile(Vector3 hitPoint)
        {
            GameObject projectile = Instantiate(m_Projectile, transform.position, Quaternion.identity);

            ThrowProjectile(projectile, hitPoint);
        }

        private void ThrowProjectile(GameObject projectile, Vector3 hitPoint)
        {
            
            float distance = Vector3.Distance(projectile.transform.position, hitPoint);
            float duration = distance / m_Speed; 
            
            projectile.transform.DOMove(hitPoint, duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (projectile.transform.position != Vector3.zero)
                {
                    ThrowProjectile(projectile,Vector3.zero);
                }
            });
        }
    }
}