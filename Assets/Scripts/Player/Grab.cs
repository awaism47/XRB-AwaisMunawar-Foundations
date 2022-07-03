using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Grab : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition;
    [SerializeField] private Transform _holdPosition;
    [SerializeField] private float _grabRange = 3f;
    [SerializeField] private float _throwForce = 20f;
    [SerializeField] private float _snapSpeed = 30f;
    
    [SerializeField] private GameObject _grabbedObject;
    [SerializeField] public CharacterController _charController;


    private Crouch _crouch;
    [SerializeField] private GameObject boxCanvas;
    

    // Update is called once per frame
   

    private void OnGrab()
    {
        boxCanvas.SetActive(false);
       
     
        Debug.Log("Grab pressed");
        if (Physics.Raycast(_cameraPosition.position, _cameraPosition.forward,out RaycastHit hit,_grabRange))
        {
           if(!hit.transform.gameObject.CompareTag("Box")) return;
           
            boxCanvas.SetActive(true);
            transform.tag = "Box";
            _charController.transform.position = _cameraPosition.position;
            _charController.height = 0.5f;
            _grabbedObject = hit.rigidbody.gameObject;
            _grabbedObject.transform.parent = transform.parent;
            _grabbedObject.SetActive(false);
            
        }
            
            
        
    }
}
