using UnityEngine;

public class Splitter : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Explosion _explosion;

    private int _splitChanceReductoinPerGeneration = 2;
    private int _rollChanceMin = 0;
    private int _rollChanceMax = 100;

    private void OnEnable()
    {
        _raycaster.CubeDetected += TryDivideCube;
    }

    private void OnDisable()
    {
        _raycaster.CubeDetected -= TryDivideCube;
    }

    private void TryDivideCube(Cube hittedCube)
    {
        int cubeGeneration = hittedCube.Generation;
        int currentExplosionChance = (int)((float)_rollChanceMax *
            ((float)_splitChanceReductoinPerGeneration / (float)(cubeGeneration * _splitChanceReductoinPerGeneration)));
        int chanceRoll = Random.Range(_rollChanceMin, _rollChanceMax);

        if (chanceRoll <= currentExplosionChance)
        {
            _spawner.Division(hittedCube);
            _explosion.Initiate(_spawner.LastCreatedCubes, _spawner.LastParentPosition);
        }
        else
        {
            Destroy(hittedCube.gameObject);
        }
    }
}