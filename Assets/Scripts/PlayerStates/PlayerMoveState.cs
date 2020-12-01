using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private float currentVelocity;
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        currentVelocity = Mathf.Abs(player.CurrentVelocity.x);
        player.running.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        player.running.SetActive(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        calculateVelocity(); 
        player.CheckIfShouldFlip(xInput);
        player.SetVelocityX(currentVelocity * xInput);

        if(xInput == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        else if (jumpInput)
        {
            player.inputHandler.UseJumpInput();
            player.jump.GetComponent<AudioSource>().Play();
            player.SetVelocityX(player.FacingDirection * playerData.movementVelocity);
            player.SetVelocityY(playerData.jumpVelocity*gravityDirection);
            stateMachine.ChangeState(player.InAirState);
        }

        else if(!isGrounded)
        {
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // Calculates Velocity based on Acceleration
    private void calculateVelocity()
    {
        currentVelocity += Time.deltaTime * playerData.acceleration;
        currentVelocity = Mathf.Clamp(currentVelocity, 0, playerData.movementVelocity);
    }
}
