using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointGenerator : MonoBehaviour
{
    private static CheckpointGenerator _Instance;
    public static CheckpointGenerator Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<CheckpointGenerator>();
            }
            return _Instance;
        }
    }

    private Vector2 CalculateIntersectionPoints(float delta, float b1, float a1, float slope, float y0, Vector2 spiderPos)
    {
        float x1_1 = (-b1 + Mathf.Sqrt(delta)) / (2 * a1);
        float x1_2 = (-b1 - Mathf.Sqrt(delta)) / (2 * a1);
        float y1_1 = slope * x1_1 + y0;
        float y1_2 = slope * x1_2 + y0;

        Vector2 point1 = new Vector2(x1_1, y1_1);
        Vector2 point2 = new Vector2(x1_2, y1_2);

        // Return the point closer to the spider
        return (point1 - spiderPos).magnitude < (point2 - spiderPos).magnitude ? point1 : point2;
    }

    public Vector3 Generate(Vector3 center, Vector3 spiderPos, float radius1, float radius2)
    {
        var r = Random.Range(radius1, radius2);
        var x = Random.Range(-r, r);
        var y = Mathf.Sqrt(r * r - x * x) * (Random.Range(0f, 1f) < 0.5f ? (-1f) : 1f);
        var point = new Vector2(x, y);

        //Debug.Log(radius1 + " " + radius2 + " " + r + " " + point);
        // Calculate the center position in 2D space
        var _center = new Vector2(center.x, center.z);
        point += _center;

        var _spiderPos = new Vector2(spiderPos.x, spiderPos.z);
        var slope = (point.y - _spiderPos.y) / (point.x - _spiderPos.x);
        var y0 = point.y - slope * point.x;
        var yp = y0 - _center.y;

        //return (new Vector3(point.x, 0, point.y), new Vector3(_spiderPos.x, 0, _spiderPos.y));
        // Calculate coefficients for quadratic equations
        var a1 = slope * slope + 1;
        var b1 = 2 * slope * yp - 2 * _center.x;
        var c1 = Mathf.Pow(yp, 2) + Mathf.Pow(_center.x, 2) - Mathf.Pow(radius1, 2);
        var c2 = Mathf.Pow(yp, 2) + Mathf.Pow(_center.x, 2) - Mathf.Pow(radius2, 2);

        var delta1 = Mathf.Pow(b1, 2) - 4 * a1 * c1;
        var delta2 = Mathf.Pow(b1, 2) - 4 * a1 * c2;

        // If delta1 is non-positive, return the generated point
        if (delta1 <= 0)
        {
            return new Vector3(point.x, 0, point.y);
        }

        // Calculate intersection points for the first radius
        Vector2 v1 = CalculateIntersectionPoints(delta1, b1, a1, slope, y0, _spiderPos);
        
        // Calculate intersection points for the second radius
        Vector2 v2 = CalculateIntersectionPoints(delta2, b1, a1, slope, y0, _spiderPos);

        var res = new Vector2(0, 0);
        res.x = Random.Range(v1.x, v2.x);
        var alpha = Mathf.Abs(x - v1.x) / Mathf.Abs(v2.x - v1.x);
        //Debug.Log(alpha);
        res.y = Mathf.Lerp(v1.y, v2.y, alpha);

        return new Vector3(res.x, 0, res.y);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
