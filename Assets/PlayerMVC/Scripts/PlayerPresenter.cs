public class PlayerPresenter
{
    private readonly PlayerView _playerView;
    private readonly PlayerModel _playerModel;

    public PlayerPresenter(PlayerView playerView, PlayerModel playerModel)
    {
        _playerView = playerView;
        _playerModel = playerModel;
        Enable();
    }
    
    private void  Enable()
    {
        _playerView.SendInput += InputSent;
        _playerModel.GetPlayerTransformAction += SetPlayerParams;
        _playerModel.GetPlayerVelocityAction += SetPlayerVelocity;
    }

    private void InputSent(string input)
    {
        _playerModel.SendInput(input);
    }

    private void SetPlayerParams(Coordinates2D position, float angle)
    {
        _playerView.SetPlayerParams(position, angle);
    }

    private void SetPlayerVelocity(float velocity)
    {
        _playerView.SetPlayerVelocity(velocity);
    }
}
