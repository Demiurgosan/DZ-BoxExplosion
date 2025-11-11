using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private int _generation;

    public int Generation => _generation;

    public Rigidbody Rigidbody
    {
        get; private set;
    }

    private void OnEnable()
    {
        _generation = 1;
        this.transform.localScale = Vector3.one;
        Rigidbody = this.GetComponent<Rigidbody>();
    }

    public void Initialize(int generation, Vector3 position, Vector3 scaleChange)
    {
        _generation = generation;
        transform.position = position;
        transform.localScale = scaleChange;
    }
}