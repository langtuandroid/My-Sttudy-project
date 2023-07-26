using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    public class BottomUI : MonoBehaviour
    {
        [SerializeField, InlineButton("GetOptions")] private List<OptionButton> m_OptionButtonList;
        [SerializeField] private Vector2 m_ButtonDefaultSize;

        private float _screenWidthChangeFlag;

        private void Update()
        {
            float screenWidth = Screen.width;
            
            if (System.Math.Abs(_screenWidthChangeFlag - screenWidth) > 0 )
            {
                _screenWidthChangeFlag = screenWidth;
                GetResponsive(screenWidth);
            }
        }
        
        private void GetResponsive(float screenWidth)
        {
            
            int optionCount = m_OptionButtonList.Count;
            float width = screenWidth / optionCount;
            float xPos = 0;
            
            foreach (var buttonRectTransform in m_OptionButtonList.Select(optionButton => optionButton.GetComponent<RectTransform>()))
            {
                buttonRectTransform.sizeDelta = new Vector2(width, m_ButtonDefaultSize.y);

                buttonRectTransform.anchoredPosition = new Vector2(xPos, buttonRectTransform.anchoredPosition.y);
                
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
