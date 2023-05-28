using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelBehaviour : MonoBehaviour
{
    public GameObject arrowPrefab;  
    public float shootingVelocity = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("hedddllo");
        GameObject Arrow = Instantiate(arrowPrefab);
        Arrow.transform.position = transform.position;
        SetArrow(Arrow);
    }

    public void SetArrow(GameObject Arrow)
    {
       
        Arrow.GetComponent<ArrowBehaviour>().StartMovement();
        Arrow.GetComponent<Rigidbody2D>().velocity = transform.up * shootingVelocity;
    }
}
