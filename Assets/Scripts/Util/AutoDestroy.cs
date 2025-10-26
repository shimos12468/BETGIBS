using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float seconds=3f;
    void Start()
    {
        Destroy(gameObject, seconds);
    }

}
