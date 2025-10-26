using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Rigidbody2D[] _rigidbodies;
    private HingeJoint2D[] _hingeJoints;
    private Transform[] _transforms;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody2D>();
        _hingeJoints = GetComponentsInChildren<HingeJoint2D>();
        _transforms = GetComponentsInChildren<Transform>();
        _animator = GetComponent<Animator>();
        DisableRagdollPhysics();
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
        //FlattenTransformTree();
        if (_animator)
        {
            _animator.StopPlayback();
        }
        foreach (var rb in _rigidbodies)
        {
            rb.isKinematic = false;
        }
    }

    public void FlattenTransformTree()
    {
        foreach (var t in _transforms)
        {
            t.parent = transform;
        }
    }

    public void DisableRagdollPhysics()
    {
        foreach (var rb in _rigidbodies)
        {
            rb.isKinematic = true;
        }
    }

}
