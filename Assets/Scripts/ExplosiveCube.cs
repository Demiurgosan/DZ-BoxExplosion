using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer))]

public class ExplosiveCube : MonoBehaviour
{
    [SerializeField] private int _generation;
    [SerializeField] private float _impulseForceMin = 5f;
    [SerializeField] private float _impulseForceMax = 10f;

    public event Action<GameObject> Destroing;

    public int Generation => _generation;

    private void OnEnable()
    {
        _generation = 1;
        this.transform.localScale = Vector3.one;
    }

    public void Initialize(int generation, Vector3 position, Vector3 scaleChange)
    {
        _generation = generation;
        transform.position = position;
        transform.localScale = scaleChange;
        InitialExplosion();
    }

    public void OnMouseDown()
    {
        Destroing.Invoke(this.gameObject);
    }

    private void InitialExplosion()
    {
        Vector3 impulseDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f))
               * Random.Range(_impulseForceMin, _impulseForceMax);
        GetComponent<Rigidbody>().AddForce(impulseDirection, ForceMode.Impulse);
    }
}
