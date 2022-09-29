using UnityEngine;

public class CountingObstacleManager : MonoSingleton<CountingObstacleManager>
{
    [SerializeField] private int totalObstacleCount;
    [SerializeField] private int droppedObstacleCount;

    public void IncreaseTotalObstacleCount(int amount)
    {
        totalObstacleCount += amount;
    }
    
    public void IncreaseDroppedObstacleCount(int amount)
    {
        droppedObstacleCount += amount;
    }

    public int DroppedObstacleCount => droppedObstacleCount;
    public int TotalObstacleCount => totalObstacleCount;
}
