using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;

    private string animBoolName;


    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingWallBack;
    protected int gravityDirection;

    protected int xInput;
    protected bool jumpInput;
    protected bool jumpInputStop;
    protected bool gravityInput;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.playerData = playerData;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        player.CreateDust();
        DoChecks();
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
        gravityDirection = (int)Mathf.Sign(player.GetComponent<Rigidbody2D>().gravityScale);
    }

    public virtual void Exit()
    {
        player.CreateDust();
        player.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {
        xInput = player.inputHandler.NormInputX;
        jumpInput = player.inputHandler.JumpInput;
        jumpInputStop = player.inputHandler.JumpInputStop;
        gravityInput = player.inputHandler.GravityInput;
        gravityDirection = (int)Mathf.Sign(player.GetComponent<Rigidbody2D>().gravityScale);
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
        if(gravityInput && gravityDirection != -1)
        {
            player.ReverseGravity();
        }
    }

    public virtual void DoChecks()
    {
        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingWallBack = player.CheckIfTouchingWallBack();
    }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
