using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private GameObject goal;
    [SerializeField] private GameObject cube;


    private void OnTriggerEnter(Collider other)
    {
        
        
        if (other.GetComponent<GoalInteractor>())
        {
            cube.AddComponent<Rigidbody>();


        }
    }
}
