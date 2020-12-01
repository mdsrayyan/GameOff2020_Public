using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPlayerData", menuName ="Data/Player Data/ Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10.0f;
    public float acceleration = 20.0f;
    public float timeToMaxSpeed = 0.2f;
    public float gravityScale = 2.0f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int numberOfJumps = 1;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float reverseMultiplier = 0.2f;
    public float onSpotMultiplier = 0.4f;
    public float coolDownTime = 0.2f;
    public float gravityMultiplier = 1.5f;


    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3.0f;
    public float wallSlideDeceleration = 10.0f;
    public float wallSlideExitVelocity = 5.0f;
    public float maxSlideDownVelocity = 10.0f;
    public float stickTime = 0.2f;

    [Header("Wall Climb State")]
    public float wallClimbvelocity = 3.0f;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);
    public Vector2 wallJumpAngleSameDir = new Vector2(2, 1);
    public float wallJumpVelocitySameDir = 10f;


    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;

    [Header("New Player Data")]
    public float maxVelocity = 10.0f;

    [Header("Flip Gravity State")]
    public float flipGravityTime = 1.0f;
    public float gravityCoolDown = 2.0f;
    public float gravityVelocity = 20.0f;

}
