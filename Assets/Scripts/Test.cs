using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Rigidbody2D[] _rigidbodies;
    private HingeJoint2D[] _hingeJoints;
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody2D>();
        _hingeJoints = GetComponentsInChildren<HingeJoint2D>();
        foreach (var rb in _rigidbodies)
        {
            rb.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
      
    }
}
