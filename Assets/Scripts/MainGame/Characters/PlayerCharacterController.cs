using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class PlayerCharacterController : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    public event UnityAction<int> onTakeDamageEventAction;
    [SerializeField] private UnityEvent<int> onTakeDamageEvent;

    [Header("Navigation")]
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform[] pathWaypoints;

    [Header("Stats")]
    [SerializeField] private int startingHp = 100;
    [SerializeField] private bool hasBloodyBoots = true;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    public int Hp
    {
        get => hp;
        set => hp = value;
    }

    public int CurrentWaypointIndex
    {
        get => currentWaypointIndex;
        set => currentWaypointIndex = value;
    }

    private bool isMoving = true;
    private int currentWaypointIndex;
    private int hp;

    public void ToggleMoving(bool shouldMove)
    {
        isMoving = shouldMove;
        if (navMeshAgent) navMeshAgent.enabled = shouldMove;
    }

    private void SetDestination(Transform targetTransformWaypoint)
    {
        if (navMeshAgent)
            navMeshAgent.SetDestination(targetTransformWaypoint.position);
    }

    public void SetDestination(int waypointIndex)
    {
        SetDestination(pathWaypoints[waypointIndex]);
    }

    public void TakeDamage(int damageAmount)
    {
        hp -= damageAmount;
        float hpPercentLeft = (float)hp / startingHp;
        animator.SetLayerWeight(1, (1 - hpPercentLeft));
        onTakeDamageEvent.Invoke(hp);
        onTakeDamageEventAction?.Invoke(hp);
    }

    private void Start()
    {
        hp = startingHp;

        if (!animator) Debug.LogError("No animator found on " + gameObject.name);
        if (!navMeshAgent) Debug.LogError("No navMeshAgent found on " + gameObject.name);
        
        SetMudAreaCost();
        ToggleMoving(true);
        SetDestination(pathWaypoints[0]);
    }

    private void SetMudAreaCost()
    {
        if (hasBloodyBoots)
        {
            navMeshAgent.SetAreaCost(3, 1);
        }
    }

    [ContextMenu("Take Damage Test")]
    private void TakeDamageTesting()
    {
        TakeDamage(10);
    }


    private void Update()
    {
        if (isMoving && !navMeshAgent.isStopped && navMeshAgent.remainingDistance <= 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % pathWaypoints.Length;
            SetDestination(pathWaypoints[currentWaypointIndex]);
        }

        if (animator)
            animator.SetFloat(Speed, navMeshAgent.velocity.magnitude);
    }
}