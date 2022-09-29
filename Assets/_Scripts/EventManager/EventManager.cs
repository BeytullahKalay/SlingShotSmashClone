using System;
using UnityEngine;

public static class EventManager
{
    public static Action OnCollideWithObstacles;
    public static Action<float> UpdateSliderValue;
    public static Action<int> UpdateShotLeft;
    public static Action MoveNextManToSling;
    public static Action <Transform> ApplyNextManToSling;
    public static Action OnGameStart;
    public static Action OnGameFinished;
}
