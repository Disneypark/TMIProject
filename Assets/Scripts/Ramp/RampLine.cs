using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampLine : MonoBehaviour
{
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private Transform point3;
    [SerializeField] float vertecCount = 12.0f;

    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var pointList = new List<Vector3>();

        for (float ratio = 0; ratio <= 1; ratio += 1 / vertecCount)
        {
            var tangent1 = Vector3.Lerp(point1.position, point2.position, ratio);
            var tangent2 = Vector3.Lerp(point2.position, point3.position, ratio);
            var curve = Vector3.Lerp(tangent1, tangent2, ratio);

            pointList.Add(curve);
        }

        line.positionCount = pointList.Count;
        line.SetPositions(pointList.ToArray());
    }
}
