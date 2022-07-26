using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class BladeR : MonoBehaviour
{
    enum States
    {
        ping=0,
        pong=1
        
    }

    private States _currentState = States.ping;
    [SerializeField] private Transform position1;

    [SerializeField] private Transform position2;

    [SerializeField] private float duration = 1;
    // Start is called before the first frame update

    void FixedUpdate()
    {
        if (_currentState==States.ping)
        {
            QuaternionSlerpPos1();
        }
        else
        {
            QuaternionSlerpPos2();
        }
    }

    [Button]
    private void QuaternionSlerpPos1()
    {
        StartCoroutine(Slerp1());
    }

    IEnumerator Slerp1()
    {
        for (float f = 0; f <= duration; f += Time.deltaTime)
        {
            transform.rotation=Quaternion.Slerp(position2.rotation,position1.rotation,f/duration);
            yield return null;
        }

        transform.rotation = position1.rotation;
        _currentState = States.pong;
    }
    [Button]
    // Update is called once per frame[Button]
    private void QuaternionSlerpPos2()
    {
        StartCoroutine(Slerp2());
    }

    IEnumerator Slerp2()
    {
        for (float f = 0; f <= duration; f += Time.deltaTime)
        {
            transform.rotation=Quaternion.Slerp(position1.rotation,position2.rotation,f/duration);
            yield return null;
        }

        transform.rotation = position1.rotation;
        _currentState = States.ping;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            SceneManager.LoadScene("VRLevel");
        }
    }
}
