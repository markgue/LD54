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
    bool dashReady = true;

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
        transform.rotation = q;
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
