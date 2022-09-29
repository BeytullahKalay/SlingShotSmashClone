using UnityEngine;

public class ManCollisionBehaviour : MonoBehaviour
{
    private GameObject _PFX;

    private ObjectPoolerManager _poolerManager;

    private bool _isCollided;

    private void Start()
    {
        _poolerManager = ObjectPoolerManager.Instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var obj = collision.gameObject.GetComponent<ICollidible>();

        if (obj != null && !_isCollided)
        {
            _isCollided = true;
            obj.OnCollide(transform);
            EventManager.OnCollideWithObstacles();
            EventManager.MoveNextManToSling();
            PlayParticleFx();
            Destroy(this, 1.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sea") && !_isCollided)
        {
            _isCollided = true;
            EventManager.MoveNextManToSling();
            Destroy(this, 1.5f);
        }
    }

    private void PlayParticleFx()
    {
        _PFX = _poolerManager.GetDustFromPool();
        _PFX.transform.position = transform.position;
        Invoke("KillShape", .5f);
    }

    private void KillShape()
    {
        _poolerManager.KillDustGameObject(_PFX);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, GameManager.Instance.ExplosionRadius);
    }
}