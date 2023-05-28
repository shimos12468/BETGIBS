using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    public bool move = false;
    private bool attached = false;

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.right = GetComponent<Rigidbody2D>().velocity.normalized;
        }
    }


    public void StartMovement()
    {
        move = true;   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
