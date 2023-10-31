﻿
using UnityEngine;

namespace FK_3.Player.StateMachine
{
    public class PlayerGroundState : PlayerBaseState
    {
        public PlayerGroundState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }

        public override void EnterState()
        {
        }

        public override void UpdateState()
        {
            CheckSwitchSate();
        }

        public override void ExitState()
        {
           
        }

        public override void CheckSwitchSate()
        {
            if (ctx.IsJumpPressed)
            {
                SwitchState(factory.Jump());
            }
        }

        public override void InitializeSubState()
        {
           
        }
    }
}