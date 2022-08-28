public class GamePresenter
{
    private readonly GameView _gameView;

    private readonly GameModel _gameModel;

    public GamePresenter(GameView gameView, GameModel gameModel)
    {
        _gameView = gameView;
        _gameModel = gameModel;
        Enable();
    }

    private void Enable()
    {
        _gameView.GameUpdateAction += GameUpdate;
        _gameModel.ObjectDestroyedAction += DestroyObject;
        _gameModel.CreatedBigAsteroidAction += CreateBigAsteroid;
        _gameModel.CreatedLittleAsteroidAction += CreateLittleAsteroid;
        _gameModel.CreatedUfoAction += CreateUfo;
        _gameModel.BulletEntityCreatedAction += CreateBullet;
        _gameModel.LaserCreatedAction += CreateLaser;
        _gameModel.ChangedTransformOfObjectAction += ChangeTransformOfGameObject;
        _gameModel.CreatedPlayerAction += CreatePlayer;
        _gameModel.LaserNumberOfShootsChangedAction += ChangeLaserChargesCountText;
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

    private void CreateUfo(Coordinates2D position)
    {
        _gameView.CreateUfo(position);
    }
    
    private void CreateBullet(string name, Coordinates2D position, float angle)
    {
        _gameView.CreateBullet(name, position, angle);
    }

    private void CreateLaser(Coordinates2D position, float angle)
    {
        _gameView.CreateLaser(position, angle);
    }

    private void CreatePlayer()
    {
        _gameView.CreatePlayer();
    }

    private void StartGame()
    {
        _gameModel.StartGame();
    }
    
    private void ChangeLaserChargesCountText(int chargesCount)
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
