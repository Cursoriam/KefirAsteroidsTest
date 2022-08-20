using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEntity : Entity
{
    public InputEntity(): base(){}
    public InputEntity(string entityId) : base(entityId)
    {
        _components.Add(new InputComponent());
    }
}
