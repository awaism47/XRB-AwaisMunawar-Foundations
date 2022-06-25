using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private GameObject goal;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject effect;

    private void OnTriggerEnter(Collider other)
    {
        
        
        if (other.GetComponent<GoalInteractor>())
        {

            StartCoroutine(Wait());
            
        }
    }
    private IEnumerator Wait()
    {
        effect.SetActive(true);
        yield return new WaitForSeconds(3);
        canvas.transform.GetChild(1).gameObject.SetActive(true);
        canvas.transform.GetChild(2).gameObject.SetActive(true);
        Debug.Log("You have reached your Goal!");

    }

}
