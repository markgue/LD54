using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//place this on the hex mesh in order to manipulate its altitude
public class HexAltitude : MonoBehaviour
{
    [SerializeField] float magnitude;
    Coroutine rumble;
    Vector3 start;

    public void Rumble(int times, int period)
    {
        if (rumble != null)
        {
            StopCoroutine(rumble);
            transform.position = start;
        }
        rumble = StartCoroutine(RumbleRoutine(times, period));
    }

    private IEnumerator RumbleRoutine(int times, int period)
    {
        start = transform.localPosition;
        for (int i = 0; i < times; i++)
        {
            for (int j = 0; j < period; j++)
            {
                transform.localPosition = start + new Vector3(0, Mathf.Lerp(0, -magnitude, j / (float)period), 0);
                yield return new WaitForFixedUpdate();
            }
            transform.localPosition = start + new Vector3(0, -magnitude, 0);
            yield return new WaitForFixedUpdate();
            for (int j = 0; j < period; j++)
            {
                transform.localPosition = start + new Vector3(0, Mathf.Lerp(-magnitude, 0, j / (float)period), 0);
                yield return new WaitForFixedUpdate();
            }
        }
        transform.localPosition = start;
        rumble = null;
    }
}
