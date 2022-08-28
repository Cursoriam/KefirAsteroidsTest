using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Utilities
{
    /// <summary>
    /// Rotates one point around another
    /// </summary>
    /// <param name="pointToRotate">The point to rotate.</param>
    /// <param name="centerPoint">The center point of rotation.</param>
    /// <param name="angleInDegrees">The rotation angle in degrees.</param>
    /// <returns>Rotated point</returns>
    public static Coordinates2D RotatePoint(Coordinates2D pointToRotate, Coordinates2D centerPoint, double angleInDegrees)
    {
        double angleInRadians = angleInDegrees * (Math.PI / 180);
        double cosTheta = Math.Cos(angleInRadians);
        double sinTheta = Math.Sin(angleInRadians);
        Coordinates2D newPoint;
        newPoint.X = (float)(cosTheta * (pointToRotate.X - centerPoint.X) - sinTheta * (pointToRotate.Y - centerPoint.Y)
                             + centerPoint.X);
        newPoint.Y = (float) (sinTheta * (pointToRotate.X - centerPoint.X) + cosTheta * (pointToRotate.Y - centerPoint.Y)
                                                                           + centerPoint.Y);
        return newPoint;
    }
    
    public static bool IsPolygonsIntersecting(List<Coordinates2D> a, List<Coordinates2D> b)
    {
        foreach (var polygon in new[] { a, b })
        {
            for (int i1 = 0; i1 < polygon.Count; i1++)
            {
                int i2 = (i1 + 1) % polygon.Count;
                var p1 = polygon[i1];
                var p2 = polygon[i2];

                Coordinates2D normal;
                normal.X = p2.Y - p1.Y;
                normal.Y = p1.X - p2.X;

                double? minA = null, maxA = null;
                foreach (var p in a)
                {
                    var projected = normal.X * p.X + normal.Y * p.Y;
                    if (minA == null || projected < minA)
                        minA = projected;
                    if (maxA == null || projected > maxA)
                        maxA = projected;
                }

                double? minB = null, maxB = null;
                foreach (var p in b)
                {
                    var projected = normal.X * p.X + normal.Y * p.Y;
                    if (minB == null || projected < minB)
                        minB = projected;
                    if (maxB == null || projected > maxB)
                        maxB = projected;
                }

                if (maxA < minB || maxB < minA)
                    return false;
            }
        }
        return true;
    }
    
    public static Coordinates2D CalculateEnemySpawnPosition()
    {
        Coordinates2D position;
        Coordinates2D tenth;
        tenth.X = Constants.ScreenWidth * 0.2f;
        tenth.Y = Constants.ScreenHeight * 0.2f;
        float leftRandomPosition = (float) new Random().NextDouble() * ((-Constants.ScreenWidth + tenth.X) - 
                                                                        (-Constants.ScreenWidth)) - Constants.ScreenWidth;
        float rightRandomPosition = (float) new Random().NextDouble() * (Constants.ScreenWidth - 
                                                                         (Constants.ScreenWidth - tenth.X)) + 
                                    (Constants.ScreenWidth - tenth.X);
        float upperRandomPosition = (float) new Random().NextDouble() * (Constants.ScreenHeight - 
                                                                         (Constants.ScreenHeight - tenth.Y)) + 
                                    (Constants.ScreenHeight - tenth.Y);
        float lowerRandomPosition = (float) new Random().NextDouble() * ((-Constants.ScreenHeight + tenth.Y) - 
                                                                         (-Constants.ScreenHeight)) - Constants.ScreenHeight;
        position.X = (new Random().Next(0, 2) == 0) ? leftRandomPosition : rightRandomPosition;
        position.Y = (new Random().Next(0, 2) == 0) ? lowerRandomPosition : upperRandomPosition;
        return position;
    }

    public static bool IsEmpty<T>(List<T> list)
    {
        if (list == null)
        {
            return true;
        }

        return list.Count == Constants.NumberOfElementsInEmptyList;
    }

    public static bool IsNegative(float value)
    {
        return value < Constants.FloatZero;
    }
    
    public static bool ContainsInput(List<string> inputs, string actionName)
    {
        return inputs.Any(input => input == actionName);
    }

    public static float DegreesToRadians(float angle)
    {
        return (float)(Math.PI * angle / Constants.PIInDegrees);
    }

    public static float RadiansToDegrees(float angle)
    {
        return (float)(angle * Constants.PIInDegrees / Math.PI);
    }
    
    public static List<Coordinates2D> GetPrimitiveConvexHull(Coordinates2D position, Coordinates2D size, float angle)
    {
        var lowerLeftVertex = new Coordinates2D(position.X - size.X/Constants.FloatTwo,
            position.Y - size.Y/Constants.FloatTwo);
        var upperLeftVertex = new Coordinates2D(position.X - size.X/Constants.FloatTwo,
            position.Y + size.Y/Constants.FloatTwo);
        var upperRightVertex = new Coordinates2D(position.X + size.X / Constants.FloatTwo,
            position.Y + size.Y / Constants.FloatTwo);
        var lowerRightVertex = new Coordinates2D(position.X + size.X / Constants.FloatTwo,
            position.Y - size.Y / Constants.FloatTwo);

        var vertices = new List<Coordinates2D>{lowerLeftVertex, upperLeftVertex, upperRightVertex, lowerRightVertex};

        for (var i = 0; i < vertices.Count; i++)
        {
            vertices[i] = Utilities.RotatePoint(vertices[i], position, angle);
        }
        return vertices;
    }

    public static string GetPlayerCoordinatesText(Coordinates2D position)
    {
        return $"Player Coordinates: ({position.X:0.#}, {position.Y:0.#})";
    }
    
    private void DrawPrimitiveConvexHull(Coordinates2D position, Coordinates2D size, float angle)
    {
        var vertices = GetPrimitiveConvexHull(position, size, angle);
        Debug.DrawLine(new Vector2(vertices[0].X, vertices[0].Y), new Vector2(vertices[1].X,
            vertices[1].Y));
        Debug.DrawLine(new Vector2(vertices[1].X, vertices[1].Y), new Vector2(vertices[2].X,
            vertices[2].Y));
        Debug.DrawLine(new Vector2(vertices[2].X, vertices[2].Y), new Vector2(vertices[3].X,
            vertices[3].Y));
        Debug.DrawLine(new Vector2(vertices[3].X, vertices[3].Y), new Vector2(vertices[0].X,
            vertices[0].Y));
    }
}
