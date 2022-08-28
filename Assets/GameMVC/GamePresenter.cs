using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        _gameModel.DestroyObjectAction += DestroyObject;
        _gameModel.CreatedBigAsteroidAction += CreateBigAsteroid;
        _gameModel.CreatedLittleAsteroidAction += CreateLittleAsteroid;
        _gameModel.CreatedUfoAction += CreateUFO;
        _gameModel.BulletEntityCreated += BulletCreated;
        _gameModel.CreateLaserAction += CreateLaser;
        _gameModel.ChangedTransformOfObjectAction += ChangeTransformOfGameObject;
        _gameModel.CreatedPlayerAction += PlayerCreated;
        _gameModel.LaserNumberOfShootsChangedAction += LaserNumberOfShootsChangeText;
        _gameModel.LaserReloadTimeChangedAction += ChangeLaserReloadTime;
        _gameModel.SendScoreAction += IncreaseScore;
        _gameModel.GameOverAction += OverGame;
        _gameView.GameStartAction += StartGame;
        _gameView.GameRestartAction += RestartGame;
    }

    private void ChangeTransformOfGameObject(string name, Coordinates2D position, Coordinates2D size, float angle)
    {
        _gameView.ChangeTransformOfGameObject(name, position, size, angle);
    }
    
    private void GameUpdate()
    {
        _gameModel.Update();
    }

    private void DestroyObject(string name)
    {
        _gameView.DestroyObjectOnScene(name);
    }

    private void CreateBigAsteroid(string name, Coordinates2D position, float angle)
    {
        _gameView.CreateBigAsteroid(name, position, angle);
    }

    private void CreateLittleAsteroid(string name, Coordinates2D position, float angle)
    {
        _gameView.CreateLittleAsteroid(name, position, angle);
    }

    private void CreateUFO(Coordinates2D position)
    {
        _gameView.CreateUfo(position);
    }
    
    private void BulletCreated(string name, Coordinates2D position, float angle)
    {
        _gameView.CreateBullet(name, position, angle);
    }

    private void CreateLaser(Coordinates2D position, float angle)
    {
        _gameView.CreateLaser(position, angle);
    }

    private void PlayerCreated()
    {
        _gameView.CreatePlayer();
    }

    private void StartGame()
    {
        _gameModel.StartGame();
    }
    
    private void LaserNumberOfShootsChangeText(int chargesCount)
    {
        _gameView.ChangeLaserChargesCount(chargesCount);
    }

    private void ChangeLaserReloadTime(float reloadTime)
    {
        _gameView.ChangeLaserReloadTime(reloadTime);
    }

    private void IncreaseScore(int score)
    {
        _gameView.IncreaseScore(score);
    }

    private void OverGame()
    {
        _gameView.GameOver();
    }

    private void RestartGame()
    {
        _gameModel.RestartGame();
    }
}
