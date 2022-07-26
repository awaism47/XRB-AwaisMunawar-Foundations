using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Blades : MonoBehaviour
{
    enum States
    {
        right=0,
        left=1,
        active=3,
        nonActive=4
        
    }

    [SerializeField] private States _currentState;
    [SerializeField] private Quaternion _angleRight=Quaternion.Euler(0,0,45);
    [SerializeField] private Quaternion _angleLeft=Quaternion.Euler(0,0,-45);
    [SerializeField] private Quaternion currentAngle;
    private float futureAngle;
    public float speed = 10f;
    public float pingPong;

    private float timeCount=0.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (_currentState == States.right)
        {
            transform.rotation=_angleRight;
            currentAngle = _angleRight;
            futureAngle = -46;
            _currentState = States.active;

        }
        else if(_currentState == States.left)
        {
            transform.rotation = _angleLeft;
            currentAngle = _angleLeft;
            futureAngle = 46;
            _currentState = States.active;
        }
    }

    // Update is called once per frame
    void Update()
    {

        currentAngle = transform.rotation;
        pingPong= Mathf.PingPong(Time.time*speed, 360);
        //ChangeDirection();
        StartCoroutine(RotateOverTime(currentAngle, Quaternion.Euler(0, 0, pingPong), speed));

        // transform.rotation = Quaternion.Slerp(currentAngle, Quaternion.Euler(0,0,futureAngle), 2);
        //timeCount =timeCount+ Time.deltaTime;



    }

    IEnumerator RotateOverTime(Quaternion start, Quaternion end, float speed)
    {
        _currentState = States.nonActive;
        float t = 0f;
        while(t < speed)
        {
            transform.rotation = Quaternion.Slerp(start, end, t / speed);
            yield return null;
            t += Time.deltaTime;
        }

        transform.rotation = end;
        _currentState = States.active;
    }

    private void ChangeDirection()
    {
        print(currentAngle.z);
        if (_currentState == States.active)
        {


            if (pingPong > 0.8)
            {
                futureAngle = -45;
                StartCoroutine(RotateOverTime(currentAngle, Quaternion.Euler(0, 0, futureAngle), 2));



            }
            else if (pingPong < 0.2)
            {
                futureAngle = 45;
                StartCoroutine(RotateOverTime(currentAngle, Quaternion.Euler(0, 0, futureAngle), 2));

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            SceneManager.LoadScene("VRLevel");
        }
    }
}
