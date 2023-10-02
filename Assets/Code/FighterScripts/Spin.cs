using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] float speed; //in degrees per physics tick
    Coroutine lerp;

    private void FixedUpdate()
    {
        transform.Rotate(0, speed, 0);
    }

    public void SetSpeed(float f)
    {
        if (lerp != null)
            StopCoroutine(lerp);
        lerp = StartCoroutine(SpeedLerp(f, 20));
    }

    private IEnumerator SpeedLerp(float goal, float ticks)
    {
        float start = speed;
        for (float i = 0f; i < ticks; i++)
        {
            speed = Mathf.Lerp(start, goal, i / ticks);
            yield return new WaitForFixedUpdate();
        }
        lerp = null;
    }
}
