using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelBehaviour : MonoBehaviour
{
    public GameObject arrowPrefab;  
    public float shootingVelocity = 10;
    public float cooldown = 4;
    public float time = 4;
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(name);
        if (time >= cooldown)
        {
            GameObject Arrow = Instantiate(arrowPrefab);
            Arrow.transform.position = transform.position;
            SetArrow(Arrow);
            time = 0;
        }
    }

    private void Update()
    {
        time+=Time.deltaTime;
    }
    public void SetArrow(GameObject Arrow)
    {
        Arrow.GetComponent<ArrowBehaviour>().StartMovement();
        Arrow.GetComponent<Rigidbody2D>().linearVelocity = transform.up * shootingVelocity;
    }
}
