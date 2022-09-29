using UnityEngine;

public class CountingObstacle : MonoBehaviour
{
    private CountingObstacleManager _countObstacleManager;

    private bool counted;
    
    private GameObject _PFX;
    
    private ObjectPoolerManager _poolerManager;

    private void Awake()
    {
        _poolerManager = ObjectPoolerManager.Instance;
        _countObstacleManager = CountingObstacleManager.Instance;
        _countObstacleManager.IncreaseTotalObstacleCount(1);
    }

    private void Start()
    {
        SetSliderValue();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sea") && !counted)
        {
            _countObstacleManager.IncreaseDroppedObstacleCount(1);
            SetSliderValue();
            counted = true;
            PlayParticleFx();
            Destroy(gameObject,1f);
        }
    }

    private void PlayParticleFx()
    {
        _PFX = _poolerManager.GetWaterFromPool();
        _PFX.transform.position = transform.position;
        Invoke("KillShape",.5f);
    }

    private void KillShape()
    {
        _poolerManager.KillWaterGameObject(_PFX);
    }

    private void SetSliderValue()
    {
        EventManager.UpdateSliderValue((float)_countObstacleManager.DroppedObstacleCount /(float)
                                       _countObstacleManager.TotalObstacleCount);
    }
}