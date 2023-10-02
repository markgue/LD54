using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class  HexTile : MonoBehaviour
{
    private const int MAX_HP = 3;
    public int q; // Row
    public int r; // Column

    public int hp;
    public bool isDestroyed = false;
    public bool ignoreDamageIndicator = false;

    private Material mat;
    [SerializeField] HexAltitude alt;


    public void Start()
    {
        hp = MAX_HP;
        if (!isDestroyed) // Not an AI tile
        {
            mat = gameObject.GetComponentInChildren<Renderer>().material;
            mat.SetFloat("_CrackAmount", 0);
        }
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
        float dmgVisual = Mathf.Min(1, ((float)(MAX_HP - hp) / (float)(MAX_HP - 1)));
        if (!ignoreDamageIndicator)
            mat.SetFloat("_CrackAmount", dmgVisual);
        if (hp <= 0)
            DestroyTile();
        else
            alt.Rumble(1, 3);
    }

    private void DestroyTile()
    {
        if(isDestroyed) 
        {
            return;
        }
        isDestroyed = true;
        alt.Rumble(5, 5);
        StartCoroutine(DelayedFall(70));   
    }
    private IEnumerator DelayedFall(int delayTicks)
    {
        for (int i = 0; i < delayTicks; i++)
            yield return new WaitForFixedUpdate();
        GameObject hexCollider = gameObject.transform.GetChild(0).gameObject;
        Rigidbody rb = hexCollider.AddComponent<Rigidbody>();
        rb.AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 100f);
        hexCollider.transform.parent = null;
    }

    public void OnDrawGizmos() 
    {
        Handles.Label(transform.position, "⦿" + q + "," + r);
    }
}
