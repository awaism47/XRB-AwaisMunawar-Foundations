using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class SoundEmitter : MonoBehaviour
{
    [SerializeField] private float _soundRadius = 5f;
    [SerializeField] private float _impulseThreshold = 2f;
    private AudioSource _audioSource;
    
    [SerializeField] private NavMeshAgent _robotInvestigator;

    [SerializeField] private NavMeshAgent _waitingRobot;

    private EnemyController _enemyController;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
       

    }
    // Update is called once per frame
    private void Update()
    {
        if(Vector3.Distance(_robotInvestigator.transform.position ,_waitingRobot.transform.position)<0.5f)
        {
           
            _robotInvestigator.SetDestination(transform.position);
            _waitingRobot.SetDestination(transform.position);
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.impulse.magnitude > _impulseThreshold || other.gameObject.CompareTag("Player"))
        {
            _audioSource.Play();
            Collider[] _colliders = Physics.OverlapSphere(transform.position, _soundRadius);
            foreach (var col in _colliders)
            {
                if (col.TryGetComponent(out EnemyController enemyController))
                {
                    _robotInvestigator.SetDestination(_waitingRobot.transform.position);

                    _enemyController = enemyController;


                }
            
            }
        }
        
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.magenta;
        Gizmos.DrawWireSphere(transform.position,_soundRadius);
    }
}
