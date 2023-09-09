using UnityEngine;

namespace Player.StateMachine
{
    public class PlayerStateManager : MonoBehaviour
    {
        public Animator m_Animator;
        public float m_WorryIdleDuration;
        
        private PlayerBaseState _currentState;

        public PlayerIdleState PlayerIdleState;
        public PlayerWorryIdleState PlayerWorryIdleState;
        public PlayerAction1State PlayerAction1State;
        public PlayerAction2State PlayerAction2State;

        private void Start()
        {

            PlayerIdleState = new PlayerIdleState();
            PlayerWorryIdleState = new PlayerWorryIdleState();
            PlayerAction1State = new PlayerAction1State();
            PlayerAction2State = new PlayerAction2State();
            
            _currentState = PlayerIdleState;
            _currentState.EnterState(this);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                _currentState = PlayerWorryIdleState;
                _currentState.EnterState(this);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                _currentState = PlayerAction1State;
                _currentState.EnterState(this);
            }
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                _currentState = PlayerAction2State;
                _currentState.EnterState(this);
            }
        }
    }
}