public class LaserPresenter
{
    private readonly LaserView _laserView;
    private readonly LaserModel _laserModel;

    public LaserPresenter(LaserView laserView, LaserModel laserModel)
    {
        _laserView = laserView;
        _laserModel = laserModel;
        Enable();
    }

    private void Enable()
    {
        _laserModel.LaserTransformChangedAction += ChangeLaserTransform;
        _laserView.LaserDestroyed += DestroyLaser;
    }

    private void ChangeLaserTransform(Coordinates2D position, float angle)
    {
        _laserView.ChangeTransform(position, angle);
    }
    
    private void DestroyLaser()
    {
        _laserModel.OnDestroy();
    }
}
