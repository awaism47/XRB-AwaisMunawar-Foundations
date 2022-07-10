using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;


public class ProximityMine : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private ParticleSystem _particleSystem;
    private GameObject _enemy;
    private List<GameObject> _bodyPieces;
    [SerializeField] private float _explosionForce = 4f;
    [SerializeField] private GameObject _body;
    
    [SerializeField] private float mineRadius = 2f;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = transform.GetComponent<Rigidbody>();
        _rigidbody.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] _colliders = Physics.OverlapSphere(transform.position, mineRadius);
        foreach (Collider collider in _colliders)
        {
            
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Debug.Log(" Detected");
                _enemy = collider.gameObject;
                ExplosionActivated(collider);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_rigidbody.velocity.magnitude > 15f && collision.gameObject.CompareTag("Stickable"))
        {
            _rigidbody.isKinematic = true;
            transform.up = collision.contacts[0].normal;
            
            transform.SetParent(collision.transform);
           
            

            
        }
        
    }

    private void ExplosionActivated(Collider collider)
    {
        
        _particleSystem.transform.position = transform.position;
        _particleSystem.gameObject.SetActive(true);
        _particleSystem.Play();
        
        collider.gameObject.SetActive(false);
        _body.SetActive(true);
        Rigidbody[] _bodyRigidbody = _body.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody i in _bodyRigidbody)
        {
            i.AddExplosionForce(_explosionForce,transform.position,mineRadius);
            i.gameObject.transform.position = collider.transform.position;
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color=Color.magenta;
        Gizmos.DrawWireSphere(transform.position,mineRadius);
    }
}
