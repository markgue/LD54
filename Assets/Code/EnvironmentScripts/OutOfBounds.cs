using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This bound is meant to tell when a fighter has fallen off the arena into the out of bounds area
public class OutOfBounds : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FighterMovement f = other.transform.GetComponentInParent<FighterMovement>();
        if (f != null && !other.isTrigger)
            LevelManager.Instance.RemoveFighter(f);
    }
}
