using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockAndKey : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<KeyInteractor>())
        {
            Debug.Log("Detected");
            
        }
    }
}
