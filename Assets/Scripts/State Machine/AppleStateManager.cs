using UnityEngine;

namespace State_Machine
{
    public class AppleStateManager : MonoBehaviour
    {
        private AppleBaseState _currenState;
        public AppleGrowingState GrowingState;
        public AppleWholeState WholeState;
        
        private void Start()
        {
            GrowingState = new AppleGrowingState();
            WholeState = new AppleWholeState();
            
            _currenState = GrowingState;
            _currenState.EnterState(this);
        }
        
        private void Update()
        {
            _currenState.UpdateState(this);
        }

        public void SwitchState(AppleBaseState state)
        {
            _currenState = state;
            state.EnterState(this);
        }
        
        private void LogController()
        {
#if  UNITY_EDITOR
            Debug.unityLogger.logEnabled = true;
#else
            Debug.unityLogger.logEnabled = false;
#endif
        }
        
    }
}