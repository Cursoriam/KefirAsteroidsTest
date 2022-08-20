using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePresenter
{
    private GameView _gameView;

    private GameModel _gameModel;

    public GamePresenter(GameView gameView, GameModel gameModel)
    {
        _gameView = gameView;
        _gameModel = gameModel;
        Enable();
    }

    private void Enable()
    {
        _gameView.GameUpdateAction += GameUpdate;
    }

    private void GameUpdate()
    {
        _gameModel.Update();
    }
}
