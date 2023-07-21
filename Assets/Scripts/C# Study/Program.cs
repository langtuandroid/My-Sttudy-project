using System;
using UnityEngine;
using UnityEngine.Events;

namespace C__Study
{
    public class Program : MonoBehaviour
    {
        public event EventHandler<MyEventsArgs> OnSpacePress;
        private int index;

        public UnityEvent m_MyUnityEvent;
        public class MyEventsArgs : EventArgs
        {
            public int Index;
        }
        
        private void Start()
        {
            index = 0;
            OnSpacePress += PressSpace;
        }

        private void PressSpace(object sender, MyEventsArgs e)
        {
            Debug.Log("Index : " + e.Index);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                index++;
                OnSpacePress?.Invoke(this, new MyEventsArgs{Index = index});
            }
        }

    }
    
}