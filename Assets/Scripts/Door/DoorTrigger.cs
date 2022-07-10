using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private Transform _key;
    [SerializeField] private Transform _lock;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<DoorInteractor>() && _lock.transform.position==_key.transform.position)
        {
            door.SetActive(false);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.GetComponent<DoorInteractor>())
        {
            door.SetActive(true);
            
        }
    }
}
