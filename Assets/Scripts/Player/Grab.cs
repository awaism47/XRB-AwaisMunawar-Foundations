using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Grab : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition;
    [SerializeField] private Transform _holdPosition;
    [SerializeField] private float _grabRange = 3f;
    [SerializeField] private float _throwForce = 40f;
    [SerializeField] private float _snapSpeed = 15f;
    
    private Rigidbody _grabbedObject;
    private bool _grabPressed = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (_grabbedObject)
        {
            _grabbedObject.velocity = (_holdPosition.position - _grabbedObject.transform.position)*_snapSpeed;
        }
    }

    private void OnGrab()
    {
        if (_grabPressed)
        {
            _grabPressed = false;
           
            if(!_grabbedObject) return;
            DropGrabbedObject();
        }
        else
        {
            _grabPressed = true;
            
            if (Physics.Raycast(_cameraPosition.position, _cameraPosition.forward,out RaycastHit hit,_grabRange))
            {
                if(!hit.transform.gameObject.CompareTag("Grabbable")) return;
                _grabbedObject = hit.transform.GetComponent<Rigidbody>();
                _grabbedObject.transform.parent = _holdPosition;
                _grabbedObject.rotation = _holdPosition.rotation;
            }
            
            
        }
    }

    private void DropGrabbedObject()
    {
        _grabbedObject.transform.parent = null;
        _grabbedObject = null;
    }

    private void OnThrow()
    {
        if(!_grabbedObject) return;
        
        _grabbedObject.AddForce(_cameraPosition.forward*_throwForce,ForceMode.Impulse);
        DropGrabbedObject();

        
        
    }
}
