using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;



public class BoardManager : MonoBehaviour
{
    public GameObject hexagonPrefab; // Reference to the hexagon prefab.
    public GameObject borderHexagonPrefab; // Reference to the border hexagon prefab (for the AI)
    public Material[] materials = new Material[3];
    public int numRings = 5; // Number of rings in the hex grid
    public float hexRadius = 1.0f; // The distance from the center of a hexagon to any of its vertices.
    public float delayBetweenTiles = 0.1f; // Delay in seconds between creating each tile.

    private const float ROOT3 = 1.73205f;

    private IEnumerator Start()
    {
        yield return StartCoroutine(GenerateHexagonalGrid());
    }

    IEnumerator InstantiateHex(int x, int y, bool isBorder)
    {
        Vector3 hexPosition = new Vector3((x + y / 2f) * ROOT3 * hexRadius, 0, (y / 2f) * 3f * hexRadius);
        GameObject hexFab = Instantiate(isBorder ? borderHexagonPrefab : hexagonPrefab, hexPosition, Quaternion.identity);
        HexTile hexTileScript = hexFab.GetComponent<HexTile>();
        hexTileScript.SetHexTileLocation(x, y);
        if (!isBorder)
        {
            hexFab.GetComponentInChildren<Renderer>().material = materials[((x-y)%3 + 3) % 3];
            yield return new WaitForSeconds(delayBetweenTiles); // Wait before creating the next tile.
        }
        yield return true;
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
        for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(x, ++y, true)); // move down right. Note N-1
        for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(--x, ++y, true)); // move down left
        for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(--x, y, true)); // move left
        for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(x, --y, true)); // move up left
        for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(++x, --y, true)); // move up right
        for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(++x, y, true));  // move right


        yield return true;
    }
}
