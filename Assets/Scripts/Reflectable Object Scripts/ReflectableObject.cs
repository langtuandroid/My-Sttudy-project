using DG.Tweening;
using Interfaces;
using UnityEngine;

namespace Reflectable_Object_Scripts
{
    public class ReflectableObject : MonoBehaviour , IReflectable
    {
        public enum  ObjectType
        {
            Bounce,
            Static
        }

        [SerializeField] private ObjectType m_ObjectType;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (m_ObjectType == ObjectType.Bounce)
                {
                    Vector3 dir = (other.gameObject.transform.position - transform.position).normalized;
                    Vector3 punchOffset = dir + new Vector3(0.1f, 0.1f, 0.1f);

                    transform.DOPunchPosition(punchOffset, 0.5f);
                }
            }
        }
    }
}