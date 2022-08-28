using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Constants
{
    //Constants
    public const float FloatComparisionPrecision = 0.0001f;
    private const float FloatBaseValue = -1;
    private const  int AccessorCharacterNumberToDeleteFromPropertyName = 4;
    public const int NumberOfElementsInEmptyList = 0;
    public const float RotationSpeed = 0.4f;
    public const double PIInDegrees = 180.0;
    public const int IntZero = 0;
    public const float FloatZero = 0.0f;
    public const float FloatTwo = 2.0f;
    public const string BulletName = "Bullet";
    public const float UpdateStep = 0.001f;
    public const float BulletLifeTime = 1.0f;
    public const string LaserName = "Laser";
    public const int NumberOfLaserShoots = 3;
    public const float LaserLifeTime = 0.5f;
    public const float LaserReloadTime = 20f;
    public const float CoolDownBetweenAsteroidSpawn = 2.0f;
    public const int MaxAsteroidsOnScene = 5;
    public const float CoolDownBetweenUfoSpawn = 5.0f;
    public const string UfoEntityName = "Ufo";
    public const string PlayerEntityName = "Player";
    public const string AsteroidEntityName = "Asteroid";
    public const float BigAsteroidVelocity = 0.001f;
    public const float LittleAsteroidVelocity = 0.004f;
    public const float BulletVelocity = 0.03f;
    public const float UfoVelocity = 0.004f;
    public const float PlayerAcceleration = -0.00002f;
    public const float MaxPlayerVelocity = 0.01f;
    public const string PlayerAngleText = "Player Rotation Angle: ";
    public const string PlayerCoordinatesObjectName = "PlayerCoordinates";
    public const string PlayerAngleObjectName = "PlayerRotationAngle";
    public const string LaserChargesCountText = "Laser Charges: ";
    public const string LaserCoolDownText = "Laser Cooldown: ";
    public const string PlayerVelocityTextObjectName = "PlayerVelocity";
    public const string PlayerVelocityText = "Player Velocity: ";
    public const int BigAsteroidScore = 100;
    public const int LittleAsteroidScore = 50;
    public const int UfoScore = 200;

    //Systems actions names
    public const string AccelerateAction = "accelerate";
    public const string RotateLeftAction = "rotateLeft";
    public const string RotateRightAction = "rotateRight";
    public const string ShootAction = "shoot";
    public const string LaserAction = "laser";
    
    //Variables that need to be initialized before the game starts
    private static float _screenWidth = -1;
    private static float _screenHeight = -1;

    public static readonly Coordinates2D BaseCoordinates2DValue = new Coordinates2D(0, 0);
    private static Coordinates2D _playerSize = BaseCoordinates2DValue;
    private static Coordinates2D _bulletSize = BaseCoordinates2DValue;
    private static Coordinates2D _laserSize = BaseCoordinates2DValue;
    private static Coordinates2D _bigAsteroidSize = BaseCoordinates2DValue;
    private static Coordinates2D _littleAsteroidSize = BaseCoordinates2DValue;
    private static Coordinates2D _ufoSize = BaseCoordinates2DValue;

    private static void SetProperty(ref float variable, float value, string propertyName)
    {
        if (Math.Abs(variable - FloatBaseValue) > FloatComparisionPrecision)
        {
            throw new PropertyHasAlreadyBeenInitializedException(propertyName);
        }

        variable = value;
    }

    private static float GetProperty(float variable, string propertyName)
    {
        if (Math.Abs(variable - FloatBaseValue) < FloatComparisionPrecision)
        {
            throw new PropertyHasNotBeenInitializedException(propertyName);
        }

        return variable;
    }

    private static void SetProperty(ref Coordinates2D variable, Coordinates2D value, string propertyName)
    {
        if (variable != BaseCoordinates2DValue)
        {
            throw new PropertyHasAlreadyBeenInitializedException(propertyName);
        }

        variable = value;
    }

    private static Coordinates2D GetProperty(Coordinates2D variable, string propertyName)
    {
        if (variable == BaseCoordinates2DValue)
        {
            throw new PropertyHasNotBeenInitializedException(propertyName);
        }

        return variable;
    }
    
    public static float ScreenWidth
    {
        set => SetProperty(ref _screenWidth, value,
            MethodBase.GetCurrentMethod().Name.Substring(AccessorCharacterNumberToDeleteFromPropertyName));
        get => GetProperty(_screenWidth, 
            MethodBase.GetCurrentMethod().Name.Substring(AccessorCharacterNumberToDeleteFromPropertyName));
    }

    public static float ScreenHeight
    {
        set => SetProperty(ref _screenHeight, value,
            MethodBase.GetCurrentMethod().Name.Substring(AccessorCharacterNumberToDeleteFromPropertyName));
        get => GetProperty(_screenHeight,
            MethodBase.GetCurrentMethod().Name.Substring(AccessorCharacterNumberToDeleteFromPropertyName));
    }

    public static Coordinates2D PlayerSize
    {
        set => SetProperty(ref _playerSize, value,
            MethodBase.GetCurrentMethod().Name.Substring(AccessorCharacterNumberToDeleteFromPropertyName));
        get => GetProperty(_playerSize,
            MethodBase.GetCurrentMethod().Name.Substring(AccessorCharacterNumberToDeleteFromPropertyName));
    }

    public static Coordinates2D BulletSize
    {
        set => SetProperty(ref _bulletSize, value,
            MethodBase.GetCurrentMethod().Name.Substring(AccessorCharacterNumberToDeleteFromPropertyName));
        get => GetProperty(_bulletSize,
            MethodBase.GetCurrentMethod().Name.Substring(AccessorCharacterNumberToDeleteFromPropertyName));
    }
    
    public static Coordinates2D LaserSize
    {
        set => SetProperty(ref _laserSize, value,
            MethodBase.GetCurrentMethod().Name.Substring(AccessorCharacterNumberToDeleteFromPropertyName));
        get => GetProperty(_laserSize,
            MethodBase.GetCurrentMethod().Name.Substring(AccessorCharacterNumberToDeleteFromPropertyName));
    }
    
    public static Coordinates2D BigAsteroidSize
    {
        set => SetProperty(ref _bigAsteroidSize, value,
            MethodBase.GetCurrentMethod().Name.Substring(AccessorCharacterNumberToDeleteFromPropertyName));
        get => GetProperty(_bigAsteroidSize,
            MethodBase.GetCurrentMethod().Name.Substring(AccessorCharacterNumberToDeleteFromPropertyName));
    }
    
    public static Coordinates2D LittleAsteroidSize
    {
        set => SetProperty(ref _littleAsteroidSize, value,
            MethodBase.GetCurrentMethod().Name.Substring(AccessorCharacterNumberToDeleteFromPropertyName));
        get => GetProperty(_littleAsteroidSize,
            MethodBase.GetCurrentMethod().Name.Substring(AccessorCharacterNumberToDeleteFromPropertyName));
    }
    
    public static Coordinates2D UfoSize
    {
        set => SetProperty(ref _ufoSize, value,
            MethodBase.GetCurrentMethod().Name.Substring(AccessorCharacterNumberToDeleteFromPropertyName));
        get => GetProperty(_ufoSize,
            MethodBase.GetCurrentMethod().Name.Substring(AccessorCharacterNumberToDeleteFromPropertyName));
    }
}
