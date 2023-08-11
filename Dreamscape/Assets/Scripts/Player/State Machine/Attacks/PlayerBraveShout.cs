using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBraveShout : PlayerState
{
    private float timeAttacked;
    private float lastTimeAttacked;

    public GameObject projectilePrefab; // Prefab to instantiate
    public float scaleRate = 0.1f; // Rate of scaling per second

    public PlayerBraveShout(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        string prefabPath = "Assets/Prefabs/Projectiles/BraveShoutProjectile.prefab";
        projectilePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        timeAttacked = Time.time;

        Shout();
    }

    public override void Exist()
    {
        base.Exist();
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        player.HandleGravity();
        player.HandleJump();
        if (!player.IsGrounded() && player.velocity.y < 0)
        {
            player.Run(xInput, 0.4f);
        }
        else
        {
            player.Run(xInput, 1f);
        }

        if (Time.time - timeAttacked > 1f)
        {
            if (xInput == 0)
            {
                player.ResetVelocityX();
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.moveState);
            }
        }
    }

    private void Shout()
    {
        GameObject obj = GameObject.Instantiate(projectilePrefab, player.transform.position, Quaternion.identity);
        obj.GetComponent<BraveShoutProjectile>().facingDir = player.facingDir;
    }
}