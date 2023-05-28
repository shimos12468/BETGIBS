using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DoodleController : MonoBehaviour
{


    public static DoodleController instance;
    public LineRenderer lineRenderer;
    public List<BoxCollider2D> _greenAreas;
    public List<GameObject> _meshes;
    public int lengthLimit = 100;
    public int totalDrawnPointsLimit = 1000;
    public int totalDrawnPoints;
    public GameObject linePrefab;
    public float newPointThreshold = 0.1f;
    private bool _dragging = false;
    Vector3 mousePosition;

    private void Awake()
    {
        if(instance==null)instance= this; 
        else { Destroy(gameObject); }
    }
    private void Start()
    {
        GameObject[]GreenAreas= GameObject.FindGameObjectsWithTag("GreenArea");

        foreach(var area in GreenAreas)
        {
            _greenAreas.Add(area.GetComponent<BoxCollider2D>());
        }
    }

    private void OnDestroy()
    {
        instance = null;
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
        // foreach (var point in points)
        // {
        //     var col = line.AddComponent<CircleCollider2D>();
        //     col.offset = point;
        //     col.radius = 0.1f;
        // }

        line.GetComponent<Rigidbody2D>().mass = line.GetComponent<EdgeCollider2D>().points.Length;
        //setting center of mass
        Vector2 center = new Vector2(0, 0);
        foreach (Vector2 point in points)
        {
            center += point;
        }
        center /= points.Length;
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
            if (lineRenderer.positionCount > 1)
            {
                SpawnLine();
                totalDrawnPoints += lineRenderer.positionCount - 1;
            }
            lineRenderer.positionCount = 0;
        }

    }

    public bool CanDraw()
    {
        
        foreach (var area in _greenAreas)
        {
            Bounds bounds = area.bounds;
            
            Vector3 mousePosition1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition1.z = 0f;
            
            if (bounds.Contains(mousePosition1))
            {

                return true;
            }
        }
        return false;
    }

    public void EnableDoodlePhysics()
    {
        foreach(var mesh in _meshes)
        {
            mesh.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }

    public void DisableDoodlePhysics()
    {
        foreach (var mesh in _meshes)
        {
            mesh.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    public void HideDrawingAreas() {
        foreach (var area in _greenAreas)
        {
            area.gameObject.SetActive(false);
        }
    }

    public void ShowDrawingAreas()
    {
        foreach (var area in _greenAreas)
        {
            area.gameObject.SetActive(true);
        }
    }

    public void ResetDoodle()
    {
        foreach (var mesh in _meshes)
        {
            Destroy(mesh);
        }
        _meshes.Clear();
        totalDrawnPoints = 0;
    }
}
