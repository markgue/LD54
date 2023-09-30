using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

//This class listens for movement input commands and moves the player accordingly
public class FighterMovement : MonoBehaviour
{
    [SerializeField] float moveForce;
    Rigidbody rb;
    Vector3 horizontalMovement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.AddForce(horizontalMovement);
    }

    public void SetMovement(CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        horizontalMovement = (Quaternion.Euler(90, 0, 0) * value * moveForce);
    }
}
