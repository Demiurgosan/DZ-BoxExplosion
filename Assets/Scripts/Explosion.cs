using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _impulseForceMin = 5f;
    [SerializeField] private float _impulseForceMax = 10f;
    [SerializeField] private float _explosionRadius = 5f;

    public void Initiate(IReadOnlyList<Cube> newCubes, Vector3 perentCubePosition)
    {
        foreach (Cube cube in newCubes)
        {
            if (cube.Rigidbody)
            {
                float explosionForce = Random.Range(_impulseForceMin, _impulseForceMax);
                cube.Rigidbody.AddExplosionForce(explosionForce, perentCubePosition, _explosionRadius);
            }
        }
    }
}
