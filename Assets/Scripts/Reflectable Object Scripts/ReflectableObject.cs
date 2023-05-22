using DG.Tweening;
using Interfaces;
using Level_Scripts;
using UnityEngine;

namespace Reflectable_Object_Scripts
{
    public class ReflectableObject : MonoBehaviour , IReflectable
    {
        public enum  ObjectType
        {
            Bounce,
            Static,
            BanishCollider
        }

        [SerializeField] private ObjectType m_ObjectType;

        private Tween _bouncingTween;
        
        private Vector3 _initialPosition;

        private void Start()
        {
            _initialPosition = gameObject.transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                
                if (m_ObjectType == ObjectType.Bounce)
                {
                    if (_bouncingTween.IsActive())
                    {
                        _bouncingTween.Kill();
                        gameObject.transform.position = _initialPosition;
                    }

                    Vector3 dir = (other.gameObject.transform.position - transform.position).normalized;
                    Vector3 punchOffset = dir + new Vector3(0.1f, 0.1f, 0.1f);

                    _bouncingTween = transform.DOPunchPosition(punchOffset, 0.4f);

                }
                else if (m_ObjectType == ObjectType.BanishCollider && other.gameObject.CompareTag("Player"))
                {
                    other.transform.DOKill();
                    Destroy(other.gameObject);

                    CuttingKnifeController cuttingKnifeController = GetComponent<CuttingKnifeController>();
                    if (cuttingKnifeController != null)
                    {
                        cuttingKnifeController.KnifeCut();
                    }
                }
            }
        }
    }
}