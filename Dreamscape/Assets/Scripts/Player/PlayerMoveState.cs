using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{

    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exist()
    {
        base.Exist();
    }

    public override void Update()
    {
        base.Update();

        player.Run(xInput, 1);
        player.HandleGravity();
        player.HandleJump();

        if (xInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

}
