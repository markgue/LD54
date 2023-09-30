using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

//This class listens for movement input commands and moves the player accordingly
public class FighterMovement : MonoBehaviour
{
    [SerializeField] float moveForce;
    [SerializeField] float jumpForce;
    [SerializeField] float dashForce;
    [SerializeField] float airMobility;
    Rigidbody rb;
    Vector3 horizontalMovement; //the direction the fighter is trying to move right now due to inputs
    Vector3 latestMovement;     //latest non-zero direction vector inputted
    Vector3 jumpVector;         //vector applied to make this fighter jump
    float airMod = 1f;
    bool airborne;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        jumpVector = new Vector3(0f, jumpForce, 0f);
        latestMovement = new Vector3(0f, 0f, 0f);
        SetAirborne(true); //because you start in the air rn
    }

    private void FixedUpdate()
    {
        rb.AddForce(horizontalMovement * airMod);
    }

    public void SetMovement(CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        horizontalMovement = (Quaternion.Euler(90, 0, 0) * value * moveForce);
        if (!horizontalMovement.Equals(Vector3.zero))
            latestMovement = horizontalMovement;
    }

    public void Jump(CallbackContext context)
    {
        if (context.performed && !airborne)
        {
            rb.AddForce(jumpVector);
        }
    }

    public void Dash(CallbackContext context)
    {
        if (context.performed)
        {
            rb.AddForce(latestMovement.normalized * dashForce);
        }
    }

    private void SetAirborne(bool b)
    {
        airborne = b;
        if (b)
            airMod = airMobility;
        else
            airMod = 1f;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.material.name.StartsWith("Tile"))
            SetAirborne(false);
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.collider.material.name.StartsWith("Tile"))
            SetAirborne(true);
    }
}
