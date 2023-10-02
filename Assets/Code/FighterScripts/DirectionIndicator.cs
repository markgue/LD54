using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Directs the visuals for the directional indicator at the foot of the fighter
public class DirectionIndicator : MonoBehaviour
{
    [SerializeField] SpriteRenderer circle;
    [SerializeField] SpriteRenderer arrow;
    [SerializeField] Color baseColor;
    [SerializeField] Color dashReadyColor;
    [SerializeField] int lerpTicks = 10;
    bool dashReady = true;
    Coroutine lerp;

    private void Awake()
    {
        UpdateColors();
    }

    //takes a horizontal vector (meaning one on the xz plane) and rotates this object accordingly.
    public void SetDirection(Vector3 v)
    {
        float rotation = Vector3.Angle(new Vector3(0, 0, 1), v);
        if (v.x > 0)
            rotation *= -1;
        Quaternion q = new Quaternion();
        q.eulerAngles = new Vector3(90, 0, rotation);
        if (lerp != null)
            StopCoroutine(lerp);
        lerp = StartCoroutine(LerpRotation(q));
    }

    private IEnumerator LerpRotation(Quaternion goal)
    {
        Quaternion start = transform.rotation, current;
        for (int i = 0; i <= lerpTicks; i++)
        {
            current = Quaternion.Lerp(start, goal, i / (float)lerpTicks);
            transform.rotation = current;
            yield return new WaitForFixedUpdate();
        }
        lerp = null;
    }

    private void UpdateColors()
    {
        circle.color = baseColor;
        if (dashReady)
            arrow.color = dashReadyColor;
        else
            arrow.color = baseColor;
    }

    public void SetDashReady(bool b)
    {
        dashReady = b;
        UpdateColors();
    }

    public void SetBaseColor(Color c)
    {
        baseColor = c;
        UpdateColors();
    }

    public void SetDashReadyColor(Color c)
    {
        dashReadyColor = c;
        UpdateColors();
    }
}
