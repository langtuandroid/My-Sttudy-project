using DG.Tweening;
using UnityEngine;

namespace Level_Scripts
{
    public class PropsActivator : MonoBehaviour
    {
        [SerializeField] private GameObject m_StaticPotato;
        [SerializeField] private Transform m_StaticPotatoCuttingPoint;
        
        [SerializeField] private GameObject m_PringlesBottle;

        public void ActiveStaticPotato(Vector3 pos)
        {
            m_StaticPotato.transform.position = pos;
            m_StaticPotato.SetActive(true);

            m_StaticPotato.transform.DOMove(m_StaticPotatoCuttingPoint.position, 0.5f).SetEase(Ease.Linear);
        }
        public void HideStaticPotato()
        {
            m_StaticPotato.SetActive(false);
        }
        
        public void ActivePringlesBottle()
        {
            m_PringlesBottle.SetActive(true);
        }
        public void HidePringlesBottle()
        {
            m_PringlesBottle.SetActive(false);
        }
    }
}