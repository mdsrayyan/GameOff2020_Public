using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    
    private bool coyoteTime;
    private bool isJumping;
    private Vector2 startVelocity;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        startVelocity = player.CurrentVelocity;
    }

    public override void Enter()
    {
        base.Enter();
        isJumping = true;
        StartCoyoteTime();
        if(Mathf.Abs(player.CurrentVelocity.y) < 0.000001)
        {

            player.SetVelocityY(-playerData.gravityVelocity * gravityDirection);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.landing.GetComponent<AudioSource>().Play();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();

        CheckJumpMultiplier();

        if (isGrounded && Mathf.Abs(player.CurrentVelocity.y) < 0.01f)
        {
            if(player.CurrentVelocity.x == 0)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                stateMachine.ChangeState(player.MoveState);
            }
            isJumping = false;
        }
        else if((isTouchingWall || isTouchingWallBack) && Mathf.Abs(player.CurrentVelocity.x) <= 0.05f)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else if (jumpInput && coyoteTime)
        {
            player.inputHandler.UseJumpInput();
            player.SetVelocityY(playerData.jumpVelocity * gravityDirection);
            stateMachine.ChangeState(player.InAirState);
        }
        else if(xInput != 0)
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(SetXVelocity());
            //player.Anim.SetFloat("YVelocity", player.CurrentVelocity.y);
            //player.Anim.SetFloat("XVelocity", Mathf.Abs(player.CurrentVelocity.x));
        }
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }

            else if (player.CurrentVelocity.y*gravityDirection <= 0f)
            {
                isJumping = false;
            }
        }
    }

    private float SetXVelocity()
    {
        if((Time.time-startTime) > playerData.coolDownTime)
        {
            if (Mathf.Abs(startVelocity.x) < 0.0001)
            {
                return xInput * playerData.movementVelocity * playerData.onSpotMultiplier;
            }
            else
            {
                //if (xInput == 0)
                //{
                //    Debug.Log("detected x inp zero");
                //    return startVelocity.x;
                //}
                //else
                {
                    if (Mathf.Sign(startVelocity.x) == Mathf.Sign(xInput))
                    {
                        return startVelocity.x;
                    }
                    if (Mathf.Sign(startVelocity.x) != Mathf.Sign(xInput))
                    {
                        return xInput * playerData.movementVelocity * playerData.reverseMultiplier;
                    }
                }
            }
        }

        return startVelocity.x;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time >startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;

    public void SetIsJumping() => isJumping = true;
}
