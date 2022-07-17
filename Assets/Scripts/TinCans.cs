using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TinCans : MonoBehaviour
{
    public enum canState
    {
        standing=1,
        falling=0
        
    }
    
    public canState _currentState = canState.standing;
    public int _numberOfCansStanding = 0;
    public UnityEvent canFellOver;

    public Quaternion _originalRotation;
    public Vector3 _currentPosition;
    public Quaternion _currentRotation;
     
    void Start ()
    {
        
        _originalRotation = transform.rotation;
    }

    private void Update()
    {
        if (_currentState == canState.falling) return;
        _currentRotation = transform.rotation;
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Grabbable"))
        {
            
            CheckIfFell();
        }
        
    }

    private void CheckIfFell()
    {
        if (transform.rotation != _originalRotation)
        {
            if (_currentState == canState.falling) return;
            Debug.Log("Can fell over");
            canFellOver.Invoke();
            
            
            _currentState = canState.falling;
        }
    }
}
