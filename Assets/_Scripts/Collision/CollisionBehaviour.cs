using UnityEngine;

public class CollisionBehaviour : MonoBehaviour, ICollidible
{
    [HideInInspector] public Rigidbody _rb;
    [HideInInspector] public GameManager _gm;

    [HideInInspector] public float _explosionRadius = 5;
    [HideInInspector] public float _explosionForce = 5;
    [HideInInspector] public float _explosionUpwardModifier = 5;


    private void OnEnable()
    {
        EventManager.OnCollideWithObstacles += OpenUseGravity;
    }

    private void OnDisable()
    {
        EventManager.OnCollideWithObstacles -= OpenUseGravity;
    }

    private void Start()
    {
        GetValues();
    }

    private void GetValues()
    {
        _rb = GetComponent<Rigidbody>();
        _gm = GameManager.Instance;

        _explosionRadius = _gm.ExplosionRadius;
        _explosionForce = _gm.ExplosionForce;
        _explosionUpwardModifier = _gm.ExplosionUpwardModifier;
    }

    private void OpenUseGravity()
    {
        _rb.useGravity = true;
    }


    public void OnCollide(Transform comingTransform)
    {
        KnockBack(comingTransform);
        transform.SetParent(null);
    }

    protected void KnockBack(Transform comingTransform)
    {
        Collider[] colliders = Physics.OverlapSphere(comingTransform.position, _explosionRadius);

        foreach (var col in colliders)
        {
            var rb = col.GetComponent<Rigidbody>();
            var collidible = col.GetComponent<ICollidible>();

            if (rb != null && collidible != null)
            {
                rb.useGravity = true;
                rb.AddExplosionForce(_explosionForce, comingTransform.position, _explosionRadius,
                    _explosionUpwardModifier, ForceMode.Impulse);
                //col.transform.SetParent(null);
            }
        }
    }
}