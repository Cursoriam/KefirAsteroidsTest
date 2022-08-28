using System.Collections.Generic;

public class PlayerComponent : IComponent
{
    public PlayerComponent()
    {
        Inputs = new List<string>();
    }
    public readonly List<string> Inputs;
}
