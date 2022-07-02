using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class PatrolRoute : MonoBehaviour
{
    //Good practice to hardcode enum with integers to not confuse unity
    public enum PatrolType
    {
        Loop =0,
        PingPong=1
    }
    //Serialising enum to allow for drop down
    [SerializeField] public PatrolType patrolType;
    [SerializeField] public List<Transform> route;
    [SerializeField] private Color _patrolRouteColor=Color.green;

    
    [Button("Add Patrol Point")]
    private void AddPatrolPoint()
    {
        GameObject thisPoint = new GameObject();
        thisPoint.transform.position = transform.position;
        thisPoint.transform.parent = transform;
        thisPoint.name = "Point" + (route.Count + 1);
        route.Add(thisPoint.transform);
        
        #if UNITY_EDITOR
        Undo.RegisterCreatedObjectUndo(thisPoint,"Petrol point undo");
        #endif
    }

    [Button("Reverse Patrol Route")]
    private void ReversePatrolRoute()
    {
        route.Reverse();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _patrolRouteColor;
        
        for (int i = 0; i < route.Count - 1; i++)
        {
            Gizmos.DrawLine(route[i].position,route[i+1].position);
        }

        if (patrolType == PatrolType.Loop)
        {
            Gizmos.DrawLine(route[route.Count-1].position,route[0].position);
        }
        #if UNITY_EDITOR
        Handles.Label(transform.position,gameObject.name);
        #endif
    }
}
