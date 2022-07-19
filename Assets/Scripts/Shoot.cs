using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class Shoot : MonoBehaviour
{
    public float speed = 100f;
    public GameObject bullet;
    public Transform hand;
    






    public void OnActivate()
    {
        RaycastHit Hit;
        if(Physics.Raycast(hand.transform.position,hand.transform.forward,out Hit,100f))
        {
            Instantiate(bullet, Hit.point, Quaternion.LookRotation(Hit.normal));
            bullet.GetComponent<Rigidbody>().AddForce(hand.transform.up*speed);
            Debug.Log(Hit.transform.name);
        }



        //Debug.Log("Fired");
        
    }




}
