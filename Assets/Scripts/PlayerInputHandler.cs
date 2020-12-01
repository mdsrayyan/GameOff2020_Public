using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }

    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }

    public int CameraInputY { get; private set; }

    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }

    public bool GravityInput { get; private set; }

    public bool RewindInput { get; private set; }

    private LevelManager levelManager;

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        if(Mathf.Abs(RawMovementInput.x) > 0.5f && levelManager.levelStatus != LevelManager.Status.Won)
        {
            NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        }
        else
        {
            NormInputX = 0;
        }

        if (Mathf.Abs(RawMovementInput.y) > 0.5f && levelManager.levelStatus != LevelManager.Status.Won)
        {
            NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
        }
        else
        {
            NormInputY = 0;
        }
        
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.started && levelManager.levelStatus != LevelManager.Status.Won)
        {
            if(levelManager == null)
            {
                JumpInput = true;
                JumpInputStop = false;
                jumpInputStartTime = Time.time;
            }

            else if(!(levelManager.levelStatus == LevelManager.Status.Paused))
            {
                JumpInput = true;
                JumpInputStop = false;
                jumpInputStartTime = Time.time;
            }

        }

        if(context.canceled)
        {
            JumpInputStop = true;
            JumpInput = false;
        }
    }

    public void OnGravityInput(InputAction.CallbackContext context)
    {
        if(context.started && levelManager.levelStatus != LevelManager.Status.Won)
        {
            GravityInput = true;
        }

        if(context.canceled && levelManager.levelStatus != LevelManager.Status.Won)
        {
            GravityInput = false;
        }
    }

    public void OnRewindInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            RewindInput = true;
        }

        if (context.canceled)
        {
            RewindInput = false;
        }
    }

    public void OnCameraUpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CameraInputY = 1;
        }

        if (context.canceled)
        {
            CameraInputY = 0;
        }
    }

    public void OnCameraDownInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CameraInputY = -1;
        }

        if (context.canceled)
        {
            CameraInputY = 0;
        }
    }

    public void UseJumpInput() => JumpInput = false;

    public void UseGravityInput() => GravityInput = false;

    public void UseRewindInput() => RewindInput = false;

}
