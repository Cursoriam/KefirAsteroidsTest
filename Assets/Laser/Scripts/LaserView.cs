using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserView : MonoBehaviour
{
    public Action LaserDestroyed;
    private LaserView _laserView;
    private LaserPresenter _laserPresenter;
    private LaserModel _laserModel;
    void Awake()
    {
        _laserView = this;
        _laserModel = new LaserModel();
        _laserPresenter = new LaserPresenter(_laserView, _laserModel);
    }

    public void ChangeTransform(Coordinates2D position, float angle)
    {
        GetComponent<Transform>().position = new Vector2(position.X, position.Y);
        GetComponent<Transform>().rotation = Quaternion.Euler(Constants.FloatZero, Constants.FloatZero, angle);
    }
    
    private void OnDestroy()
    {
        LaserDestroyed?.Invoke();
    }
}
