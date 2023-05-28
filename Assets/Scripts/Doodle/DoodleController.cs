using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DoodleController : MonoBehaviour
{


    public static DoodleController _instance;
    public LineRenderer lineRenderer;
    public List<BoxCollider2D> _greenAreas;
    public List<GameObject> _meshes;
    public int lengthLimit=100;
    public int totalDrawnPointsLimit = 1000;
    public int totalDrawnPoints;
    public GameObject linePrefab;
    public float newPointThreshold = 0.1f;
    private bool _dragging = false;
    Vector3 mousePosition;

    private void Awake()
    {
        if(_instance==null)_instance= this; 
        else { Destroy(gameObject); }
    }

    void SpawnLine()
    {
        GameObject line = Instantiate(linePrefab);
        Mesh mesh = new Mesh();
        lineRenderer.BakeMesh(mesh, true);
        Vector3[] points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);
        line.GetComponent<MeshFilter>().mesh = mesh;
        line.GetComponent<EdgeCollider2D>().points = points.ToList().ConvertAll(p => new Vector2(p.x, p.y)).ToArray();
        //setting center of mass
        Vector2 center = new Vector2(0, 0);
        foreach (Vector2 point in line.GetComponent<EdgeCollider2D>().points)
        {
            center += point;
        }
        center /= line.GetComponent<EdgeCollider2D>().points.Length;
        line.GetComponent<Rigidbody2D>().centerOfMass = center;
        line.GetComponent<Rigidbody2D>().isKinematic = true;
        _meshes.Add(line);
    }



    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        if (Input.GetMouseButtonDown(0) && CanDraw())
        {
            _dragging = true;
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, mousePosition);
        }
        else if (Input.GetMouseButton(0) && _dragging && CanDraw())
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
            SpawnLine();
            totalDrawnPoints += lineRenderer.positionCount - 1;
            lineRenderer.positionCount = 0;
        }

    }

    public bool CanDraw()
    {
        
        foreach (var area in _greenAreas)
        {
            Bounds bounds = area.bounds;
            print(bounds);
            Vector3 mousePosition1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition1.z = 0f;
            print(mousePosition1);
            if (bounds.Contains(mousePosition1))
            {
                return true;
            }
        }
        return false;
    }

    public void SetDoodleKinamatic()
    {
        foreach(var mesh in _meshes)
        {
            mesh.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}
