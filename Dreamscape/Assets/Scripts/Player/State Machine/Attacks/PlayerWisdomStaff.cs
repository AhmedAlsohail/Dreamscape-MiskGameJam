using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerWisdomStaff : PlayerState
{
    private float timeAttacked;
    private float lastTimeAttacked;

    public float offset;

    public Transform shootPoint;
    public GameObject projectilePrefab;
    public float startTimeBtwShoots;
    private float timeBtwShoots;

    public GameObject arm;
    private Quaternion shootDirection;

    //is first special on
    public bool isFSon;
    private float StopAnimationsCounter = 0.5f;
    public PlayerWisdomStaff(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.lastTimeAttacked = Time.time;

        // Assign needed variables
        shootPoint = player.transform;

        // Porjectile Prefab
        projectilePrefab = player.wisdomPrefab;

        offset = 90f;
        startTimeBtwShoots = 1f;
        shootDirection = Quaternion.Euler(0f, 0f, 0f);

        isFSon = false;

        timeAttacked = Time.time;

        shoot();
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
            player.Run(xInput, 1f);
        }

        if (Time.time - timeAttacked > 0.25f)
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

    private void shoot()
    {
        if (timeBtwShoots <= 0)
        {
            if (Input.GetMouseButtonDown(0)) // Check If mouse
            {
                setToMousePosition();
            }
            else // else it is controller
            {
                setToControllerAim();
            }

            GameObject.Instantiate(projectilePrefab, shootPoint.position, shootDirection);

        }
        else
        {
            timeBtwShoots -= Time.deltaTime;
        }
    }

    private void StopShootingAnimation()
    {
        if (isFSon && StopAnimationsCounter <= 0)
        {
            player.gameObject.transform.parent.GetComponent<Animator>().SetFloat("isShooting", 0);
            arm.SetActive(false);
            isFSon = false;
        }
        else if (isFSon)
        {
            StopAnimationsCounter -= Time.deltaTime;
        }
    }

    private void setToMousePosition()
    {
        Vector3 diffrence = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        float rotZ = Mathf.Atan2(diffrence.y, diffrence.x) * Mathf.Rad2Deg;
        shootDirection = Quaternion.Euler(0f, 0f, rotZ - offset);
    }

    private void setToControllerAim()
    {
        float aimHorizontal = Input.GetAxis("AimHorizontal");
        float aimVertical = Input.GetAxis("AimVertical");

        Vector3 aimDirection = new Vector3(aimHorizontal, aimVertical, 0f);
        if (aimDirection.magnitude > 0.1f)  // Add a deadzone to avoid accidental aiming
        {
            float rotZ = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            shootDirection = Quaternion.Euler(0f, 0f, rotZ + offset);
        }
    }
}
