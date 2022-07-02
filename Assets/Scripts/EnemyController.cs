using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _threshold=0.5f;
    [SerializeField] private PatrolRoute _patrolRoute;
    
    private Transform _currentPoint;
    private bool _moving = false;
    //Need to create integer to allow us to loop through each point on each frame
    private int _routeIndex = 0;
    private bool _movingForward = true;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentPoint = _patrolRoute.route[_routeIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!_moving)
        {
            NextPatrolPoint();
            _agent.SetDestination(_currentPoint.position);
            _moving = true;

        }

        if (_moving && Vector3.Distance(transform.position,_currentPoint.position)<_threshold)
        {
            _moving = false;
        }
    }

    private void NextPatrolPoint()
    {
        //If Loop then always moving forward but if not then moving backwards after reaching 3rd point
        
        if (_movingForward)
        {
            _routeIndex++;
            
        }
        else
        {
            _routeIndex--;
        }
        
        
        if (_routeIndex == _patrolRoute.route.Count)
        {
            if (_patrolRoute.patrolType==PatrolRoute.PatrolType.Loop)
            {
                _routeIndex = 0;
            }
            else
            {
                _movingForward = false;
                _routeIndex -= 2;
            }
            
        }

        if (_routeIndex == 0)
        {
            _movingForward = true;
        }

        _currentPoint = _patrolRoute.route[_routeIndex];
    }
}
