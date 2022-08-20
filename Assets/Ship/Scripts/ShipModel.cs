using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipModel
{
    public Action<Coordinates2D> PositionChanged;
    public Action<int> AngleChanged;

    public ShipModel()
    {
        SystemEventManager.GetInstance().Subscribe("shipTransformChanged", GetTransform);
    }

    public void GetTransform()
    {
        var transform = ComponentManager.GetInstance().GetComponent<Transform>("ship").Position;
        Debug.Log(transform);
        PositionChanged?.Invoke(transform);
    }

    public void GetAngle(int angle)
    {
        AngleChanged?.Invoke(angle);
    }
}
