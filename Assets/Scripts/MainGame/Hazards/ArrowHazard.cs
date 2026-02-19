using System;
using UnityEngine;

public class ArrowHazard : MonoBehaviour
{
    public GameObject arrowPrefab;
    [SerializeField] float shootInterval;
    private float shootIntervalLeft;
    
    private void Awake()
    {
        //TODO remove
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootIntervalLeft = shootInterval;
    }

    
    //TODO change from update to timed based call for shooting with pooling 
    
    // Update is called once per frame
    void Update()
    {
        shootIntervalLeft -= Time.deltaTime;
        if (shootIntervalLeft <= 0)
        {
            
            //TODO change to pool
            ArrowObject arrow = Instantiate(arrowPrefab,transform.position,Quaternion.identity).GetComponent<ArrowObject>();
            arrow.transform.Rotate(0,90,0);
            arrow.transform.Rotate(0,90,0);
            shootIntervalLeft = shootInterval;
        }
    }
}
