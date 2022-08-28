using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : IComponent
{
    public PlayerComponent()
    {
        Inputs = new List<string>();
    }
    public readonly List<string> Inputs;
}
