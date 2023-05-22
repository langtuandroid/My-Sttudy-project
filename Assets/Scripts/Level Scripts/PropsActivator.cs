using UnityEngine;

namespace Level_Scripts
{
    public class PropsActivator : MonoBehaviour
    {
        [SerializeField] private GameObject m_StaticPotato;
        [SerializeField] private GameObject m_PringlesBottle;

        public void ActiveStaticPotato()
        {
            m_StaticPotato.SetActive(true);
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