using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _impulseForceMin = 5f;
    [SerializeField] private float _impulseForceMax = 10f;
    [SerializeField] private float _explosionRadius = 5f;

    public void Initiate(Cube[] newCubes, Cube perentCube)
    {
        foreach (Cube cube in newCubes)
        {
            if (cube.RB)
            {
                float explosionForce = Random.Range(_impulseForceMin, _impulseForceMax);
                cube.RB.AddExplosionForce(explosionForce, perentCube.transform.position, _explosionRadius);
            }
        }
    }
}
