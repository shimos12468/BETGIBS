using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelBehaviour : MonoBehaviour
{
    public GameObject arrowPrefab;
  
    
    public float force = 10;
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
      

        GameObject Arrow = Instantiate(arrowPrefab);
        Arrow.transform.position = transform.position;
        
        SetArrow(Arrow);

    }

    public void SetArrow(GameObject Arrow)
    {
        Arrow.gameObject.GetComponent<ArrowBehaviour>().move = true;
        Arrow.GetComponent<Rigidbody2D>().AddForce(transform.up * force, ForceMode2D.Impulse);
    }

   
   
}
