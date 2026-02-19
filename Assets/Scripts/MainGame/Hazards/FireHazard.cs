using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class FireHazard : MonoBehaviour
{
    public event UnityAction<FireEnteredEventArgs> onCharacterEnteredAction;
    
    [HideInInspector] public FireHazardScriptableObject fireHazardData;

    [SerializeField]
    private UnityEvent<FireEnteredEventArgs> onCharacterEntered = new UnityEvent<FireEnteredEventArgs>();

    //TODO remove
    
    // public void SetScriptableData(FireHazardScriptableObject fireHazardScriptableObject)
    // {
    //     fireHazardData = fireHazardScriptableObject;
    // }
    // private void Start()
    // { 
    //     if(onCharacterEnteredAction != null)
    //        onCharacterEntered.AddListener(onCharacterEnteredAction);
    // }

    private void OnTriggerEnter(Collider other)
    {
        
        //TODO change to get reference from game manager for the player character and compare that to other
        if (other.gameObject.CompareTag("PlayerCharacter")) 
        {
            Debug.Log("Player entered this hazard");
            FireEnteredEventArgs fireEnteredEventArgs = new FireEnteredEventArgs
            {
                damageDealt = fireHazardData.GetRandomFireDamage(),
                targetCharacterController = other.GetComponent<PlayerCharacterController>()
            };
            onCharacterEntered?.Invoke(fireEnteredEventArgs);
            onCharacterEnteredAction.Invoke(fireEnteredEventArgs); //TODO add null check remove?
        }
    }
}

public class FireEnteredEventArgs
{
    public int damageDealt;
    public PlayerCharacterController targetCharacterController;
}
