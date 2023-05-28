using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowBehaviour : MonoBehaviour
{

    public GameObject arrowPrefab;
    public Transform anchor, endAnchor, startAnchor;
    public float shootingVelocity = 30;
    public LineRenderer firstLine, secondLine;
    [Range(0f,10f)] public float timeOfShooting;
    private float currentTime = 0;
    GameObject Arrow;
    private int shots = 0;
    private bool instantiateArrow = true;

    private void OnGUI()
    {
        if (GUILayout.Button("Shoot"))
        {
            StartShooting();
        }
    }

    void Start()
    {
        firstLine.SetPosition(0, firstLine.transform.position);
        firstLine.SetPosition(1, anchor.position);
        
        secondLine.SetPosition(0, secondLine.transform.position);
        secondLine.SetPosition(1, anchor.position);

        anchor.position = startAnchor.position;
    }

    private void OnDrawGizmos()
    {
        anchor.position = startAnchor.position;
        firstLine.SetPosition(0, firstLine.transform.position);
        firstLine.SetPosition(1, anchor.position);
        secondLine.SetPosition(0, secondLine.transform.position);
        secondLine.SetPosition(1, anchor.position);

    }

    void Update()
    {
        if (shots > 0)
        {
            if (instantiateArrow)
            {
                instantiateArrow = false;
                Arrow = Instantiate(arrowPrefab);
                Arrow.transform.right = anchor.up;
                Arrow.transform.position = anchor.transform.position;
            }

            currentTime += Time.deltaTime;
            if (currentTime >= timeOfShooting)
            {
                currentTime = 0;
                shots--;
                Arrow.GetComponent<ArrowBehaviour>().StartMovement();
                Arrow.GetComponent<Rigidbody2D>().velocity = Arrow.transform.right * shootingVelocity;
                instantiateArrow = true;
            }
            
            var percentage = (currentTime/timeOfShooting);
            anchor.position = Vector3.MoveTowards(startAnchor.position, endAnchor.position, percentage);
            Arrow.transform.position = anchor.position;
            firstLine.SetPosition(0, firstLine.transform.position);
            firstLine.SetPosition(1, anchor.position);
            secondLine.SetPosition(0, secondLine.transform.position);
            secondLine.SetPosition(1, anchor.position);
        }
    }

    public void StartShooting()
    {
        shots++;
    }
}
