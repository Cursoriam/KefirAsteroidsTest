using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserModel
{
    public Action<Coordinates2D, float> LaserTransformChangedAction;

    public LaserModel()
    {
        SystemManager.Instance.GetSystem<LaserSystem>().LaserTransformChanged += LaserTransformChanged;
    }

    private void LaserTransformChanged(Coordinates2D position, float angle)
    {
        LaserTransformChangedAction?.Invoke(position, angle);
    }
    
    public void OnDestroy()
    {
        SystemManager.Instance.GetSystem<LaserSystem>().LaserTransformChanged -= LaserTransformChanged;
    }
}
