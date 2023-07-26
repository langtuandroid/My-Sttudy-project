using System;
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

        private void Start()
        {
             Init();
        }

        private void Init()
        {
            SelectedOptionButtonExtrude(m_OptionButtonList[0], 0);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                const int index = 3;
                SelectedOptionButtonExtrude(m_OptionButtonList[index], index);
            }
        }
        
        // private void BothSideExtrude()
        // {
        //     _buttonRectTransform.pivot = new Vector2(0.5f, 0.5f);
        //     
        //     DOVirtual.Float(250f, m_ActiveButtonWidthExtrudeSize, m_ExtrudeDuration, delegate(float value)
        //     {
        //         _buttonRectTransform.sizeDelta = new Vector2(value, 250f);
        //     });
        // }
        // private void LeftSideExtrude()
        // {
        //     _buttonRectTransform.anchoredPosition = new Vector2(0f, _buttonRectTransform.anchoredPosition.y);
        //     
        //     _buttonRectTransform.pivot = new Vector2(0f, 0.5f);
        //     
        //     DOVirtual.Float(250f, m_ActiveButtonWidthExtrudeSize, m_ExtrudeDuration, delegate(float value)
        //     {
        //         _buttonRectTransform.sizeDelta = new Vector2(value, 250f);
        //     });
        // }
        // private void RightSideExtrude()
        // {
        //     _buttonRectTransform.anchoredPosition = new Vector2(250f, _buttonRectTransform.anchoredPosition.y);
        //     
        //     _buttonRectTransform.pivot = new Vector2(1f, 0.5f);
        //     
        //     DOVirtual.Float(250f, m_ActiveButtonWidthExtrudeSize, m_ExtrudeDuration, delegate(float value)
        //     {
        //         _buttonRectTransform.sizeDelta = new Vector2(value, 250f);
        //     });
        // }
        
        private void SelectedOptionButtonExtrude(OptionButton selectedOptionButton, int index)
        {
            RectTransform buttonRectTransform = selectedOptionButton.GetComponent<RectTransform>();
            
            if (index == 0)
            {
                buttonRectTransform.anchoredPosition = new Vector2(0f, buttonRectTransform.anchoredPosition.y);
                buttonRectTransform.pivot = new Vector2(0f, 0.5f);
            }
            else if(index == m_OptionButtonList.Count-1)
            {
                buttonRectTransform.anchoredPosition = new Vector2(250f, buttonRectTransform.anchoredPosition.y);
                buttonRectTransform.pivot = new Vector2(1f, 0.5f);
            }
            else buttonRectTransform.pivot = new Vector2(0.5f, 0.5f);
            
            
            DOVirtual.Float(250f, m_ActiveButtonWidthExtrudeSize, m_ExtrudeDuration, delegate(float value)
            {
                buttonRectTransform.sizeDelta = new Vector2(value, 250f);
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
