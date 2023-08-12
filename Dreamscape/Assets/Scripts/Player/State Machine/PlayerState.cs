using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    protected float xInput;
    private string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    // Every time the state changes
    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        triggerCalled = false;
        Debug.Log($"I am currently at {animBoolName}");
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        player.anim.SetFloat("yVelocity", player.velocity.y);
        player.anim.SetFloat("moveSpd", player.velocity.x);
    }

    public virtual void Exist()
    {
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
