using Unity.VisualScripting;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private RaycastTrigger _raycastTrigger;
    [SerializeField] private Spawner _spawner;

    private int _splitChanceReductoinPerGeneration = 2;
    private int _rollChanceMin = 0;
    private int _rollChanceMax = 100;

    private void OnEnable()
    {
        _raycastTrigger.CubeDetected += TryDivideCube;
    }

    private void OnDisable()
    {
        _raycastTrigger.CubeDetected -= TryDivideCube;
    }

    private void TryDivideCube(RaycastHit hittedCube)
    {
        int cubeGeneration = hittedCube.collider.GetComponent<ExplosiveCube>().Generation;
        int currentExplosionChance = (int)((float)_rollChanceMax *
            ((float)_splitChanceReductoinPerGeneration / (float)(cubeGeneration * _splitChanceReductoinPerGeneration)));
        int chanceRoll = Random.Range(_rollChanceMin, _rollChanceMax);

        if (chanceRoll <= currentExplosionChance)
        {
            _spawner.MakeCubes(hittedCube.collider.GameObject());
        }
    }
}