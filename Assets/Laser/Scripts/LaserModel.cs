using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserModel
{
    public Action<float> LaserTransformChangedAction;

    public LaserModel()
    {
        SystemManager.Instance.GetSystem<LaserSystem>().LaserTransformChanged += LaserTransformChanged;
    }

    private void LaserTransformChanged(float angle)
    {
        LaserTransformChangedAction?.Invoke(angle);
    }
    
    public void OnDestroy()
    {
        SystemManager.Instance.GetSystem<LaserSystem>().LaserTransformChanged -= LaserTransformChanged;
    }
}
