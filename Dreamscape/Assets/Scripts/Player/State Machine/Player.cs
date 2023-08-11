/*
 *  The main controller for the player which controls the state machine & the whole movements and attack of the player.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public BoxCollider2D collider { get; private set; }

    public RuntimeAnimatorController[] anims;
    public Animator anim { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }

    public PlayerState primaryAttack { get; private set; } // Type PlayerState because the state type will be changing for each weapon

    //public PlayerDashState dashState { get; private set; }
    //public PlayerSlideState slideState { get; private set; }

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        //dashState = new PlayerDashState(this, stateMachine, "Dash");
        //slideState = new PlayerSlideState(this, stateMachine, "Slide");

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
        switch (newWeapon)
        {
            case 0:
                primaryAttack = new PlayerWisdomStaff(this, stateMachine, "Attack");
                break;
            case 1:
                primaryAttack = new PlayerHopeEnergy(this, stateMachine, "Attack");
                break;
            case 2:
                primaryAttack = new PlayerBraveShout(this, stateMachine, "Attack");
                break;
        }

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
