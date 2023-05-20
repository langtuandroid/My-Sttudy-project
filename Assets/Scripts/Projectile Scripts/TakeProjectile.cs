using UnityEngine;

namespace Projectile_Scripts
{
    public class TakeProjectile : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}