using Sirenix.OdinInspector;
using UnityEngine;

namespace Level_Scripts
{
    public class CuttingKnifeController : MonoBehaviour
    {
        [SerializeField] private Animator m_Animator;
        private static readonly int Cutting = Animator.StringToHash("Cutting");

        [Button]
        public void KnifeCut()
        {
            m_Animator.SetTrigger(Cutting);
        }
    }
}