using Unity.Mathematics;
using UnityEngine;

namespace Busket_Scripts
{
    public class BusketController : MonoBehaviour
    {
        [SerializeField] private GameObject m_BouncyProjectile;
        private void OnTriggerStay(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                Instantiate(m_BouncyProjectile, other.gameObject.transform.position, quaternion.identity);
                Destroy(other.gameObject);
            }
        }
    }
}