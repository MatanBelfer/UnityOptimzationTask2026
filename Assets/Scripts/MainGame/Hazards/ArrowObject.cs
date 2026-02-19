using UnityEngine;

public class ArrowObject : MonoBehaviour 
{
    public float speed;
    public float damage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //TODO remove
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
