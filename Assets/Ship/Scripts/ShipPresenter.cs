using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipPresenter
{
    private ShipView _shipView;
    private ShipModel _shipModel;

    public ShipPresenter(ShipView shipView, ShipModel shipModel)
    {
        _shipView = shipView;
        _shipModel = shipModel;
        Enable();
    }
    
    private void  Enable()
    {
        _shipModel.PositionChanged += PositionChanged;
        _shipModel.AngleChanged += AngleChanged;
    }

    private void PositionChanged(Coordinates2D position)
    {
        _shipView.ChangePosition(position);
    }

    private void AngleChanged(int angle)
    {
        _shipView.Rotate(angle);
    }
}
