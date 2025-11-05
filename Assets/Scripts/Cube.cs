using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private int _generation;

    private Rigidbody _rb;

    public event Action<Cube> Cliñked;

    public int Generation => _generation;
    public Rigidbody RB => _rb;

    private void OnEnable()
    {
        _generation = 1;
        this.transform.localScale = Vector3.one;
        _rb = this.GetComponent<Rigidbody>();
    }

    public void Initialize(int generation, Vector3 position, Vector3 scaleChange)
    {
        _generation = generation;
        transform.position = position;
        transform.localScale = scaleChange;
    }

    public void OnMouseDown()
    {
        Cliñked.Invoke(this.gameObject.GetComponent<Cube>());
    }
}