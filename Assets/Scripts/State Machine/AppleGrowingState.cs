using UnityEngine;

namespace State_Machine
{
    public class AppleGrowingState : AppleBaseState
    {
        private readonly Vector3 _appleStartSize = new Vector3(0.1f, 0.1f, 0.1f);
        private readonly Vector3 _appleGrownSize = new Vector3(1,1f,1f);
        
        public override void EnterState(AppleStateManager apple)
        {
            Debug.Log("Hello From The Apple Growing State.");

            apple.transform.localScale = _appleStartSize;
        }

        public override void UpdateState(AppleStateManager apple)
        {
            Debug.Log("Apple Is Growing");

            if (apple.transform.localScale.x < _appleGrownSize.x)
            {
                apple.transform.localScale += _appleGrownSize * Time.deltaTime;
            }
            else
            {
                Debug.Log("Next State From Apple Growing State.");
                apple.SwitchState(apple.WholeState);
            }
        }

        public override void OnCollisionEnter(AppleStateManager apple, Collision collision)
        {
            
        }
    }
}