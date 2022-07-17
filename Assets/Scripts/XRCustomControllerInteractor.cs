using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class XRCustomControllerInteractor : MonoBehaviour
{
    public UnityEvent fallingCan = new UnityEvent();
    private XRBaseControllerInteractor _controller;


    private void Start()
    {
        _controller=GetComponent<XRBaseControllerInteractor>();
        Assert.IsNotNull(_controller,"There is no XRBaseControllerInteractor assigned to this hand"+gameObject.name);
        
        _controller.selectEntered.AddListener(ParentInteractorable);
        _controller.selectExited.AddListener(Unparent);
    }

    private void Unparent(SelectExitEventArgs arg0)
    {
        arg0.interactableObject.transform.parent = null;
    }
    

    private void  ParentInteractorable(SelectEnterEventArgs arg0)
    {
        arg0.interactableObject.transform.parent = transform;
    }

 
}
