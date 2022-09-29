using UnityEngine;

public class BombCollisionBehaviour : CollisionBehaviour, ICollidible
{
    private bool exploded;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _gm = GameManager.Instance;

        ChangeValuesForBomb();
    }

    private void ChangeValuesForBomb()
    {
        this._explosionForce = _gm.BombExplosionForce;
        this._explosionRadius = _gm.BombExplosionRadius;
        this._explosionUpwardModifier = _gm.BombExplosionUpwardModifier;
    }

    public new void OnCollide(Transform comingTransform)
    {
        exploded = true;
        KnockBack(comingTransform);
        
        Collider[] colliders = Physics.OverlapSphere(comingTransform.position, _explosionRadius);

        for (int i = colliders.Length-1; i >= 0; i--)
        {
            var bomb = colliders[i].GetComponent<BombCollisionBehaviour>();

            if (bomb != null && !bomb.exploded)
            {
                bomb.OnCollide(transform);
            }
        }
        Destroy(gameObject);
    }


}
