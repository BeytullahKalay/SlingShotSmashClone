using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Position Values")] [SerializeField]
    private Transform manSlingPosStopPos;
    
    [Header("Launch Values")]
    [SerializeField] private float force= 5;
    [SerializeField] private float decreaseDirectionY = 2f;
    [SerializeField] private float slingResetSpeed = 7;

    [Header("Collision Knockback")]
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float explosionForce = 5f;
    [SerializeField] private float explosionUpwardModifier = 5f;
    
    [Header("Bomb Knockback")]
    [SerializeField] private float bombExplosionRadius = 5f;
    [SerializeField] private float bombExplosionForce = 5f;
    [SerializeField] private float bombExplosionUpwardModifier = 5f;

    [Header("Particles")]
    [SerializeField] private GameObject dustParticle;
    [SerializeField] private GameObject waterParticle;

    public float ExplosionRadius => explosionRadius;
    public float ExplosionForce => explosionForce;
    public float ExplosionUpwardModifier => explosionUpwardModifier;
    public float BombExplosionRadius => bombExplosionRadius;
    public float BombExplosionForce => bombExplosionForce;
    public float BombExplosionUpwardModifier => bombExplosionUpwardModifier;
    public float Force => force;
    public float SlingResetSpeed => slingResetSpeed;
    public float DecreaseDirectionY => decreaseDirectionY;

    public Transform ManSlingPosStopPos => manSlingPosStopPos;

    public GameObject DustParticle => dustParticle;
    public GameObject WaterParticle => waterParticle;
}
