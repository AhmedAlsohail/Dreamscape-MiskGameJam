using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (Input.GetButtonDown("Fire")) // Make it GetKey() only if you want the player to combo using hold button not clicks. 
        {
            stateMachine.ChangeState(player.primaryAttack);
        }

        if (!player.IsGrounded())
        {
            stateMachine.ChangeState(player.jumpState);
        }

        if (Input.GetButtonDown("Jump") && !player.IsGrounded())
        {
            stateMachine.ChangeState(player.jumpState);
        }

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    stateMachine.ChangeState(player.slideState);
        //}
    }
}
