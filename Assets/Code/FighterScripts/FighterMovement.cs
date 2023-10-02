using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

//This class listens for movement input commands and moves the player accordingly
public class FighterMovement : MonoBehaviour
{
    [SerializeField] float moveForce;
    [SerializeField] float jumpForce;
    [SerializeField] float dashForce;
    [SerializeField] float airMobility;
    [SerializeField] DirectionIndicator dir;
    Rigidbody rb;
    Vector3 horizontalMovement; //the direction the fighter is trying to move right now due to inputs
    Vector3 latestMovement;     //latest non-zero direction vector inputted
    Vector3 jumpVector;         //vector applied to make this fighter jump
    float airMod = 1f;
    bool jumping;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        jumpVector = new Vector3(0f, jumpForce, 0f);
        latestMovement = new Vector3(0f, 0f, 0f);
        SetJumping(true); //because you start in the air rn
    }

    private void FixedUpdate()
    {
        rb.AddForce(horizontalMovement * airMod);
    }

    public void SetMovement(Vector3 mov)
    {
        horizontalMovement = mov * moveForce;
        if (!horizontalMovement.Equals(Vector3.zero))
        {
            latestMovement = horizontalMovement;
            dir.SetDirection(latestMovement);
        }
    }

    public void Jump()
    {
        if (!jumping)
        {
            SetJumping(true);
            rb.AddForce(jumpVector);
        }
    }

    public void Dash()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(latestMovement.normalized * dashForce);
    }

    public bool IsJumping()
    {
        return jumping;
    }

    private void SetJumping(bool b)
    {
        jumping = b;
        if (b)
            airMod = airMobility;
        else
            airMod = 1f;
    }

    public void OnCollisionEnter(Collision collision)
    {
        HexTile ht = collision.collider.GetComponentInParent<HexTile>();
        if (ht != null && jumping)
        {
            SetJumping(false);
            ht.DamageTile(1);
        }
    }
}
