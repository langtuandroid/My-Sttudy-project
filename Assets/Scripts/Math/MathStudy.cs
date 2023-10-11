using Sirenix.OdinInspector;
using UnityEngine;

namespace Math
{
    public class MathStudy : MonoBehaviour
    {
        
        [ButtonGroup]
        private void IDVectorDirection(float value)
        {
            if (value == 0f)
            {
                Debug.Log("Sign Input can't be ZERO!!");
                return;
            }
            Debug.Log("Direction using Sign = " + Mathf.Sign(value));
        }
    }
}