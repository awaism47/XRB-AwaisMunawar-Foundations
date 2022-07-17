using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountThrows : MonoBehaviour
{
    
    public UnityEvent OneLessThrows;

    private void OnTriggerEnter(Collider other)
    {
        OneLessThrows.Invoke();
        Debug.Log("Threw object");
    }
}
