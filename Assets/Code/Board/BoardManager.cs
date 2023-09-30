using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;



public class BoardManager : MonoBehaviour
{
    public GameObject hexagonPrefab; // Reference to the hexagon prefab.
    public int numRings = 5; // Number of rings in the hex grid
    public float hexRadius = 1.0f; // The distance from the center of a hexagon to any of its vertices.
    public float delayBetweenTiles = 0.1f; // Delay in seconds between creating each tile.

    private const float ROOT3 = 1.73205f;

    private IEnumerator Start()
    {
        yield return StartCoroutine(GenerateHexagonalGrid());
    }

    IEnumerator InstantiateHex(int x, int y)
    {
        Debug.Log("x" + x + "y" + y);
        Vector3 hexPosition = new Vector3((x + y / 2f) * ROOT3 * hexRadius, 0, (y / 2f) * 3f * hexRadius);
        GameObject hexFab = Instantiate(hexagonPrefab, hexPosition, Quaternion.identity);
        yield return new WaitForSeconds(delayBetweenTiles); // Wait before creating the next tile.
    }

    IEnumerator GenerateHexagonalGrid()
    {
        int x = 0, y = 0;
        yield return StartCoroutine(InstantiateHex(x, y));    
        for (int ring = 1; ring < numRings; ring++)
        {
            for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(++x, y));  // move right
            for(int i=0; i<ring-1; ++i) yield return StartCoroutine(InstantiateHex(x, ++y)); // move down right. Note N-1
            for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(--x, ++y)); // move down left
            for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(--x, y)); // move left
            for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(x, --y)); // move up left
            for(int i=0; i<ring; ++i) yield return StartCoroutine(InstantiateHex(++x, --y)); // move up right
        }
        for(int i=0; i<numRings-1; ++i) yield return StartCoroutine(InstantiateHex(++x, y)); // move right
        yield return true;
    }
}
