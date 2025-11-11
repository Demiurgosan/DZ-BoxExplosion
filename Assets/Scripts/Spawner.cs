using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefub;

    private int _scaleReductionPerGeneration = 2;
    private int _dividePartsMin = 2;
    private int _dividePartsMax = 6;
    private List<Cube> _lastCreatedCubes = new List<Cube>();

    public Vector3 LastParentPosition
    {
        get; private set;
    }
    public IReadOnlyList<Cube> LastCreatedCubes => _lastCreatedCubes;

    private void Start()
    {
        Cube startCube = Instantiate(_cubePrefub);
        startCube.transform.position = this.transform.position;
    }

    public void Division(Cube parentCube)
    {
        _lastCreatedCubes.Clear();
        int newCubesCount = Random.Range(_dividePartsMin, _dividePartsMax);
        int childrenGeneration = parentCube.Generation + 1;
        Vector3 parentPosition = parentCube.transform.position;
        Vector3 childrenScale = Vector3.one *
            ((float)_scaleReductionPerGeneration / (float)(childrenGeneration * _scaleReductionPerGeneration));

        for (int i = 0; i < newCubesCount; i++)
        {
            _lastCreatedCubes.Add(Instantiate(_cubePrefub));
            _lastCreatedCubes[i].Initialize(childrenGeneration, parentPosition, childrenScale);

            if (_lastCreatedCubes[i].TryGetComponent(out Renderer rendererComponent))
            {
                rendererComponent.material.color = new Color(Random.value, Random.value, Random.value);
            }
        }

        LastParentPosition = parentCube.transform.position;
        Destroy(parentCube.gameObject);
    }
}