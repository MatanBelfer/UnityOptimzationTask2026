using System;
using UnityEngine;

public class ArrowObject : MonoBehaviour
{
    [SerializeField] public float speed = 10f;
    [SerializeField] public float damage = 1f;
    [SerializeField] private float maxLifetime = 5f;

    private ArrowHazard _owner;
    private float _lifeLeft;
    private bool _isReturned;

    public void Init(ArrowHazard owner)
    {
        _owner = owner;
    }

    private void OnEnable()
    {
        _lifeLeft = maxLifetime;
        _isReturned = false;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        _lifeLeft -= Time.deltaTime;
        if (_lifeLeft <= 0f)
            ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (_isReturned) return;
        _isReturned = true;
        _owner.Return(this);
    }
}
