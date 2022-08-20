using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModel
{
    public InputModel()
    {
        SystemManager.GetInstance().AddSystem(new InputSystem());
    }

    public void SendInput(string input)
    {
        SystemManager.GetInstance().GetSystem<InputSystem>().AddInput(input);
    }
}
