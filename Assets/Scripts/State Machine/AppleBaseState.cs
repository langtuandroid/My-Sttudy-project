using UnityEngine;

namespace State_Machine
{
    public abstract class AppleBaseState
    {
       public abstract void EnterState(AppleStateManager apple);
       public abstract void UpdateState(AppleStateManager apple);
       public abstract void OnCollisionEnter(AppleStateManager apple, Collision collision);
    }
}