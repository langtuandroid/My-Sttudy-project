using UnityEngine;

namespace State_Machine
{
    public class AppleWholeState : AppleBaseState
    {
        public override void EnterState(AppleStateManager apple)
        {
            Debug.Log("Hello From The Apple Whole State.");

            Rigidbody rb = apple.GetComponent<Rigidbody>();
            rb.useGravity = true;
        }

        public override void UpdateState(AppleStateManager apple)
        {
            
        }

        public override void OnCollisionEnter(AppleStateManager apple)
        {
            
        }
    }
}