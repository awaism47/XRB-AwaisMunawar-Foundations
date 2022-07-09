using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    enum EnemyState
    {
        Patrol=0,
        Investigate=1
    }
    
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _threshold=0.5f;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private PatrolRoute _patrolRoute;
    [SerializeField] private FieldOfView _fov;
    [SerializeField] private EnemyState _state = EnemyState.Patrol;

    public UnityEvent<Transform> onPlayerFound;
    public UnityEvent onInvestigate;
    public UnityEvent onReturnToPatrol;
    
    private Transform _currentPoint;
    private bool _moving = false;
    //Need to create integer to allow us to loop through each point on each frame
    private int _routeIndex = 0;
    private bool _movingForward = true;
    private Vector3 _investigationPoint;
    private float _waitTimer = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentPoint = _patrolRoute.route[_routeIndex];
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("Speed",_agent.velocity.magnitude);
        
        if (_fov.visibleObjects.Count > 0)
        {
            PlayerFound(_fov.visibleObjects[0].position);
        }
        if (_state == EnemyState.Patrol)
        {
            UpdatePatrol();
        }
        else if(_state==EnemyState.Investigate)
        {
            UpdateInvestigate();
        }
        
    }

    public void InvestigatePoint(Vector3 investigationPoint)
    {
        SetInvestigationPoint(investigationPoint);
        onInvestigate.Invoke();
    }

    private void SetInvestigationPoint(Vector3 investigationPoint)
    {
        _state = EnemyState.Investigate;
        _investigationPoint = investigationPoint;
        _agent.SetDestination(_investigationPoint);
    }

    private void PlayerFound(Vector3 investigatePoint)
    {
        SetInvestigationPoint(investigatePoint);
        onPlayerFound.Invoke(_fov.creature.head);
        
    }
    private void UpdateInvestigate()
    {
        if (Vector3.Distance(transform.position, _investigationPoint) < _threshold)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer > _waitTime)
            {
                ReturnToPatrol();
            }

        }
        //Debug.Log("In Investigate Mode");
    }

    private void ReturnToPatrol()
    {
        _state = EnemyState.Patrol;
        _waitTimer = 0f;
        _moving = false;
        onReturnToPatrol.Invoke();
    }

    private void UpdatePatrol()
    {
        if (!_moving)
        {
            NextPatrolPoint();
            _agent.SetDestination(_currentPoint.position);
            _moving = true;
        }

        if (_moving && Vector3.Distance(transform.position, _currentPoint.position) < _threshold)
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
