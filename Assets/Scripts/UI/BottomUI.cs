using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    public class BottomUI : MonoBehaviour
    {
        [SerializeField, InlineButton("GetOptions")] private List<OptionButton> m_OptionButtonList;
        [SerializeField] private Vector2 m_ButtonHeightAndWidth;
        [SerializeField] private float m_ActiveButtonWidthExtrudeSize;
        [SerializeField] private float m_ExtrudeDuration;
        
        private RectTransform _buttonRectTransform;

        private void Start()
        {
            _buttonRectTransform = m_OptionButtonList[0].GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                BothSideExtrude();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                LeftSideExtrude();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                RightSideExtrude();
            }
        }


        private void BothSideExtrude()
        {
            DOVirtual.Float(250f, m_ActiveButtonWidthExtrudeSize, m_ExtrudeDuration, delegate(float value)
            {
                _buttonRectTransform.sizeDelta = new Vector2(value, 250f);
            });
        }
        private void LeftSideExtrude()
        {
            DOVirtual.Float(250f, m_ActiveButtonWidthExtrudeSize, m_ExtrudeDuration, delegate(float value)
            {
                Debug.Log("Not Implemented yet!");
            });
        }
        private void RightSideExtrude()
        {
            DOVirtual.Float(250f, m_ActiveButtonWidthExtrudeSize, m_ExtrudeDuration, delegate(float value)
            {
                Debug.Log("Not Implemented yet!");
            });
        }
        
        
        private void GetOptions()
        {
#if UNITY_EDITOR
            m_OptionButtonList = GetComponentsInChildren<OptionButton>().ToList();
#endif
        }
    }
}
