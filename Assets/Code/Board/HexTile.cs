using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  HexTile: MonoBehaviour
{
    public int q; // Column
    public int r; // Row

    private int hp = 1;

    public void SetHexTileLocation(int column, int row)
    {
        q = column;
        r = row;
    }
    public void DamageTile(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
            ; // Debug.Log("Destroyed");
    }
}
