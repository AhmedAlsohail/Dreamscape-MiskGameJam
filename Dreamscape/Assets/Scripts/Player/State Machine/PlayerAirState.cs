using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        player.Run(xInput, 0.4f);
        player.HandleGravity();

        if (Input.GetButtonDown("Fire") && (Time.time - player.lastTimeAttacked > player.cooldown)) // Make it GetKey() only if you want the player to combo using hold button not clicks. 
        {
            stateMachine.ChangeState(player.primaryAttack);
        }

        if (player.IsGrounded())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}