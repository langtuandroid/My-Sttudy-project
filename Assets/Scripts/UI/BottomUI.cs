using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BottomUI : MonoBehaviour
    {
        [SerializeField, InlineButton("GetOptions")] private List<OptionButton> m_OptionButtonList;
        [SerializeField] private Vector2 m_ButtonDefaultSize;
        [SerializeField] private float m_ActiveButtonXAxisExtrudeOffset;
        [SerializeField] private float m_OptionButtonTweenDuration;

        private float _screenWidthChangeFlag;
        private bool _extrudeTween;

        private void Awake()
        {
            int buttonIndex = 0;
            
            foreach (OptionButton optionButton in m_OptionButtonList)
            {
                optionButton.GetComponent<Button>().onClick.AddListener(optionButton.Activate);
                optionButton.m_Index = buttonIndex;
                buttonIndex++;
            }
        }
        private void Start()
        {
            foreach (OptionButton optionButton in m_OptionButtonList) optionButton.OnButtonClick += ReSetUI;
        }
        private void Update()
        {
            float screenWidth = Screen.width;
            if (System.Math.Abs(_screenWidthChangeFlag - screenWidth) > 0 )
            {
                _screenWidthChangeFlag = screenWidth;
                GetResponsive(screenWidth);
            }
        }
        
        private void ReSetUI(int activeButtonIndex)
        {
            for (int i = 0; i < m_OptionButtonList.Count; i++)
            {
                if(i == activeButtonIndex) continue;
                m_OptionButtonList[i].Deactivate();
            }
            float screenWidth = Screen.width;
            GetResponsive(screenWidth);
        }
        
        private void GetResponsive(float screenWidth)
        {
            int optionCount = m_OptionButtonList.Count;
            float defaultWidth = screenWidth / optionCount;
            float xPos = 0;
            
            float deActiveButtonWidth = (screenWidth -  (defaultWidth + m_ActiveButtonXAxisExtrudeOffset)) / (optionCount-1);
            
            foreach (OptionButton optionButton in m_OptionButtonList)
            {
                float width;
                
                if (optionButton.GetComponent<OptionButton>().m_IsActive) width = defaultWidth + m_ActiveButtonXAxisExtrudeOffset;
                else  width = deActiveButtonWidth;

                RectTransform buttonRectTransform = optionButton.GetComponent<RectTransform>();
                
                DOVirtual.Float(buttonRectTransform.sizeDelta.x, width, m_OptionButtonTweenDuration, delegate(float value)
                {
                    buttonRectTransform.sizeDelta = new Vector2(value, m_ButtonDefaultSize.y);
                });

                DOVirtual.Float(buttonRectTransform.anchoredPosition.x, xPos, m_OptionButtonTweenDuration, delegate(float value)
                {
                    buttonRectTransform.anchoredPosition = new Vector2(value, buttonRectTransform.anchoredPosition.y);
                });
                
                xPos += width;
            }
        }
        
        private void GetOptions()
        {
#if UNITY_EDITOR
            m_OptionButtonList = GetComponentsInChildren<OptionButton>().ToList();
#endif
        }
    }
}
