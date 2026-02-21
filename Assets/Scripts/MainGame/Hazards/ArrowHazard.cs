using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHazard : MonoBehaviour
{
    [Header("Arrow")]
    [SerializeField] private ArrowObject arrowPrefab;
    [SerializeField] private Vector3 arrowRotationOffsetEuler = new Vector3(0f, 180f, 0f);

    [Header("Shooting")]
    [SerializeField] private float shootInterval = 1f;

    [Header("Pool")]
    [SerializeField] private int initialPoolSize = 8;

    private readonly Queue<ArrowObject> _pool = new Queue<ArrowObject>();
    private Coroutine _shootRoutine;

    private void Awake()
    {
        Prewarm();
    }

    private void OnEnable()
    {
        if (_shootRoutine == null)
            _shootRoutine = StartCoroutine(ShootLoop());
    }

    private void OnDisable()
    {
        if (_shootRoutine != null)
        {
            StopCoroutine(_shootRoutine);
            _shootRoutine = null;
        }
    }

    private void Prewarm()
    {
        for (int i = 0; i < initialPoolSize; i++)
            Return(CreateNew());
    }

    private IEnumerator ShootLoop()
    {
        var wait = new WaitForSeconds(shootInterval);
        while (true)
        {
            Get(transform.position, transform.rotation * Quaternion.Euler(arrowRotationOffsetEuler));
            yield return wait;
        }
    }

    private ArrowObject Get(Vector3 position, Quaternion rotation)
    {
        ArrowObject arrow = _pool.Count > 0 ? _pool.Dequeue() : CreateNew();
        arrow.transform.SetPositionAndRotation(position, rotation);
        arrow.gameObject.SetActive(true);
        return arrow;
    }

    public void Return(ArrowObject arrow)
    {
        arrow.gameObject.SetActive(false);
        arrow.transform.SetParent(null);
        _pool.Enqueue(arrow);
    }

    private ArrowObject CreateNew()
    {
        ArrowObject arrow = Instantiate(arrowPrefab);
        arrow.gameObject.SetActive(false);
        arrow.Init(this);
        return arrow;
    }
}