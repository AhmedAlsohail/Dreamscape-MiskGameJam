using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerHopeEnergy : PlayerState
{
    public GameObject projectilePrefab; // Prefab to instantiate
    public float spawnRate = 0.1f; // Time in seconds between spawns
    public float rotationIncrement = 25f; // Degrees to rotate each new object

    private float timer;
    private float currentRotation;
    private float timAttacked;


    public PlayerHopeEnergy(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.lastTimeAttacked = Time.time;

        timer = spawnRate; // Initialize timer
        currentRotation = Random.Range(0,360); // Initialize rotation
        string prefabPath = "Assets/Prefabs/Projectiles/HopeProjectile.prefab";
        projectilePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

    }

    public override void Exist()
    {
        base.Exist();

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
            player.Run(xInput, 0.5f);
        }

        if (Input.GetButton("Fire")) // If the Fire button is pressed
        {
            timer -= Time.deltaTime; // Decrease the timer by the elapsed time

            if (timer <= 0)
            {
                // Reset the timer
                timer = spawnRate;

                // Instantiate the prefab at the spawner's position
                GameObject obj = GameObject.Instantiate(projectilePrefab, player.transform.position, Quaternion.Euler(0, 0, currentRotation));

                // Increment the rotation for the next object
                currentRotation += rotationIncrement;
            }
        }
        else
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
}
