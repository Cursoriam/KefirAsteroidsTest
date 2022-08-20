using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GamePresenter _gamePresenter;
    private GameModel _gameModel;
    private GameView _gameView;
    private InputPresenter _inputPresenter;
    private InputModel _inputModel;
    private InputView _inputView;
    private ShipView _shipView;
    private ShipPresenter _shipPresenter;
    private ShipModel _shipModel;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _gameModel = new GameModel();
        _gameView = GetComponent<GameView>();
        _gamePresenter = new GamePresenter(_gameView, _gameModel);
        _inputModel = new InputModel();
        _inputView = FindObjectOfType<InputView>();
        _inputPresenter = new InputPresenter(_inputView, _inputModel);
        _shipView = FindObjectOfType<ShipView>();
        _shipModel = new ShipModel();
        _shipPresenter = new ShipPresenter(_shipView, _shipModel);
    }

    // Update is called once per frame
    void Update()
    {
        _inputView.GetPlayerInput();
        _gameView.GameUpdate();
    }
}
