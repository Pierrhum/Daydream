using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    [SerializeField]
    private bool debug = false;

    // Bezier Curve
    [SerializeField]
    private Transform[] controlPoints;
    [SerializeField]
    private long nbPoints;

    [System.NonSerialized]
    public List<Vector2> curve;
    [System.NonSerialized]
    public List<float> angles = new List<float>();

    private void Awake()
    {
        InitCurve();
    }
    private void OnDrawGizmos()
    {
        InitCurve();
        if (debug)
        {
            foreach (Vector2 pt in curve)
                Gizmos.DrawSphere(pt, 10f);
        }
    }

    private float GetPointAngle(int index)
    {
        Vector3 ab;
        if (index < curve.Count - 1)
            ab = curve[index] - curve[index + 1];
        else
            ab = curve[index-1] - curve[index];
        Vector2 cross = Vector3.Cross(ab, Vector3.forward);
        cross.Normalize();
        Vector2 ac = new Vector2(cross.y, curve[index].y);
        ac.Normalize();

        Vector2 b = 30 * cross + curve[index];
        Vector2 c = 30 * ac + curve[index]; 
        if (debug)
        {
            Debug.DrawLine(b, curve[index], Color.red);
            Debug.DrawLine(c, curve[index]);
        }

        float angle = Vector2.Angle(b - curve[index], c - curve[index]);
        float res = (index <= ((curve.Count - 1) * 0.5) ? angle : (360.0f - angle));
        res = (res > 0.0f && res < 1.0f) ? 0.0f : res;
        return res;
    }

    private void InitCurve()
    {
        List<Vector2> pts = new List<Vector2>();
        foreach (Transform pt in controlPoints)
        {
            pts.Add(new Vector2(pt.position.x, pt.position.y));
        }
        curve = BezierCurveByCasteljau(pts);
        for(int i=0; i < curve.Count; i++)
        {
            angles.Add(GetPointAngle(i));
        }
    }

    private Vector2 BezierCasteljau(List<Vector2> points, int i, int k, float u)
    {
        if (k == 0)
        {
            return points[i];
        }
        else return BezierCasteljau(points, i, k - 1, u) * (1 - u) + BezierCasteljau(points, i + 1, k - 1, u) * u;
    }

    private List<Vector2> BezierCurveByCasteljau(List<Vector2> tabPoints)
    {
        List<Vector2> points = new List<Vector2>();
        for (float k = 0f; k <= nbPoints; k++)
        {
            float u = k / nbPoints;
            Vector2 point = BezierCasteljau(tabPoints, 0, tabPoints.Count - 1, u);
            points.Add(point);
        }
        return points;
    }
}
