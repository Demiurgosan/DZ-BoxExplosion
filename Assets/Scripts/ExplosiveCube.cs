using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]

public class ExplosiveCube : MonoBehaviour
{
    [SerializeField] private int _generation;

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
    }
}
