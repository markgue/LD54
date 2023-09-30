using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This bound is meant to tell when a fighter has fallen off the arena into the out of bounds area
public class OutOfBounds : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FighterMovement f = other.transform.parent.GetComponent<FighterMovement>(); //TODO: switch this to Fighter script
        if (f != null)
            Debug.Log("someone fell out of bounds"); //TODO: reset game / go to next round
    }
}
