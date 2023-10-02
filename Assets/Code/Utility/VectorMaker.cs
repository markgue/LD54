using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Use this to help generate vectors with certain restraints
public class VectorMaker
{
    //returns a vector of magnitude 1 pointing in a random horizontal direction
    public Vector3 RandomHorizontalDirection()
    {
        float x = Random.Range(0f, 1f);
        float z = Random.Range(0f, 1f);
        if (x == 0f && z == 0f)
            return RandomHorizontalDirection();
        Vector3 v = new Vector3(x, 0, z);
        v.Normalize();
        return v;
    }

    //takes a vector and rotates it a random amount around the y axis. Input bounds are in degrees
    public Vector3 RandomYRotation(Vector3 start, float minRotation, float maxRotation)
    {
        float rotation = Random.Range(minRotation, maxRotation);
        return Quaternion.Euler(0, rotation, 0) * start;
    }
}
