using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LSInputHandler : MonoBehaviour
{

    public Vector2 RawMovementInput { get; private set; }

    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }


    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }


    private void Start()
    {
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        if (Mathf.Abs(RawMovementInput.x) > 0.5f)
        {
            NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        }
        else
        {
            NormInputX = 0;
        }

        if (Mathf.Abs(RawMovementInput.y) > 0.5f)
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
        if (context.started)
        {

            JumpInput = true;
            JumpInputStop = false;


        }

        if (context.canceled)
        {
            JumpInputStop = true;
            JumpInput = false;
        }
    }

    public void UseJumpInput() => JumpInput = false;
}
