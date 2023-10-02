using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class  HexTile: MonoBehaviour
{
    private const int MAX_HP = 3;
    public int q; // Row
    public int r; // Column

    public int hp;
    public bool isDestroyed = false;

    public void Start()
    {
        hp = MAX_HP;
    }

    public void FixedUpdate()
    {
        if (hp <= 0)
            DestroyTile();
    }

    public void SetHexTileLocation(int column, int row)
    {
        q = column;
        r = row;
    }
    public void DamageTile(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
            DestroyTile();
    }

    private void DestroyTile()
    {
        if(isDestroyed) 
        {
            return;
        }
        isDestroyed = true;
        GameObject hexCollider = gameObject.transform.GetChild(0).gameObject;
        hexCollider.AddComponent<Rigidbody>();
        hexCollider.transform.parent = null;
    }

    public void OnCollisionEnter(Collision collision)
    {
        FighterMovement ft = collision.collider.GetComponentInParent<FighterMovement>();
        if (ft != null && ft.IsAirborne())
        {
            DamageTile(1);
        }
    }

    public void OnDrawGizmos() 
    {
        Handles.Label(transform.position, "⦿" + q + "," + r);
    }
}
