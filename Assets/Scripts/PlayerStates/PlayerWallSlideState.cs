using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    private float yVelocity;
    private int wallJumpDirection;
    private bool jumped = true;
    private float timer;

    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        FlipPlayer();
        yVelocity = player.CurrentVelocity.y;
        player.SetVelocityX(0.0f);
        jumped = false;
        timer = 0.0f;
    }

    private void FlipPlayer()
    {
        if (isTouchingWall)
        {
            player.Flip();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CalculateYVelocity();
        player.SetVelocityY(yVelocity);

        if(!(isTouchingWall || isTouchingWallBack))
        {
            stateMachine.ChangeState(player.InAirState);
        }

        else if(xInput == 0)
        {
            timer = 0f;
            if (jumpInput && !jumped)
            {
                player.inputHandler.UseJumpInput();
                PerformWallJump();
                stateMachine.ChangeState(player.InAirState);
            }
        }
        
        else if(xInput == player.FacingDirection)
        {
            timer += Time.deltaTime;
            player.inputHandler.UseJumpInput();
            if(timer <= playerData.stickTime)
            {
                if (jumpInput && !jumped)
                {
                    PerformWallJump();
                    stateMachine.ChangeState(player.InAirState);
                }
            }
            else
            {
                ExitWall();
                stateMachine.ChangeState(player.InAirState);
            }
            
        }

        else if (xInput != player.FacingDirection) 
        {
            timer = 0f;
            if (jumpInput && !jumped)
            {
                player.inputHandler.UseJumpInput();
                PerformWallJumpSameDirection();
                stateMachine.ChangeState(player.InAirState);
            }
        }

        if (isGrounded)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void CalculateYVelocity()
    {
        yVelocity -= Time.deltaTime * playerData.wallSlideDeceleration*gravityDirection;
        if(Mathf.Abs(yVelocity) > playerData.maxSlideDownVelocity)
        {
            yVelocity = -playerData.maxSlideDownVelocity * gravityDirection;
        }
    }

    public void PerformWallJump()
    {
        Vector2 jumpDirection = new Vector2(playerData.wallJumpAngle.x, playerData.wallJumpAngle.y * gravityDirection);
        //DetermineWallJumpDirection(isTouchingWall);
        player.SetVelocity(playerData.wallJumpVelocity, jumpDirection , player.FacingDirection);
        //player.CheckIfShouldFlip(wallJumpDirection);
        jumped = true;
    }

    public void ExitWall()
    {
        player.SetVelocityX(playerData.wallSlideExitVelocity*player.FacingDirection);
        jumped = true;

    }

    public void PerformWallJumpSameDirection()
    {
        Vector2 jumpDirection = new Vector2(playerData.wallJumpAngleSameDir.x, playerData.wallJumpAngleSameDir.y * gravityDirection);
        player.SetVelocity(playerData.wallJumpVelocitySameDir, jumpDirection, player.FacingDirection);
        jumped = true;
    }
}
