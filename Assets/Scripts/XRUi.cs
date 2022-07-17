using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class XRUi : MonoBehaviour
{
    private TextMeshProUGUI text;
    public int NumberOfCans=6;
    public int NumberOfThrows = 3;
    [SerializeField] private GameObject _sign;
    private void Start()
    {
        _sign = transform.gameObject;
        text = _sign.GetComponent<TextMeshProUGUI>();
        text.text = "Can Toss!";
    }

    private void Update()
    {
        ChangeText();
    }


    public void ChangeText()
    {

        if (NumberOfCans == 6 && NumberOfThrows>1)
        {
            text.text = "Can Toss!";
            
        }
        else if (NumberOfCans<1)
        {
            text.fontSize = 26;
            text.text = "Congratulation you Won!";
        }
        else if(NumberOfCans>=1 && NumberOfThrows<1)
        {
            text.text = "Sorry you Lost!";
        }
    }

    public void CanFellOver()
    {
        NumberOfCans--;
    }

    public void ThrowCount()
    {
        NumberOfThrows--;
    }
    
}
