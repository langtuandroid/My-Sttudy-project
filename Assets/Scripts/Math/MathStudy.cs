using UnityEngine;

namespace Math
{
    public class MathStudy : MonoBehaviour
    {
        [SerializeField] private float m_SignXInput;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                //-------------1D Vector Direction (Will be -1 or 1)---------------
                if (m_SignXInput == 0f)
                {
                    Debug.Log("Sign Input can't be ZERO!!");
                    return;
                }
                Debug.Log("Direction using Sign = " + Mathf.Sign(m_SignXInput));
                //----------------------------------------------------------------
            }
        }
    }
}