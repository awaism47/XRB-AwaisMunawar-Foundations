using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    [SerializeField] public CharacterController _charController;
    [SerializeField] private float _crouchHeight = 1;
    [SerializeField] public Transform _cameraPosition;

    private float _originalHeight;
    private bool _crouched = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _charController.transform.position = _cameraPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Where does onCrouch gets called?
    private void OnCrouch()
    {
        if (_crouched)
        {
            _crouched = false;
            Debug.Log(("Player got up"));
            _charController.height = _originalHeight;
        }
        else
        {
            _crouched = true;
            _charController.height = _crouchHeight;
            Debug.Log(("Player crouched down"));

        }

    }

    public void GoInBox()
    {
        _charController.height = _crouchHeight;
        
    }

    
}
