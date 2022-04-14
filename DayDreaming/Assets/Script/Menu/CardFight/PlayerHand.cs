using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerHand : MonoBehaviour
{
    public Transform[] Cards;
    [SerializeField]
    private Transform[] controlPoints;


    List<UICard> UICards;

    private void Awake()
    {
        
    }

    private Vector2 BezierCasteljau(List<Vector2> points, int i, int k, float u)
    {
        if (k == 0)
        {
                return points[i];
        }
        else return BezierCasteljau(points, i, k - 1, u) * (1 - u) + BezierCasteljau(points, i + 1, k - 1, u) * u;
    }

    private List<Vector2> BezierCurveByCasteljau(List<Vector2> tabPoints, long nbU)
    {
        List<Vector2> points = new List<Vector2>();
        for(float k = 0f; k <= nbU; k++)
        {
            float u = k / nbU;
            Vector2 point = BezierCasteljau(tabPoints, 0, tabPoints.Count-1, u);
            points.Add(point);
        }
        return points;
    }

    private void OnDrawGizmos()
    {
        List<Vector2> pts = new List<Vector2>();
        foreach (Transform pt in controlPoints)
        {
            pts.Add(new Vector2(pt.position.x, pt.position.y));
        }
        List<Vector2> curve = BezierCurveByCasteljau(pts, 50);
        foreach (Vector2 pt in curve)
            Gizmos.DrawSphere(pt, 10f);

        for(int i = 0; i < Cards.Length; i++)
        {
            float gap = (float)i / (Cards.Length-1);
            int curveIndex = (int)((curve.Count-1) * gap);
            Cards[i].transform.position = new Vector3(curve[curveIndex].x, curve[curveIndex].y, 0);
        }
    }

    public void Start()
    {
        UICards = new List<UICard>(GetComponentsInChildren<UICard>());
    }



    // Ecarter les deux cartes autour
    public void OnCardHover(PointerEventData data)
    {
        Debug.Log("enter");
    }

    // Ecarter les deux cartes autour
    public void OnCardExit(PointerEventData data)
    {
        Debug.Log("exit");
    }
}
