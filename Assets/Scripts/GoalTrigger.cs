using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private GameObject goal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GoalInteractor>())
        {
            Debug.Log("You have reached your Goal!");
        }
    }

}
