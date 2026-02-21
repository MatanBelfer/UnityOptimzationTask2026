
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerCharacterController playerCharacterController;
    public Collider playerCharacterCollider ; 

    [SerializeField] private FireHazardScriptableObject[] fireHazardScriptableObjects;
    [SerializeField] private FireHazard[] fireHazards;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
  
    }

    private void Start()
    {
        foreach (FireHazard fireHazard in fireHazards)
        {
            fireHazard.fireHazardData =
                fireHazardScriptableObjects[Random.Range(0, fireHazardScriptableObjects.Length)];
            fireHazard.onCharacterEnteredAction += HandleCharacterEnteredFire;
        }
    }

    public void HandleCharacterEnteredFire(FireEnteredEventArgs args)
    {
        args.targetCharacterController.TakeDamage(args.damageDealt);
    }
}