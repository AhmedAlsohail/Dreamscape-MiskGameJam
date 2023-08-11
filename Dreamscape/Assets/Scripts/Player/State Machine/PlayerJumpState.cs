using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (Input.GetKeyDown(KeyCode.Mouse0)) // Make it GetKey() only if you want the player to combo using hold button not clicks. 
        {
            stateMachine.ChangeState(player.primaryAttack);
        }

        if (player.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}
