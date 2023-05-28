using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Rigidbody2D[] _rigidbodies;
    private HingeJoint2D[] _hingeJoints;
    private Transform[] _transforms;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody2D>();
        _hingeJoints = GetComponentsInChildren<HingeJoint2D>();
    }

    public bool IsBroken()
    {
        foreach (var hj in _hingeJoints)
        {
            if (hj == null)
            {
                return true;
            }
        }
        return false;
    }

    public void EnableRagdollPhysics() {
        foreach (var rb in _rigidbodies)
        {
            rb.isKinematic = false;
        }
    }
}
