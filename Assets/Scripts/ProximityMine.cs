using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


public class ProximityMine : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _enemy;
    
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
                
                ExplosionActivated(collider);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_rigidbody.velocity.magnitude > 15f && collision.gameObject.CompareTag("Stickable"))
        {
            _rigidbody.isKinematic = true;
            transform.up = collision.transform.right;
            
            transform.SetParent(collision.transform);
           
            

            
        }
        
    }

    private void ExplosionActivated(Collider collider)
    {
        
        _particleSystem.transform.position = transform.position;
        _particleSystem.gameObject.SetActive(true);
        _particleSystem.Play();
        Rigidbody[] _enemyRigidbodies = _enemy.GetComponentsInChildren<Rigidbody>();
        
        EnemyController _controller =_enemy.GetComponent<EnemyController>();
        _controller.Stop();
        

        foreach (Rigidbody i in _enemyRigidbodies)
        {
            i.isKinematic = false;
            i.mass=100f;
            i.drag = 10f;
            
        }


        //Destroy(collider.gameObject);
        Destroy(transform.gameObject);


    }

    private void OnDrawGizmos()
    {
        Gizmos.color=Color.magenta;
        Gizmos.DrawWireSphere(transform.position,mineRadius);
    }
}
