using UnityEngine;
using UnityEngine.Pool;

// Bu kisim kotu oldu

public class ObjectPoolerManager : MonoSingleton<ObjectPoolerManager>
{
    [SerializeField] private int spawnAmount = 20;
    [SerializeField] private int maxSpawnAmount = 35;
    
    private GameObject _dustParticlePFX;
    private GameObject _waterSplashPFX;
    
    private ObjectPool<GameObject> _dustPool;
    private ObjectPool<GameObject> _waterSplashPool;
    
    private void Start()
    {
        
        _waterSplashPFX = GameManager.Instance.WaterParticle;
        _waterSplashPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(_waterSplashPFX);
        }, shape =>
        {
            shape.gameObject.SetActive(true);
        }, shape =>
        {
            shape.gameObject.SetActive(false);  
        }, shape =>
        {
            Destroy(shape.gameObject);  
        },false,spawnAmount,maxSpawnAmount);
        
        
        
        _dustParticlePFX = GameManager.Instance.DustParticle;
        _dustPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(_dustParticlePFX);
        }, shape =>
        {
           shape.gameObject.SetActive(true);
        }, shape =>
        {
          shape.gameObject.SetActive(false);  
        }, shape =>
        {
          Destroy(shape.gameObject);  
        },false,20,35);
    }

    public GameObject GetDustFromPool()
    {
        return _dustPool.Get();
    }
    
    public GameObject GetWaterFromPool()
    {
        return _waterSplashPool.Get();
    }

    public void KillDustGameObject(GameObject objectToRelease)
    {
        _dustPool.Release(objectToRelease);
    }
    
    public void KillWaterGameObject(GameObject objectToRelease)
    {
        _waterSplashPool.Release(objectToRelease);
    }
}
