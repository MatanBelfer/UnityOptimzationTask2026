using UnityEngine;
using UnityEngine.Events;

public class FireHazard : MonoBehaviour
{
    public event UnityAction<FireEnteredEventArgs> onCharacterEnteredAction;

    [HideInInspector] public FireHazardScriptableObject fireHazardData;

    [SerializeField]
    private UnityEvent<FireEnteredEventArgs> onCharacterEntered = new UnityEvent<FireEnteredEventArgs>();

    private void OnTriggerEnter(Collider other)
    {
        if (other != GameManager.Instance.PlayerCharacterCollider)
            return;

        Debug.Log("Player entered this hazard");
        FireEnteredEventArgs fireEnteredEventArgs = new FireEnteredEventArgs
        {
            damageDealt = fireHazardData.GetRandomFireDamage(),
            targetCharacterController = GameManager.Instance.playerCharacterController
        };
        onCharacterEntered?.Invoke(fireEnteredEventArgs);
        onCharacterEnteredAction?.Invoke(fireEnteredEventArgs);
    }
}

public class FireEnteredEventArgs
{
    public int damageDealt;
    public PlayerCharacterController targetCharacterController;
}