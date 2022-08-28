using System;

public class LaserModel
{
    public Action<Coordinates2D, float> LaserTransformChangedAction;

    public LaserModel()
    {
        SystemManager.Instance.GetSystem<LaserSystem>().LaserTransformChangedAction += LaserTransformChanged;
    }

    private void LaserTransformChanged(Coordinates2D position, float angle)
    {
        LaserTransformChangedAction?.Invoke(position, angle);
    }
    
    public void OnDestroy()
    {
        SystemManager.Instance.GetSystem<LaserSystem>().LaserTransformChangedAction -= LaserTransformChanged;
    }
}
