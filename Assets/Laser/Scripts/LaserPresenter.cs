using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserPresenter
{
    private LaserView _laserView;
    private LaserModel _laserModel;

    public LaserPresenter(LaserView laserView, LaserModel laserModel)
    {
        _laserView = laserView;
        _laserModel = laserModel;
        Enable();
    }

    private void Enable()
    {
        _laserModel.LaserTransformChangedAction += LaserTransformChanged;
        _laserView.LaserDestroyed += DestroyLaser;
    }

    private void LaserTransformChanged(float angle)
    {
        _laserView.ChangeTransform(angle);
    }
    
    private void DestroyLaser()
    {
        _laserModel.OnDestroy();
    }
}
