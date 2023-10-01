using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

//interfaces with player input to send commands to the FighterMovement class
public class FighterInput : MonoBehaviour
{
    FighterMovement fighter;

    private void Awake()
    {
        fighter = GetComponent<FighterMovement>();
    }

    public void CallSetMovement(CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        Vector3 submit = Quaternion.Euler(90, 0, 0) * value;
        fighter.SetMovement(submit);
    }

    public void CallJump(CallbackContext context)
    {
        if (context.performed)
            fighter.Jump();
    }

    public void CallDash(CallbackContext context)
    {
        if (context.performed)
            fighter.Dash();
    }
}
