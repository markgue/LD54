using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;



public class BoardManager : MonoBehaviour
{
    public GameObject hexagonPrefab; // Reference to the hexagon prefab.
    public GameObject borderHexagonPrefab; // Reference to the border hexagon prefab (for the AI)
    public Material[] materials = new Material[3];
    public int numRings = 5; // Number of rings in the hex grid
    public float hexRadius = 1.0f; // The distance from the center of a hexagon to any of its vertices.
    public float delayBetweenTiles = 0.1f; // Delay in seconds between creating each tile.
    public float ringFallDelay = 0.01f;

    private const float ROOT3 = 1.73205f;

    private Dictionary<Vector2, HexTile> hexMap = new Dictionary<Vector2, HexTile>();

    private IEnumerator Start()
    {
        yield return StartCoroutine(GenerateHexagonalGrid());
        yield return StartCoroutine(TestDestroy(7));
        yield return StartCoroutine(TestDestroy(6));
        yield return StartCoroutine(TestDestroy(5));
        yield return StartCoroutine(TestDestroy(4));
        yield return StartCoroutine(TestDestroy(3));
        yield return StartCoroutine(TestDestroy(2));
    }

    IEnumerator TestDestroy(int ring)
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Destroying ring " + ring);
        yield return DestroyRing(ring);
    }

    IEnumerator InstantiateHex(int x, int y, bool isBorder)
    {
        Vector3 hexPosition = new Vector3((x + y / 2f) * ROOT3 * hexRadius, 0, (y / 2f) * 3f * hexRadius);
        GameObject hexFab = Instantiate(isBorder ? borderHexagonPrefab : hexagonPrefab, hexPosition, Quaternion.identity, gameObject.transform);
        HexTile hexTileScript;
        Vector2 v = new Vector2(x, y);
        if (isBorder) 
        {
            hexTileScript = hexFab.GetComponentInChildren<HexTile>();
            hexTileScript.SetHexTileLocation(x, y);
            hexTileScript.isDestroyed = true;
            yield return true;
        }
        else
        {
            hexTileScript = hexFab.GetComponent<HexTile>();
            hexTileScript.SetHexTileLocation(x, y);
            hexMap.Add(v, hexTileScript);
            hexFab.GetComponentInChildren<Renderer>().material = materials[((x-y)%3 + 3) % 3];
            yield return new WaitForSeconds(delayBetweenTiles); // Wait before creating the next tile.
        }
    }

    IEnumerator GenerateHexagonalGrid()
    {
        int x = 0, y = 0, ring = 1;
        yield return StartCoroutine(InstantiateHex(x, y, false));    
        for (; ring < numRings; ring++)
        {
            for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(++x, y, false));  // move right
            for(int i=0; i<ring-1; ++i) yield return StartCoroutine(InstantiateHex(x, ++y, false)); // move down right. Note N-1
            for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(--x, ++y, false)); // move down left
            for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(--x, y, false)); // move left
            for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(x, --y, false)); // move up left
            for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(++x, --y, false)); // move up right
        }
        for(int i=0; i<numRings-1; ++i) yield return StartCoroutine(InstantiateHex(++x, y, false)); // move right

        // Create AI border
        ++x;
        --y;
        Debug.Log("Pos " + x + y);
        for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(x, ++y, true)); // move down right. Note N-1
        for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(--x, ++y, true)); // move down left
        for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(--x, y, true)); // move left
        for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(x, --y, true)); // move up left
        for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(++x, --y, true)); // move up right
        for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(++x, y, true));  // move right


        yield return true;
    }

    public void DestroyTile(Vector2 tile, bool ignoreDamage)
    {
        HexTile ht = hexMap[tile];
        if (ht)
        {
            ht.ignoreDamageIndicator = ignoreDamage;
            ht.DamageTile(ht.hp);
        }
    }

    private IEnumerator DestroyRing(int ring)
    {
        int x = ring, y = -ring;
        for(int i=0; i<ring; ++i) 
        {
            DestroyTile(new Vector2(x, y++), true); // move down right. Note N-1 
            yield return new WaitForSeconds(ringFallDelay);
        } 
        for(int i=0; i<ring; ++i) 
        {
            DestroyTile(new Vector2(x--, y++), true); // move down left 
            yield return new WaitForSeconds(ringFallDelay);
        } 
        for(int i=0; i<ring; ++i) 
        {
            DestroyTile(new Vector2(x--, y), true); // move left 
            yield return new WaitForSeconds(ringFallDelay);
        } 
        for(int i=0; i<ring; ++i) 
        {
            DestroyTile(new Vector2(x, y--), true); // move up left 
            yield return new WaitForSeconds(ringFallDelay);
        } 
        for(int i=0; i<ring; ++i) 
        {
            DestroyTile(new Vector2(x++, y--), true); // move up right 
            yield return new WaitForSeconds(ringFallDelay);
        } 
        for(int i=0; i<ring; ++i) 
        {
            DestroyTile(new Vector2(x++, y), true);  // move right 
            yield return new WaitForSeconds(ringFallDelay);
        } 
        yield return true;
    }
}
