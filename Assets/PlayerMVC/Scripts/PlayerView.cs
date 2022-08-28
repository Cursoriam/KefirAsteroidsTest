using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    private PlayerView _playerView;
    private PlayerPresenter _playerPresenter;
    private PlayerModel _playerModel;
    public Action<string> SendInput;
    private Text _playerCoordinatesText;
    private Text _playerAngleText;
    private Text _playerVelocityText;
    
    // Start is called before the first frame update
    private void Start()
    {
        _playerView = GetComponent<PlayerView>();
        _playerModel = new PlayerModel();
        _playerPresenter = new PlayerPresenter(_playerView, _playerModel);
        _playerCoordinatesText = GameObject.Find(Constants.PlayerCoordinatesObjectName).GetComponent<Text>();
        _playerAngleText = GameObject.Find(Constants.PlayerAngleObjectName).GetComponent<Text>();
        _playerVelocityText = GameObject.Find(Constants.PlayerVelocityTextObjectName).GetComponent<Text>();
    }

    private void GetInput()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            SendInput?.Invoke(Constants.AccelerateAction);
        
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            SendInput?.Invoke(Constants.RotateLeftAction);
        
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            SendInput?.Invoke(Constants.RotateRightAction);
        
        if(Input.GetKeyDown(KeyCode.Space))
            SendInput?.Invoke(Constants.ShootAction);
        
        if(Input.GetKeyDown(KeyCode.C))
            SendInput?.Invoke(Constants.LaserAction);
    }
    
    private void Update()
    {
        GetInput();
    }

    public void SetPlayerParams(Coordinates2D position, float angle)
    {
        _playerCoordinatesText.text = Utilities.GetPlayerCoordinatesText(position);
        _playerAngleText.text = Constants.PlayerAngleText + $"{angle:0.#}";
    }

    public void SetPlayerVelocity(float velocity)
    {
        _playerVelocityText.text = Constants.PlayerVelocityText + $"{velocity:0.######}";
    }

    private void OnDestroy()
    {
        if (_playerCoordinatesText != null)
        {
            _playerCoordinatesText.text = Utilities.GetPlayerCoordinatesText(Constants.BaseCoordinates2DValue);
        }

        if (_playerAngleText != null)
        {
            _playerAngleText.text = Constants.PlayerAngleText + $"{Constants.FloatZero:0.#}";
        }

        if (_playerVelocityText != null)
        {
            _playerVelocityText.text = Constants.PlayerVelocityText + $"{Constants.FloatZero:0.#}";
        }
    }
}
