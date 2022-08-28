using System;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public Action GameUpdateAction;
    public Action GameStartAction;
    public Action GameRestartAction;
    private GameView _gameView;
    private GamePresenter _gamePresenter;
    private GameModel _gameModel;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject bigAsteroidPrefab;
    [SerializeField] private GameObject littleAsteroidPrefab;
    [SerializeField] private GameObject ufoPrefab;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject uiElementsBeforeGameStarted;
    [SerializeField] private GameObject uiStats;
    [SerializeField] private Text laserChargesCountText;
    [SerializeField] private Text laserReloadTimeText;
    [SerializeField] private GameObject uiElementsBeforeGameRestarted;
    [SerializeField] private Text scoreText;

    private void Awake()
    {
        InitializeVariables();
        _gameView = this;
        _gameModel = new GameModel();
        _gamePresenter = new GamePresenter(_gameView, _gameModel);
    }

    public void StartGame()
    {
        uiElementsBeforeGameStarted.SetActive(false);
        uiStats.SetActive(true);
        scoreText.gameObject.SetActive(true);
        laserChargesCountText.text = Constants.LaserChargesCountText + Constants.NumberOfLaserShoots;
        laserReloadTimeText.text = Constants.LaserCoolDownText + Constants.LaserReloadTime;
        GameStartAction?.Invoke();
    }

    public void RestartGame()
    {
        uiElementsBeforeGameRestarted.SetActive(false);
        scoreText.text = Constants.IntZero.ToString();
        GameRestartAction?.Invoke();
    }

    private void InitializeVariables()
    {
        if (Camera.main is { })
        {
            Constants.ScreenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,
                Screen.height)).x;
            Constants.ScreenHeight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,
                Screen.height)).y;
        }

        Constants.PlayerSize = GetSpriteSize(playerPrefab);
        Constants.BulletSize = GetSpriteSize(bullet);
        Constants.LaserSize = GetSpriteSize(laser.GetComponent<Transform>().GetChild(0).gameObject);
        Constants.BigAsteroidSize = GetSpriteSize(bigAsteroidPrefab);
        Constants.LittleAsteroidSize = GetSpriteSize(littleAsteroidPrefab);
        Constants.UfoSize = GetSpriteSize(ufoPrefab);
    }
    
    private Coordinates2D GetSpriteSize(GameObject prefab)
    {
        var size = new Coordinates2D(prefab.GetComponent<SpriteRenderer>().bounds.size.x,
            prefab.GetComponent<SpriteRenderer>().bounds.size.y);
        
        return size;
    }

    private void Update()
    {
        GameUpdate();
    }

    // Update is called once per frame
    private void GameUpdate()
    {
        GameUpdateAction?.Invoke();
    }

    public void ChangeTransformOfGameObject(string gameObjectName, Coordinates2D position, Coordinates2D size,
        float angle)
    {
        var gameObjectWithName = GameObject.Find(gameObjectName);
        gameObjectWithName.transform.position = new Vector2(position.X, position.Y);
        gameObjectWithName.transform.rotation = Quaternion.Euler(Constants.FloatZero, Constants.FloatZero, angle);
    }
    
    public void DestroyObjectOnScene(string gameObjectName)
    {
        Destroy(GameObject.Find(gameObjectName));
    }

    public void CreatePlayer()
    {
        var player = Instantiate(playerPrefab);
        player.name = player.name.Replace("(Clone)", "");
    }

    public void CreateBigAsteroid(string asteroidName, Coordinates2D position, float angle)
    {
        bigAsteroidPrefab.name = asteroidName;
        var asteroid = Instantiate(bigAsteroidPrefab, new Vector3(position.X, position.Y),
            Quaternion.identity);
        asteroid.name = asteroid.name.Replace("(Clone)", "");
        asteroid.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }

    public void CreateLittleAsteroid(string asteroidName, Coordinates2D position, float angle)
    {
        littleAsteroidPrefab.name = asteroidName;
        var asteroid = Instantiate(littleAsteroidPrefab, new Vector3(position.X, position.Y),
            Quaternion.identity);
        asteroid.name = asteroid.name.Replace("(Clone)", "");
        asteroid.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }

    public void CreateUfo(Coordinates2D position)
    {
        var ufo = Instantiate(ufoPrefab, new Vector2(position.X, position.Y),
            Quaternion.identity);
        ufo.name = Constants.UfoEntityName;
    }
    
    public void CreateBullet(string bulletName, Coordinates2D position, float angle)
    {
        bullet.name = bulletName;
        var bulletObject = Instantiate(bullet, new Vector2(position.X, position.Y),
            Quaternion.Euler(Constants.FloatZero, Constants.FloatZero, angle));
        bulletObject.name = bulletObject.name.Replace("(Clone)", "");
    }

    public void CreateLaser(Coordinates2D position, float angle)
    {
        var laserObject = Instantiate(laser, new Vector3(position.X, position.Y), Quaternion.identity);
        laserObject.name = laserObject.name.Replace("(Clone)", "");
        laserObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }
    
    public void ChangeLaserChargesCount(int charges)
    {
        laserChargesCountText.text = Constants.LaserChargesCountText + charges;
    }

    public void ChangeLaserReloadTime(float reloadTime)
    {
        laserReloadTimeText.text = Constants.LaserCoolDownText + $"{reloadTime:0.##}";
    }

    public void GameOver()
    {
        uiElementsBeforeGameRestarted.SetActive(true);
    }

    public void IncreaseScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
