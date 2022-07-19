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
        falling=0,
        movingRight=3,
        MovingLeft=4
        
    }
    
    public canState _currentState = canState.movingRight;
    public int _numberOfCansStanding = 0;
    public UnityEvent canFellOver;
    public float smoothing = 4f;
    public float distanceMove = 0.05f;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform RightPoint;
    [SerializeField] private Transform MiddlePoint;

    public Quaternion _originalRotation;
    public Vector3 _currentPosition;
    public Quaternion _currentRotation;
     
    void Start ()
    {
        _currentState = canState.movingRight;
    

    }

    

    private void Update()
    {
        if (_currentState == canState.falling) return;
        _currentRotation = transform.rotation;
        
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("bullet"))
        {
            canFellOver.Invoke();
            
            fall();
        }
        
    }

    private void fall()
    {
        transform.parent = null;
        transform.Rotate(10.0f, 0.0f, 0.0f, Space.Self);
        //transform.rotation.z=
    }
}
