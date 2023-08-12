/*
 *  The main controller for the player which controls the state machine & the whole movements and attack of the player.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{
    public PlayerController controller;

    private float accelerationTimeAirborne = .2f;
    private float accelerationTimeGrounded = .1f;
    private float currentMoveSpeed;

    public int facingDir { get; private set; } = 1;
    public bool isFacingRight = true;

    public bool isBusy { get; private set; }


    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;

    [Header("Physics")]
    private float gravity;
    private float jumpVelocity;
    private float velocityXSmoothing;
    public Vector3 velocity;
    public float slope;
    public Vector2 slopeNormalPerp;
    public float slopeDownAngle;

    public bool isGrounded;

    private int currentWeapon = -1;
    [Header("Cooldown")]
    public float lastTimeAttacked;
    public float cooldown;
    public float[] cds;
    public TextMeshProUGUI cdText;

    public BoxCollider2D collider { get; private set; }

    public RuntimeAnimatorController[] anims;
    public Animator anim { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }

    public PlayerState primaryAttack { get; private set; } // Type PlayerState because the state type will be changing for each weapon


    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");

        primaryAttack = new PlayerBraveShout(this, stateMachine, "Attack");
    }
    private void Start()
    {
        controller = GetComponent<PlayerController>();
        anim = GetComponentInChildren<Animator>();
        collider = GetComponentInChildren<BoxCollider2D>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        currentMoveSpeed = 10f;
        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        stateMachine.currentState.Update();
        controller.Move(velocity * Time.deltaTime);
        handleUI();

    }

    public void HandleGravity()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        //  calling y twice once before and other after Move() ,I recommend to use this method every time you have to calculate acceleration/deceleration. src: https://github.com/SebLague/2DPlatformer-Tutorial/issues/19
        velocity.y += gravity * Time.deltaTime;
    }

    public void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            velocity.y = jumpVelocity;
        }
    }

    public void Run(float xInput, float speedPercent)
    {
        // 2 lines below changed, src: https://github.com/SebLague/2DPlatformer-Tutorial/issues/3
        float targetVelocityX = xInput * currentMoveSpeed * speedPercent;
        //velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.x = targetVelocityX == 0 ? targetVelocityX : Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        FlipController(velocity.x);
    }

    public void Flip()
    {
        if (isFacingRight)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false; // We want to flip when not facing right.
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true; // We want to flip when not facing right.
        }

    }

    public void FlipController(float _x)
    {
        if (_x > 0 && !isFacingRight)
        {
            facingDir = 1;
            isFacingRight = !isFacingRight;
        }
        else if (_x < 0 && isFacingRight)
        {
            facingDir = -1;
            isFacingRight = !isFacingRight;
        }

        Flip();
    }

    public void ResetVelocityX()
    {
        velocity.x = 0;
    }

    public void ResetVelocityY()
    {
        velocity.y = 0;
    }

    public void switchWeapon(int newWeapon)
    {
        if(newWeapon != currentWeapon)
        {
            currentWeapon = newWeapon;
        }
        else
        {
            currentWeapon = (currentWeapon + 1) % 3;
        }

        switch (currentWeapon)
        {
            case 0:
                primaryAttack = new PlayerWisdomStaff(this, stateMachine, "Attack");
                cooldown = 0.5f;
                break;
            case 1:
                primaryAttack = new PlayerHopeEnergy(this, stateMachine, "Attack");
                cooldown = 0f;
                break;
            case 2:
                primaryAttack = new PlayerBraveShout(this, stateMachine, "Attack");
                cooldown = 5f;
                break;
        }

        // Reset Cooldown.
        lastTimeAttacked = Time.time - 100f;

        anim.SetFloat("Weapon", currentWeapon);
        stateMachine.ChangeState(idleState);
    }
    public bool IsGrounded()
    {
        if (isGrounded)
            return true;

        if (controller.collisions.below)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void handleUI()
    {
        float cd = (cooldown + (lastTimeAttacked - Time.time));
        if(cd <= 0)
        {
            cdText.fontSize = 14;
            switch (currentWeapon)
            {
                case 0:
                    cdText.text = "Wisdom";
                    break;
                case 1:
                    cdText.text = "Hope";
                    break;
                case 2:
                    cdText.text ="Bravery";
                    break;
            }
        }
        else
        {
            cdText.fontSize = 24;
            cdText.text = cd.ToString("F2");
        }

       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = false;
    }
}
