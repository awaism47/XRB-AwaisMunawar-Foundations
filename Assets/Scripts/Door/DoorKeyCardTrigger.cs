using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorKeyCardTrigger : DoorTrigger
{
    [SerializeField] private int _KeyCardLevel = 1;

    [SerializeField] private XRSocketInteractor _socket;
    [SerializeField] private Renderer _lightObject;
    [SerializeField] private Light _light;
    [SerializeField] private Color _defaultColor = Color.blue;
    [SerializeField] private Color _errorColor =Color.red;
    [SerializeField] private Color _successColor = Color.green;
    private bool _IsOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        SetLightColor(_defaultColor);
        _socket.selectEntered.AddListener(KeyCardInserted);
        _socket.selectExited.AddListener(KeyCardRemoved);
    }

    private void KeyCardRemoved(SelectExitEventArgs arg0)
    {
        SetLightColor(_defaultColor);
        _IsOpen = false; 
        CloseDoor();
    }

    private void KeyCardInserted(SelectEnterEventArgs arg0)
    {
        if (!arg0.interactable.transform.TryGetComponent(out KeyCard keycard))
        {
            Debug.Log("No keycard component attached to inserted object");
            SetLightColor(_errorColor);
            return;
        }

        if (keycard.keyCardLevel >= _KeyCardLevel)
        {
            SetLightColor(_successColor);
            _IsOpen = true;
            OpenDoor();
        }
        else
        {
            SetLightColor(_errorColor);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if(_IsOpen) return;
        
        base.OnTriggerExit(other);
    }

    private void SetLightColor(Color color)
    {
        _lightObject.material.color = color;
        _light.color = color;
    }
}

