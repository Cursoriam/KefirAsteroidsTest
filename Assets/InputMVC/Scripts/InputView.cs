using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputView : MonoBehaviour
{
    public Action<string> SendInput;
    
    public void GetPlayerInput()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            SendInput?.Invoke("accelerate");
        
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            SendInput?.Invoke("rotateLeft");
        
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            SendInput?.Invoke("rotateRight");
        
        if(Input.GetKeyDown(KeyCode.Space))
            SendInput?.Invoke("shoot");
    }
}
