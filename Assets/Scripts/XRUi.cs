using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class XRUi : MonoBehaviour
{
    private TextMeshProUGUI text;
    public int OnTarget = 0;
    [SerializeField] private GameObject _sign;
    private void Start()
    {
        _sign = transform.gameObject;
        text = _sign.GetComponent<TextMeshProUGUI>();
        text.text = "Can Shoot!";
    }

    private void Update()
    {
        ChangeText();
    }


    public void ChangeText()
    {

        text.text = "Score: " + OnTarget;
    }

    public void CanFellOver()
    {
        OnTarget++;
    }


    
}
