using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DoodleController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject linePrefab;
    public float newPointThreshold = 0.1f;
    private bool _dragging = false;
    
    void SpawnLine()
    {
        GameObject line = Instantiate(linePrefab);
        Mesh mesh = new Mesh();
        lineRenderer.BakeMesh(mesh, true);
        Vector3[] points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);
        line.GetComponent<MeshFilter>().mesh = mesh;
        //line.GetComponent<EdgeCollider2D>().points = points.ToList().ConvertAll(p => new Vector2(p.x, p.y)).ToArray();
        foreach (var point in points)
        {
            var col = line.AddComponent<CircleCollider2D>();
            col.offset = point;
            col.radius = 0.1f;
        }
        line.GetComponent<Rigidbody2D>().mass = line.GetComponent<EdgeCollider2D>().points.Length;
        //setting center of mass
        Vector2 center = new Vector2(0, 0);
        foreach (Vector2 point in points)
        {
            center += point;
        }
        center /= points.Length;
        line.GetComponent<Rigidbody2D>().centerOfMass = center;
    }



    // Update is called once per frame
    void Update()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        if (Input.GetMouseButtonDown(0))
        {
            _dragging = true;
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, mousePosition);
        }
        else if (Input.GetMouseButton(0) && _dragging)
        {
            if (Vector3.Distance(lineRenderer.GetPosition(lineRenderer.positionCount - 1), mousePosition) > newPointThreshold)
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, mousePosition);
            }
        }
        else if (Input.GetMouseButtonUp(0) && _dragging)
        {
            _dragging = false;
            if (lineRenderer.positionCount > 1)
            {
                SpawnLine();
            }
            lineRenderer.positionCount = 0;
        }

    }
}
