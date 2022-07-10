using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteractor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("lock"))
        {
            transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            transform.position = collision.transform.position;

           
            

            
        }
        
    }

}
