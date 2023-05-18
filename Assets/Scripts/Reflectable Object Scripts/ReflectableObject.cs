using DG.Tweening;
using Interfaces;
using UnityEngine;

namespace Reflectable_Object_Scripts
{
    public class ReflectableObject : MonoBehaviour , IReflectable
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Vector3 dir = (other.gameObject.transform.position - transform.position).normalized;
                Vector3 punchOffset = dir + new Vector3(0.1f, 0.1f, 0.1f);
                
                transform.DOPunchPosition(punchOffset, 0.5f);
            }
        }
    }
}